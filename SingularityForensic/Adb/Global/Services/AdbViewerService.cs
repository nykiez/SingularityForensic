using CDFC.Info.Adb;
using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using Prism.Events;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using System;
using System.ComponentModel.Composition;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Adb.Global.Services {
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AdbViewerService {
        [ImportingConstructor]
        public AdbViewerService(ICaseService csService) {
            this._csService = csService;
            RegisterEvents();
        }

        private ICaseService _csService;
       
        /// <summary>
        /// 加载ADB节点;在连接手机并获得了信息后处理获得的结果;
        /// </summary>
        /// <param name="container"></param>
        public void LoadAdbPhoneContainer(PhoneFullInfoContainer container) {
            try {
                AddAdbInfoNode(container);
                AddContainerToCase(container);
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(AdbViewerService)}->{nameof(LoadAdbPhoneContainer)}:{ex.Message}");

                RemainingMessageBox.Tell($"{LanguageService.FindResourceString("FaileToLoadAdbInfo")}:{ex.Message}");
            }
        }

        //adb设备案件文件类型(值);
        public const string AdbCaseFileType = "AdbDevice";
        //adb设备序列号(名称);
        public const string AdbSerialNumber = "AdbSN";
        //adb设备文件文档位置(名称);
        public const string AdbStorageFile = "AdbStorageFilePath";

        //向案件中写入adb容器;
        public void AddContainerToCase(PhoneFullInfoContainer container) {
            try {
                //AdbDeviceCaseFile adbCSFile = null;
                ////查看是否存在同一个容器案件文件;
                //if (ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase.CaseEvidences.FirstOrDefault(p => p is AdbDeviceCaseFile adbCFile &&
                //adbCFile.Container.Device.Serial == container.Device.Serial) is AdbDeviceCaseFile preAdbFile) {
                //    preAdbFile.Container.CombineWith(container);
                //    container = preAdbFile.Container;
                //}
                //else {
                //    adbCSFile = new AdbDeviceCaseFile(container, DateTime.Now);
                //    ServiceProvider.Current?.GetInstance<ICaseService>().AddNewCaseFile(adbCSFile);
                //}

                ////准备本地存储;
                ////查询是否存在Adb设备目标目录;
                //var containerBinFile = $"{ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase.Path}/{adbCSFile.BasePath}/{AdbDeviceCaseFile.AdbStorageFile}";

                ////二进制存储容器;
                //var formatter = new BinaryFormatter();
                //using (var fs = File.Create(containerBinFile)) {
                //    formatter.Serialize(fs, container);
                //}
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(AdbViewerService)}->{nameof(AddAdbInfoNode)}:{ex.Message}");
                RemainingMessageBox.Tell($"{ex.Message}");
            }
            finally {
                ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase.Save();
            }
        }

        /// <summary>
        /// 加入Adb单位信息;
        /// </summary>
        /// <param name="container"></param>
        public void AddAdbInfoNode(PhoneFullInfoContainer container) {
            //if (container == null)
            //    return;

            ////查询是否具有相同的Adb设备;
            //var preAdbCFile = ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase.CaseEvidences.
            //    FirstOrDefault(p => p.EvidenceTypeGuids?.Contains(nameof(AdbDeviceCaseFile))
            //&& adbCSFile.Container?.Device?.Serial == container.Device.Serial);
            
            ////若有，则联合后移除;
            //if (preAdbCFile is AdbDeviceCaseFile adbCFile) {
            //    container.CombineWith(adbCFile.Container);
            //}

            ////向树形中加入;
            //AppInvoke(() => {
            //    AddAdbUnit(container);
            //});
        }

        private void AddAdbUnit(PhoneFullInfoContainer container) {
            //if(mNodeManagerService?.Value == null) {
            //    RemainingMessageBox.Tell($"{FindResourceString("Failed To Get FsNodeMananerService")}");
            //    return;
            //}

            //var curUnits = mNodeManagerService.Value.CurrentUnits;
            ////检查是否已经加载了相同的设备节点;
            ////若存在,则去除之;
            //if (curUnits.FirstOrDefault(p =>
            //    p is AdbDeviceCaseFileUnit fullContainer &&
            //    fullContainer.PhoneInfoContainer.Device.Serial == container.Device.Serial
            //) is AdbDeviceCaseFileUnit preContainerUnit) {
            //    mNodeManagerService.Value.RemoveUnit(preContainerUnit);
            //}


            //var adbUnit = new AdbDeviceCaseFileUnit(container,null);
            //mNodeManagerService.Value.AddUnit(adbUnit);
            //TreeUnits.Add(adbUnit);
            //NotifyUnitExpand(adbUnit);
        }
        
        private SubscriptionToken closeCaseToken;
        //private SubscriptionToken caseLoadedToken;
        //注册事件;
        private void RegisterEvents() {
            //注册关闭案件事件;
            if (closeCaseToken != null) {
                //Aggregator?.GetEvent<CloseCaseEvent>().Unsubscribe(closeCaseToken);
                closeCaseToken = null;
            }

            ////订阅案件加载事件;
            //if (caseLoadedToken != null) {
            //    Aggregator?.GetEvent<CaseLoadedEvent>().Unsubscribe(caseLoadedToken);
            //    caseLoadedToken = null;
            //}
            //caseLoadedToken = Aggregator?.GetEvent<CaseLoadedEvent>().Subscribe(() => {
            //    try {
            //        //找出所有Adb证据文件;
            //        var adbFilesElems = SingularityCase.Current.XDoc.Root.Element(XName.Get(nameof(SingularityCase.CaseFiles))).
            //        Elements(XName.Get("CaseFile")).
            //        Where(p => p.Attribute(nameof(StandardCaseFile.Type))?.Value == AdbCaseFileType);

            //        foreach (var elem in adbFilesElems) {
            //            try {
            //                var storageFile = $"{SingularityCase.Current.Path}/{elem.Element(XName.Get(AdbStorageFile)).Value}";
            //                var formatter = new BinaryFormatter();
            //                using (var fs = File.OpenRead(storageFile)) {
            //                    var container = formatter.Deserialize(fs) as PhoneFullInfoContainer;
            //                    if (container == null) {
            //                        throw new NullReferenceException($"{nameof(container)} can't be null!" +
            //                            $"SN : {elem.Element(XName.Get(AdbSerialNumber)).Value}");
            //                    }
            //                    AddAdbInfoNode(container);
            //                }
            //            }
            //            catch (Exception ex) {
            //                Logger.WriteLine($"{nameof(AdbViewerService)}->{nameof(CaseLoadedEvent)}:{ex.Message}");
            //                RemainingMessageBox.Tell($"{FindResourceString("FailedToLoadAdbDevice")}:{ex.Message}");
            //            }
            //        }
            //    }
            //    catch {

            //    }
            //});
        }
    }
}
