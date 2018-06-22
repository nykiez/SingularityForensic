using CDFC.Util.PInvoke;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace SingularityForensic.BaseDevice {

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
        
        public IFile ParseStream(Stream stream, string name, XElement xElem, IProgressReporter reporter) {
            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }
            
            var pType = GetPartsType(stream);
            if(pType == null) {
                throw new InvalidOperationException($"The {nameof(stream)} is not a valid base device stream.");
            }

            IDevice device = null;
            DeviceStoken deviceStoken = null;
            var unEntity = UnMgdBasicDeviceManagerFactory.Create(stream);
            
            //编辑Stoken;
            void EditStoken() {
                if(deviceStoken == null) {
                    throw new InvalidOperationException($"{nameof(deviceStoken)} can't be null.");
                }

                deviceStoken.BaseStream = stream;
                deviceStoken.BlockSize = 512;
                deviceStoken.Name = name;
                deviceStoken.Size = stream.Length;
            }

            switch (pType.Value) {
                case InnerPartsType.DOS:
                    device = FileFactory.CreateDevice(Constants.DeviceKey_DOS);
                    deviceStoken = device.GetStoken(Constants.DeviceKey_DOS);
                    EditStoken();
                    EditStokenOnDos(deviceStoken, xElem, unEntity);
                    break;
                case InnerPartsType.GPT:
                    device = FileFactory.CreateDevice(Constants.DeviceKey_GPT);
                    deviceStoken = device.GetStoken(Constants.DeviceKey_GPT);
                    EditStoken();
                    EditStokenOnGpt(deviceStoken, xElem, unEntity);
                    break;
                default:
                    break;
            }
            
            if (device != null) {
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
            IUnmanagedBasicDeviceManager entity) {

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
                var infoDiskIndex = 0;

                while (partNode != IntPtr.Zero) {
                    var dosPTable = partNode.GetStructure<StDosPTable>();
                    var dosPartInfo = new DOSPartInfo();
                    dosPartInfo.DosPTable = new DosPTable(dosPTable);

                    if(dosPTable.Info != IntPtr.Zero) {
                        var stInfoDisk = dosPTable.Info.GetStructure<StInFoDisk>();
                        dosPartInfo.InfoDisk = new InfoDisk(stInfoDisk) {
                            InternalDisplayName = LanguageService.FindResourceString($"{Constants.DisplayName_InfoDisk}{++infoDiskIndex}")
                        };
                    }

                    dosDeviceInfo.DosPartInfos.Add(dosPartInfo);
                    partNode = dosPTable.next;
                }

                EditDosPartEntries(dosDeviceInfo, deviceStoken);

                //编辑拓展;
                deviceStoken.SetInstance(dosDeviceInfo,Constants.DeviceStokenTag_DOSDeviceInfo);
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
                long? partSize = dosPartInfo.InfoDisk.StructInstance.AllSector * SECSIZE;
                
                switch (dosPartInfo.DosPTable.StDosPTable.DosPartType) {
                    case DosPartType.Error:
                        return;
                    case DosPartType.Main:
                        startLBA = (long)dosPartInfo.DosPTable.StDosPTable.nOffset;
                        partStartLBA = dosPartInfo.InfoDisk.StructInstance.HeadSector * SECSIZE;
                        break;
                    case DosPartType.Extend:
                        extendPartLBA += dosPartInfo.InfoDisk.StructInstance.HeadSector * SECSIZE;
                        startLBA = (long)dosPartInfo.DosPTable.StDosPTable.nOffset;
                        partSize = null;
                        partStartLBA = null;
                        break;
                    case DosPartType.Logic:
                        startLBA = (long)dosPartInfo.DosPTable.StDosPTable.nOffset;
                        partStartLBA = extendPartLBA + dosPartInfo.InfoDisk.StructInstance.HeadSector * SECSIZE;
                        break;
                    default:
                        break;
                }
                var entry = PartitionEntryFactory.CreatePartitionEntry(Constants.PartEntryKey_DOS);
                var entryStoken = entry.GetStoken(Constants.PartEntryKey_DOS);

                entryStoken.TypeGUID = FromDosPartTypeToCons(dosPartInfo.DosPTable.StDosPTable.DosPartType);

                entryStoken.StartLBA = startLBA;
                entryStoken.Size = Marshal.SizeOf(typeof(StInFoDisk));

                entryStoken.PartStartLBA = partStartLBA;
                entryStoken.PartSize = partSize;

                entryStoken.SetInstance(dosPartInfo,Constants.PartitionEntryStokenTag_DOS);
                
                deviceStoken.PartitionEntries.Add(entry);
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
        private static void EditStokenOnGpt(DeviceStoken deviceStoken,XElement xElem, IUnmanagedBasicDeviceManager entity) {
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
                var infoDiskIndex = 0;
                var efiInfoIndex = 0;
                var efiPTableIndex = 0;

                while (partNode != IntPtr.Zero) {
                    var gptPTable = partNode.GetStructure<StGptPTable>();
                    var gptPartInfo = new GPTPartInfo();
                    gptPartInfo.StGptPTable = gptPTable;
                    
                    if (gptPTable.InfoDisk != IntPtr.Zero) {
                        var stInfoDisk = gptPTable.InfoDisk.GetStructure<StInFoDisk>();
                        gptPartInfo.InfoDisk = new InfoDisk(stInfoDisk) {
                            InternalDisplayName = $"{LanguageService.FindResourceString(Constants.DisplayName_InfoDisk)}{++infoDiskIndex}"
                        };
                    }
                    
                    if(gptPTable.EFIInfo != IntPtr.Zero) {
                        var stEFIInfo = gptPTable.EFIInfo.GetStructure<StEFIInfo>();
                        gptPartInfo.EFIInfo = new EFIInfo(stEFIInfo) {
                            InternalDisplayName = $"{LanguageService.FindResourceString(Constants.DisplayName_EFIInfo)}{++efiInfoIndex}"
                        };
                    }

                    if(gptPTable.EFIPTable != IntPtr.Zero) {
                        var stEFITable = gptPTable.EFIPTable.GetStructure<StEFIPTable>();
                        gptPartInfo.EFIPTable = new EFIPTable(stEFITable) {
                            InternalDisplayName = $"{LanguageService.FindResourceString(Constants.DisplayName_EFIPTable)}{++efiPTableIndex}"
                        };
                    }

                    gptDeviceInfo.GptPartInfos.Add(gptPartInfo);
                    partNode = gptPTable.next;
                }

                EditGptPartEntries(gptDeviceInfo, deviceStoken);

                //编辑拓展;
                deviceStoken.SetInstance(gptDeviceInfo,Constants.DeviceStokenTag_GPTDeviceInfo);
                
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
                if(gptPartInfo.EFIPTable == null) {
                    return;
                }

                var efiTable = gptPartInfo.EFIPTable.StructInstance ;
                var entry = PartitionEntryFactory.CreatePartitionEntry(Constants.PartEntryKey_GPT);
                var entryStoken = entry.GetStoken(Constants.PartEntryKey_GPT);
                entryStoken.TypeGUID = Constants.PartEntryType_GPT;

                entryStoken.StartLBA = (long)gptPartInfo.StGptPTable.nOffset;
                entryStoken.Size = Marshal.SizeOf(typeof(StEFIPTable));

                entryStoken.PartStartLBA = (long)efiTable.PartTabStartLBA * deviceStoken.BlockSize;
                entryStoken.PartSize = (long)(efiTable.PartTabEndLBA - efiTable.PartTabStartLBA) * deviceStoken.BlockSize;
                
                deviceStoken.PartitionEntries.Add(entry);
            });
        }
        
        /// <summary>
        /// 释放非托管的内存;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected static void OnDeviceDisposing(object sender, EventArgs e) {
            if (!(sender is IDevice device)) {
                return;
            }

            DeviceStoken deviceStoken = null;
            BaseDeviceInfo deviceInfo = null;

            //验证类型,尝试获取凭据;
            try {
                if (device.TypeGuids?.Contains(Constants.DeviceType_DOS) ?? false) {
                    deviceStoken = device.GetStoken(Constants.DeviceKey_DOS);
                    deviceInfo = deviceStoken.GetInstance<DOSDeviceInfo>(Constants.DeviceStokenTag_DOSDeviceInfo);
                }
                else if(device.TypeGuids?.Contains(Constants.DeviceType_DOS) ?? false){
                    deviceStoken = device.GetStoken(Constants.DeviceKey_GPT);
                    deviceInfo = deviceStoken.GetInstance<GPTDeviceInfo>(Constants.DeviceStokenTag_GPTDeviceInfo);
                }
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            //若凭据为空,需返回;
            if (deviceStoken == null) {
                return;
            }

            if(deviceInfo == null) {
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
            IUnmanagedBasicDeviceManager unManagedManager = null;

            try {
                unManagedManager = UnMgdBasicDeviceManagerFactory.Create(stream);
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
        
        /// <summary>
        /// 从Dos分区表项类型转换至Constants;
        /// </summary>
        /// <param name="dosPartType"></param>
        /// <returns></returns>
        private static string FromDosPartTypeToCons(DosPartType dosPartType) {
            switch (dosPartType) {
                case DosPartType.Error:
                    return Constants.PartEntryType_DOS_Error;
                case DosPartType.Main:
                    return Constants.PartEntryType_DOS_Main;
                case DosPartType.Extend:
                    return Constants.PartEntryType_DOS_Extended;
                case DosPartType.Logic:
                    return Constants.PartEntryType_DOS_Logic;
                default:
                    return Constants.PartEntryType_DOS_Error;
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
