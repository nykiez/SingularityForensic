using SingularityForensic.Contracts.Casing;
using System.ComponentModel.Composition;

namespace SingularityForensic.Controls.ITunes.Global.Services {
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class ForensicService {
        /// <summary>
        /// 开始对某个案件文件取证;
        /// </summary>
        /// <param name="cFile"></param>
        public void StartForensic(ICaseEvidence csFile) {
            //var window = new StartForensicWindow();
            //var vm = ServiceProvider.Current.GetInstance<ITunesStartForensicWindowViewModel>();

            //window.DataContext = vm;
            //vm.DeviceFile = csFile;
            //vm.CloseRequest += delegate {
            //    window.Close();
            //};

            //window.ShowDialog();
            //ITunesBackUpCaseFile cFile
        }

        /// <summary>
        /// 加载取证信息节点;
        /// </summary>
        /// <param name="adCFile"></param>
        public void LoadForensicUnit(ICaseEvidence adCFile) {
            //var frService = ServiceProvider.Current.GetInstance<ICommonForensicService>();
            ////加载取证分析节点;
            //var fUnit = frService?.AddForensicUnit(adCFile);
            //if (fUnit == null) {
            //    RemainingMessageBox.Tell($"{nameof(fUnit)} can't be null!");
            //    return;
            //}

            //foreach (var infoKind in PinKindsDefinitions.ForensicClassTypes) {
            //    if (fUnit.Children.FirstOrDefault(p => p is PinTreeUnit pinUnit && pinUnit.ContentId == infoKind) == null) {
            //        fUnit.Children.Add(
            //            new PinTreeUnit(infoKind, fUnit) {
            //                Label = PinKindsDefinitions.GetClassLabel(infoKind)
            //            }
            //        );
            //    }
            //}
        }
    }
}
