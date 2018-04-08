using CDFC.Util.PInvoke;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace SingularityForensic.FileSystem {

    /// <summary>
    /// 基础分区表解析装置(GPT/DOS);
    /// </summary>
    [Export(typeof(IStreamParsingProvider))]
    partial class BaseDeviceStreamParsingProvider : IStreamParsingProvider {
        private const int SECSIZE = 512;
        public int Order => 64;

        public string GUID => Constants.StreamParser_BaseDevice;

        public bool CheckIsValidStream(Stream stream) {
            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            return GetPartsType(stream) != null;
        }
        
        public FileBase ParseStream(Stream stream, string name, XElement xElem, ProgressReporter reporter) {
            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }
            
            var pType = GetPartsType(stream);
            if(pType == null) {
                throw new InvalidOperationException($"The {nameof(stream)} is not a valid base device stream.");
            }

            Device device = null;

            var unEntity = new UnmanagedBasicDeviceManager(stream);

            //构建Stoken;
            var deviceStoken = new DeviceStoken {
                BaseStream = stream,
                BlockSize = 512,
                Name = name,
                Size = stream.Length
            };

            switch (pType.Value) {
                case InnerPartsType.DOS:
                    EditStokenOnDos(deviceStoken, xElem, unEntity);
                    device = new Device(Constants.DeviceKey_DOS, deviceStoken);
                    break;
                case InnerPartsType.GPT:
                    EditStokenOnGpt(deviceStoken, xElem, unEntity);
                    device = new Device(Constants.DeviceKey_GPT, deviceStoken);
                    break;
                default:
                    break;
            }

            if(device != null) {
                //加载分区;
                device.FillParts(xElem, reporter);
                device.Disposing += OnDeviceDisposing;
            }
            

            return device;
        }
        
        /// <summary>
        /// 编辑Dos设备的Stoken;
        /// </summary>
        /// <param name="deviceStoken"></param>
        /// <param name="xElem">案件文件相关Xml元素</param>
        private static void EditStokenOnDos(
            DeviceStoken deviceStoken,
            XElement xElem,
            UnmanagedBasicDeviceManager entity) {

            if(deviceStoken == null) {
                throw new ArgumentNullException(nameof(deviceStoken));
            }
            
            var dosDeviceInfo = new DOSDeviceInfo();
            try {
                deviceStoken.TypeGuids = new string[] {
                    Constants.DeviceType_DOS
                };
                deviceStoken.PartsType = Constants.PartsType_DOS;
                
                //获取Dos链表;
                var partPtr = Partition_Get_DosPTable(entity.BasicDevicePtr);
                var partNode = partPtr;
                
                while (partNode != IntPtr.Zero) {
                    var dosPTable = partNode.GetStructure<StDosPTable>();
                    var dosPartInfo = new DOSPartInfo();
                    dosPartInfo.StDosPTable = dosPTable;

                    if(dosPTable.Info != IntPtr.Zero) {
                        var stInfoDisk = dosPTable.Info.GetStructure<StInFoDisk>();
                        dosPartInfo.StInFoDisk = stInfoDisk;
                    }

                    dosDeviceInfo.DosPartInfos.Add(dosPartInfo);
                    partNode = dosPTable.next;
                }

                EditDosPartEntries(dosDeviceInfo, deviceStoken);

                //编辑拓展;
                deviceStoken.Tag = dosDeviceInfo;
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }

        /// <summary>
        /// 根据Dos分区表项编辑Dos分区项;
        /// </summary>
        /// <param name="dosDeviceInfo"></param>
        private static void EditDosPartEntries(DOSDeviceInfo dosDeviceInfo,DeviceStoken deviceStoken) {
            if(dosDeviceInfo == null) {
                throw new ArgumentNullException(nameof(dosDeviceInfo));
            }
            if(deviceStoken == null) {
                throw new ArgumentNullException(nameof(deviceStoken));
            }

            //拓展分区偏移;
            long extendPartLBA = 0;
            
            dosDeviceInfo.DosPartInfos.ForEach(dosPartInfo => {
                //确定起始绝对位移;
                long startLBA = 0;
                long? partStartLBA = null;
                long? partSize = dosPartInfo.StInFoDisk.Value.AllSector * SECSIZE;
                
                switch (dosPartInfo.StDosPTable.DosPartType) {
                    case DosPartType.Error:
                        return;
                    case DosPartType.Main:
                        startLBA = (long)dosPartInfo.StDosPTable.nOffset;
                        partStartLBA = dosPartInfo.StInFoDisk.Value.HeadSector * SECSIZE;
                        break;
                    case DosPartType.Extend:
                        extendPartLBA += dosPartInfo.StInFoDisk.Value.HeadSector * SECSIZE;
                        startLBA = (long)dosPartInfo.StDosPTable.nOffset;
                        partSize = null;
                        partStartLBA = null;
                        break;
                    case DosPartType.Logic:
                        startLBA = (long)dosPartInfo.StDosPTable.nOffset;
                        partStartLBA = extendPartLBA + dosPartInfo.StInFoDisk.Value.HeadSector * SECSIZE;
                        break;
                    default:
                        break;
                }

                var entryStoken = new PartitionEntryStoken {
                    TypeGUID = FromDosPartTypeToCons(dosPartInfo.StDosPTable.DosPartType),

                    StartLBA = startLBA,
                    Size = Marshal.SizeOf(typeof(StInFoDisk)),

                    PartStartLBA = partStartLBA,
                    PartSize = partSize,

                    Tag = dosPartInfo,
                };
                deviceStoken.PartitionEntries.Add(
                    new PartitionEntry(Constants.PartEntryKey_Dos,entryStoken)
                );
            });
            
        }

        /// <summary>
        /// 编辑GPT设备的Stoken;
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="name"></param>
        /// <param name="xElem"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        private static void EditStokenOnGpt(DeviceStoken deviceStoken,XElement xElem, UnmanagedBasicDeviceManager entity) {
            if (deviceStoken == null) {
                throw new ArgumentNullException(nameof(deviceStoken));
            }

            var gptDeviceInfo = new GPTDeviceInfo();
            try {
                deviceStoken.TypeGuids = new string[] {
                    Constants.DeviceType_GPT
                };
                deviceStoken.PartsType = Constants.PartsType_GPT;
                
                //获取GPT链表;
                var partPtr = Partition_Get_GptPTable(entity.BasicDevicePtr);
                var partNode = partPtr;
                while (partNode != IntPtr.Zero) {
                    var gptPTable = partNode.GetStructure<StGptPTable>();
                    var gptPartInfo = new GPTPartInfo();
                    gptPartInfo.StGptPTable = gptPTable;

                    if (gptPTable.Info != IntPtr.Zero) {
                        var stInfoDisk = gptPTable.Info.GetStructure<StInFoDisk>();
                        gptPartInfo.StInFoDisk = stInfoDisk;
                    }
                    
                    if(gptPTable.EFIInfo != IntPtr.Zero) {
                        var stEFIInfo = gptPTable.EFIInfo.GetStructure<StEFIInfo>();
                        gptPartInfo.StEFIInfo = stEFIInfo;
                    }

                    if(gptPTable.EFIPTable != IntPtr.Zero) {
                        var stEFITable = gptPTable.EFIPTable.GetStructure<StEFIPTable>();
                        gptPartInfo.StEFIPTable = stEFITable;
                    }

                    gptDeviceInfo.GptPartInfos.Add(gptPartInfo);
                    partNode = gptPTable.next;
                }

                EditGptPartEntries(gptDeviceInfo, deviceStoken);

                //编辑拓展;
                deviceStoken.Tag = gptDeviceInfo;
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }

        /// <summary>
        /// 根据Gpt分区表项编辑Gpt分区项;
        /// </summary>
        /// <param name="gptDeviceInfo"></param>
        /// <param name="devieStoken"></param>
        private static void EditGptPartEntries(GPTDeviceInfo gptDeviceInfo,DeviceStoken deviceStoken) {
            if (gptDeviceInfo == null) {
                throw new ArgumentNullException(nameof(gptDeviceInfo));
            }
            if (deviceStoken == null) {
                throw new ArgumentNullException(nameof(deviceStoken));
            }

            gptDeviceInfo.GptPartInfos.ForEach(gptPartInfo => {
                //若EFIPTable为空,则非分区;
                if(gptPartInfo.StEFIPTable == null) {
                    return;
                }

                var efiTable = gptPartInfo.StEFIPTable.Value;
                var entryStoken = new PartitionEntryStoken {
                    TypeGUID = Constants.PartEntryType_GPT,

                    StartLBA = (long)gptPartInfo.StGptPTable.nOffset,
                    Size = Marshal.SizeOf(typeof(StEFIPTable)),

                    PartStartLBA = (long) efiTable.PartTabStartLBA,
                    PartSize = (long)(efiTable.PartTabEndLBA - efiTable.PartTabStartLBA)
                };

                deviceStoken.PartitionEntries.Add(
                    new PartitionEntry(Constants.PartEntryKey_GPT,entryStoken)
                );
            });
        }
        
        /// <summary>
        /// 释放非托管的内存;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected static void OnDeviceDisposing(object sender, EventArgs e) {
            if (!(sender is Device device)) {
                return;
            }

            DeviceStoken deviceStoken = null;
            //验证类型,尝试获取凭据;
            try {
                if (device.TypeGuids?.Contains(Constants.DeviceType_DOS) ?? false) {
                    deviceStoken = device.GetStoken(Constants.DeviceKey_DOS);
                }
                else if(device.TypeGuids?.Contains(Constants.DeviceType_DOS) ?? false){
                    deviceStoken = device.GetStoken(Constants.DeviceKey_GPT);
                }
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            //若凭据为空,需返回;
            if (deviceStoken == null) {
                return;
            }

            if (!(deviceStoken.Tag is BaseDeviceInfo deviceInfo)) {
                return;
            }

            try {
                deviceInfo.UnmanagedManager.Dispose();
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

        }

        /// <summary>
        /// 获取分区表类型;
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns></returns>
        private static InnerPartsType? GetPartsType(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            InnerPartsType? pType = null;
            UnmanagedBasicDeviceManager unManagedManager = null;

            try {
                unManagedManager = new UnmanagedBasicDeviceManager(stream);
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            if (unManagedManager == null) {
                LoggerService.WriteCallerLine($"{nameof(unManagedManager)} can't be null.");
                pType = null;
            }
            
            //判断是否是符合"签名";
            try {
                if(unManagedManager.BasicDevicePtr == IntPtr.Zero) {
                    LoggerService.WriteCallerLine($"{nameof(unManagedManager.BasicDevicePtr)} can't be nullptr.");
                }
                else if (Partition_B_Dos(unManagedManager.BasicDevicePtr)) {
                    pType = InnerPartsType.DOS;
                }
                else if (Partition_B_Gpt(unManagedManager.BasicDevicePtr)) {
                    pType = InnerPartsType.GPT;
                }
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            unManagedManager?.Dispose();

            return pType;
        }
    }

    /// <summary>
    /// 内部实体;
    /// </summary>
    partial class BaseDeviceStreamParsingProvider {
        //分区表类型;
        private enum InnerPartsType {
            DOS,
            GPT
        }
        
        
        /// <summary>
        /// DOS/GPT设备存储信息基类,将会保存在FileBase->Tag字段中;
        /// </summary>
        internal abstract class BaseDeviceInfo {
            public UnmanagedBasicDeviceManager UnmanagedManager { get; set; }
        }

        /// <summary>
        /// //Dos设备信息;
        /// </summary>
        internal class DOSDeviceInfo : BaseDeviceInfo {
            public List<DOSPartInfo> DosPartInfos { get; } = new List<DOSPartInfo>();
            
            private const int SECSIZE = 512;
        }

        /// <summary>
        /// GPT设备信息;
        /// </summary>
        internal class GPTDeviceInfo : BaseDeviceInfo {
            public List<GPTPartInfo> GptPartInfos { get; } = new List<GPTPartInfo>();
            
            private const int SECSIZE = 512;
        }

        /// <summary>
        /// //GPT/Dos分区信息基类;
        /// </summary>
        internal abstract class BasePartInfo {
            public StInFoDisk? StInFoDisk { get; set; }
        }

        /// <summary>
        /// //Dos分区项信息;
        /// </summary>
        internal class DOSPartInfo : BasePartInfo {
            public StDosPTable StDosPTable { get; set; }
        }

        /// <summary>
        /// //GPT分区项信息;
        /// </summary>
        internal class GPTPartInfo : BasePartInfo {
            public StGptPTable StGptPTable { get; set; }
            public StEFIInfo? StEFIInfo { get; set; }
            public StEFIPTable? StEFIPTable { get; set; }
        }

        /// <summary>
        /// 从Dos分区表项类型转换至Constants;
        /// </summary>
        /// <param name="dosPartType"></param>
        /// <returns></returns>
        private static string FromDosPartTypeToCons(DosPartType dosPartType) {
            switch (dosPartType) {
                case DosPartType.Error:
                    return Constants.PartEntryType_Dos_Error;
                case DosPartType.Main:
                    return Constants.PartEntryType_Dos_Main;
                case DosPartType.Extend:
                    return Constants.PartEntryType_Dos_Extended;
                case DosPartType.Logic:
                    return Constants.PartEntryType_Dos_Logic;
                default:
                    return Constants.PartEntryType_Dos_Error;
            }
        }
    }

    /// <summary>
    /// 本地非托管方法;
    /// </summary>
    partial class BaseDeviceStreamParsingProvider {
        private const string partAsm = "PartitionManager.dll";
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Partition_Init(IntPtr stStream);

        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static bool Partition_B_Gpt(IntPtr stPartition);

        //StGptPTable* Partition_Get_GptPTable(IntPtr stPartition);
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Partition_Get_GptPTable(IntPtr stPartition);


        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static bool Partition_B_Dos(IntPtr stPartition);

        //StDosPTable* Partition_Get_DosPTable(void* stPartition);
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Partition_Get_DosPTable(IntPtr stPartition);

        
    }
}
