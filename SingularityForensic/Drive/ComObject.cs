using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using CDFC.Util.PInvoke;
using System.Runtime.InteropServices;
using SingularityForensic.Contracts.App;
using Microsoft.Win32.SafeHandles;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Drive {
    public class ComObject : IDisposable{
        private ComObject() { }
        [DllImport("cdfc_device.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static bool cdfc_devices_init();
        [DllImport("cdfc_device.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static bool cdfc_devices_exit();
        [DllImport("cdfc_device.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr cdfc_devices_devicelist();
        [DllImport("cdfc_device.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr cdfc_devices_patitionlist();
        [DllImport("cdfc_device.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr get_hdd_vender();
        [DllImport("cdfc_device.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr exit_hdd_vender();

        private static ComObject _current;
        /// <summary>
        /// 获得本地存储介质的对象;
        /// </summary>
        public static ComObject Current {
            [HandleProcessCorruptedStateExceptions]
            get {
                if(_current == null) {
                    _current = new ComObject();
                    try {
                        _current.Initialize();
                    }
                    catch(Exception ex) {
                        LoggerService.Current?.WriteCallerLine(ex.Message);
                        throw;
                    }
                }
                return _current;
            }
        }

        public void Refresh() {
            ReleaseHandle();

            Initialize();
        }

        //初始化存储介质管理单元;
        private void Initialize() {
            InitilizeHdds();
            InitilizeVolumes();
        }

        //初始化本地硬盘;
        private void InitilizeHdds() {
            LoggerService.Current?.WriteCallerLine("开始加载设备基本信息");
            _partitionInDeviceStructs.Clear();

            bool res = false;
            try {
#if DEBUG
                LoggerService.Current?.WriteCallerLine("开始执行八爷初始化");
#endif
                res = cdfc_devices_init();
#if DEBUG
                LoggerService.Current?.WriteCallerLine("八爷初始化执行返回");
#endif
            }
            catch (AccessViolationException ex) {
                LoggerService.Current?.WriteCallerLine("cdfc_devices_init()错误!" + ex.Message);
                throw;
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine("cdfc_devices_init()未知错误!" + ex.Message);
                throw;
            }

            if (!res) {
                throw new Exception($"{nameof(cdfc_devices_init)} returned false");
            }


            LoggerService.Current?.WriteCallerLine("设备初始化成功!");
            
            var hddPtr = get_hdd_vender();
            var devicePtr = cdfc_devices_devicelist();
            
            var deviceNode = devicePtr;
            
            while (deviceNode != IntPtr.Zero) {
                var deviceStructList = deviceNode.GetStructure<DeviceListStruct>();
                PhysicsDeviceStruct deviceStruct;
                try {
                    deviceStruct = deviceStructList.m_ThisDevice.GetStructure<PhysicsDeviceStruct>();
                }
                catch (Exception ex) {
                    LoggerService.Current?.WriteCallerLine("ComObject->LocalObject->deviceStructList.m_ThisDevice.GetStructure<PhysicsDeviceStruct>错误:" + ex.Message);
                    break;
                }
                LoggerService.Current?.WriteCallerLine("获得设备" + deviceStruct.DevName + "大小:" + deviceStruct.DevSize);
                //若ObjectID为16，则为分区(已废弃)，否则为设备；
                if (deviceStruct.ObjectID != 16) {
                    var device = new LocalHDD(deviceStruct);
                    for (var hddNode = hddPtr; hddNode != IntPtr.Zero;) {
                        var hddInfoStruct = hddNode.GetStructure<HDDInfoStruct>();
                        if (hddInfoStruct.ID == device.DeviceID) {
                            device.InternalHDDInfo = new HddInfo(hddInfoStruct);
                        }
                        hddNode = hddInfoStruct.Next;
                    }

                    _localHdds.Add(device);
                }
                else {
                    _partitionInDeviceStructs.Add(deviceStruct);
                }
                LoggerService.Current?.WriteCallerLine("Device:" + deviceNode);
                deviceNode = deviceStructList.m_next;
            }

        }

        //糟糕的硬盘内分区链表;
        private List<PhysicsDeviceStruct?> _partitionInDeviceStructs = new List<PhysicsDeviceStruct?>();

        //初始化本地盘符;
        private void InitilizeVolumes() {
            var partitionPtr = cdfc_devices_patitionlist();
            var partitionNode = partitionPtr;
            while (partitionNode != IntPtr.Zero) {
                var ptList = partitionNode.GetStructure<PartitonListStruct>();
                //为避免编写重复的continue代码,此处编写一个循环内方法,进行判断,分区添加工作;
                //return 即为continue;
                void CheckAndAdd() {
                    //判断_mThisPartition是否为空;
                    if (ptList.m_ThisPartition == IntPtr.Zero) {
                        LoggerService.Current.WriteCallerLine($"{nameof(ptList.m_ThisPartition)} is null.");
                        return;
                    }

                    var partitionStruct = ptList.m_ThisPartition.GetStructure<VolumeStruct>();

                    //若挂载号不是有效的盘标号,则不可用,需跳过;
                    var sign = Convert.ToChar(partitionStruct.m_Sign);
                    if (!char.IsLetter(sign)) {
                        LoggerService.Current.WriteCallerLine($"{nameof(partitionStruct.m_Sign)} is not a valid volume sign-{sign}");
                        return;
                    }

                    var device = _localHdds.FirstOrDefault(p => p.DeviceID == partitionStruct.m_LoGo);

                    if (device == null) {
                        LoggerService.Current?.WriteCallerLine(
                            $"Matched Hdd not found:{nameof(partitionStruct.m_LoGo)}-{partitionStruct.m_LoGo}"
                        );
                        return;
                    }

                    if (device != null) {
                        var partition = new LocalVolume(partitionStruct) {
                            InternalSecSize = device.SectorSize
                        };
                        device.InternalVolumes.Add(partition);

                        LoggerService.Current?.WriteCallerLine(
                            $"Partition aquired:{Convert.ToChar(partitionStruct.m_Sign)}" +
                            $"Size:{partitionStruct.m_Size}"
                        );
                    }
                }

                CheckAndAdd();
                partitionNode = ptList.m_next;
            }

            //附载分区到硬盘上;
            _localHdds.ForEach(p => {
                if (p.Volumes == null) {
                    return;
                }
                foreach (var volume in p.Volumes) {
                    var devName = @"\\.\" + volume.Sign + ":";
                    var partitionInDevice = _partitionInDeviceStructs.FirstOrDefault(t => t.Value.DevName == devName);
                    if((partitionInDevice?.Handle??IntPtr.Zero) != IntPtr.Zero) {
                        volume.InternalHandle = new SafeFileHandle(partitionInDevice.Value.Handle, false);
                    }
                }

            });
        }
        
        public void Dispose() {
            ReleaseHandle();
        }

        //释放;
        private void ReleaseHandle() {
            try {
                cdfc_devices_exit();
                exit_hdd_vender();
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }   
        }

        public IEnumerable<LocalHDD> LocalHdds => _localHdds.Select(p => p);
        private List<LocalHDD> _localHdds = new List<LocalHDD>();
       

    }
    
}
