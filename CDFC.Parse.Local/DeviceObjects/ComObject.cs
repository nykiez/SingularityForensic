using System;
using System.Collections.Generic;
using System.Linq;
using CDFCStatic.CMethods;
using CDFCEntities.Structs;
using CDFCEntities.DeviceInfoes;
using System.Runtime.ExceptionServices;
using EventLogger;
using CDFC.Util.PInvoke;

namespace CDFCEntities.DeviceObjects {
    public class ComObject {
        /// <summary>
        /// 获得本地的对象;
        /// </summary>
        public static ComObject LocalObject {
            [HandleProcessCorruptedStateExceptions]
            get {
                Logger.WriteLine("开始加载设备基本信息");
                bool res = false;
                try {
#if DEBUG
                EventLogger.Logger.WriteLine("开始执行八爷初始化");
#endif
                    res = ComObjectMethods.cdfc_devices_init();
#if DEBUG
                EventLogger.Logger.WriteLine("八爷初始化执行成功");
#endif
                }
                catch (AccessViolationException ex) {
                    Logger.WriteLine("cdfc_devices_init()错误!" + ex.Message);
                    return null;
                }
                catch (Exception ex) {
                    Logger.WriteLine("cdfc_devices_init()未知错误!" + ex.Message);
                    return null;
                }

                if (res) {
                    Logger.WriteLine("设备初始化成功!");
                    ComObject comObject = new ComObject();
                    List<Device> devices = comObject.Devices;
                    List<PhysicsDeviceStruct> partitionInDeviceStructs = new List<PhysicsDeviceStruct>();

                    var hddPtr = ComObjectMethods.get_hdd_vender();
                    var devicePtr = ComObjectMethods.cdfc_devices_devicelist();
                    var partitionPtr = ComObjectMethods.cdfc_devices_patitionlist();

                    var deviceNode = devicePtr;
                    var partitionNode = partitionPtr;

                    while (deviceNode != IntPtr.Zero) {
                        var deviceStructList = deviceNode.GetStructure<DeviceListStruct>();
                        PhysicsDeviceStruct deviceStruct;
                        try { 
                             deviceStruct = deviceStructList.m_ThisDevice.GetStructure<PhysicsDeviceStruct>();
                        }
                        catch(Exception ex) {
                            Logger.WriteLine("ComObject->LocalObject->deviceStructList.m_ThisDevice.GetStructure<PhysicsDeviceStruct>错误:" + ex.Message);
                            break;
                        }
                        Logger.WriteLine("获得设备"+deviceStruct.DevName+"大小:" + deviceStruct.DevSize);
                        //若ObjectID为16，则为分区（已废弃），否则为设备；
                        if (deviceStruct.ObjectID != 16) {
                            Device device = Device.Create(deviceStruct);
                            for (var hddNode = hddPtr; hddNode != IntPtr.Zero;) {
                                var hddInfoStruct = hddNode.GetStructure<HDDInfoStruct>();
                                if (hddInfoStruct.ID == device.DeviceID) {
                                    device.HddInfo = HddInfo.Create(hddInfoStruct);
                                }
                                hddNode = hddInfoStruct.Next;
                            }
                            devices.Add(device);
                        }
                        else {
                            partitionInDeviceStructs.Add(deviceStruct);
                        }
                        Logger.WriteLine("Device:" + deviceNode);
                        deviceNode = deviceStructList.m_next;
                    }
                    while (partitionNode != IntPtr.Zero) {
                        var ptList = partitionNode.GetStructure<PartitonListStruct>();
                        
                        var partitionStruct = ptList.m_ThisPartition.GetStructure<PartitonStruct>();
                        var device = devices.FirstOrDefault(p => p.DeviceID == partitionStruct.m_LoGo);

                        if (device != null) {
                            var partition = Partition.Create(partitionStruct);
                            partition.Device = device;
                            partition.SectorSize = device.SectorSize;
                            device.Partitions.Add(partition);
                        }
                        
                        Logger.WriteLine("已获得Partition:"+Convert.ToChar(partitionStruct.m_Sign)+"Size:"+
                            partitionStruct.m_Size);
                        partitionNode = ptList.m_next;
                    }
                    deviceNode = devicePtr;
                    devices.ForEach(p => {
                        p.Partitions.ForEach(q => {
                            var devName = @"\\.\" + q.Sign + ":";
                            var partitionInDevice = partitionInDeviceStructs.FirstOrDefault(t => t.DevName == devName);
                            if (partitionInDevice.Handle != null) {
                                q.Handle = partitionInDevice.Handle;
                            }
                        });
                    });
                    return comObject;
                }
                else {
                    Logger.WriteLine("设备初始化失败!");
                    throw new Exception("Initializing Error!");
                }
            }
           
        }
        public void Exit() {
            ComObjectMethods.cdfc_devices_exit();
            ComObjectMethods.exit_hdd_vender();
            ImgFiles.ForEach(p => p.Exit());
            Devices.Clear();
        }
        public List<ImgFile> ImgFiles { get; set; }
        public List<Device> Devices { get; set; }
        public ComObject() {
            Devices = new List<Device>();
            ImgFiles = new List<ImgFile>();
        }

    }
    
}
