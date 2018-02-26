using System;
using System.ComponentModel.Composition;
using System.Linq;
using EventLogger;
using System.Runtime.Serialization.Formatters.Binary;
using CDFC.Info.Adb;
using System.IO;
using CDFCMessageBoxes.MessageBoxes;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
using Microsoft.Practices.ServiceLocation;
using Singularity.UI.AdbViewer.Global.Services;
using Singularity.UI.Case;
using Singularity.Contracts.Case;
using Singularity.Contracts.Common;

namespace Singularity.UI.AdbViewer.Models {
    [Export(typeof(ICaseManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AdbDeviceCaseManager : ICaseManager {
        public int SortOrder => 0;

        public void LoadCase(CaseLoadingHanlder loadingHanlder, Func<bool> isCancel) {
            if(ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase == null) {
                Logger.WriteLine($"{nameof(AdbDeviceCaseManager)}->{nameof(LoadCase)}:{nameof(ICaseService)}-{nameof(ICaseService.CurrentCase)} can't be null.");
                return;
            }

            var cs = ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase;
            //找出所有Adb证据文件;
            var adbFilesElems = cs.XDoc.Root.
                Element(nameof(ICase.CaseEvidences)).
                Elements("CaseFile").
                Where(p => p.Attribute(nameof(ICaseEvidence.Type))?.Value == AdbDeviceCaseFile.AdbCaseFileType);

            //遍历adb证据文件;寻找容器序列化;
            foreach (var elem in adbFilesElems) {
                try {
                    var storageFile = $"{ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase.Path}/{elem.Element(nameof(ICaseEvidence.BasePath)).Value}/{AdbDeviceCaseFile.AdbStorageFile}";
                    var formatter = new BinaryFormatter();
                    using (var fs = File.OpenRead(storageFile)) {
                        var container = formatter.Deserialize(fs) as PhoneFullInfoContainer;
                        if (container == null) {
                            throw new NullReferenceException($"{nameof(container)} can't be null!" +
                                $"SN : {elem.Element(AdbDeviceCaseFile.AdbSerialNumber).Value}");
                        }
                        var cFile = new AdbDeviceCaseFile(container, elem);
                        ServiceProvider.Current.GetInstance<ICaseService>()?.AddNewCaseFile(cFile);
                        ServiceProvider.Current.GetInstance<AdbViewerService>()?.AddAdbInfoNode(container);
                    }
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(AdbDeviceCaseManager)}->{nameof(LoadCase)}:{ex.Message}");
                    Application.Current.Dispatcher.Invoke(() => {
                        RemainingMessageBox.Tell($"{FindResourceString("FailedToLoadAdbDevice")}:{ex.Message}");
                    });
                }
            }
        }

        public void Uninstall() {
            
        }
    }
}
