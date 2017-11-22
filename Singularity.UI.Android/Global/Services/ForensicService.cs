using CDFCMessageBoxes.MessageBoxes;
using Microsoft.Practices.ServiceLocation;
using Singularity.UI.FileSystem.Models;
using Singularity.UI.Info.Android.ViewModels;
using Singularity.UI.Info.Global.Services;
using Singularity.UI.Info.Views;
using SingularityForensic.Modules.MainPage.Models;
using System.ComponentModel.Composition;
using System.Linq;

namespace Singularity.UI.Info.Android.Global.Services {
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class ForensicService {
        /// <summary>
        /// 开始对某个案件文件取证;
        /// </summary>
        /// <param name="cFile"></param>
        public void StartForensic(AndroidDeviceCaseFile cFile) {
            var window = new StartForensicWindow();
            var vm = ServiceLocator.Current.GetInstance<AndStartForensicWindowViewModel>();

            window.DataContext = vm;
            vm.DeviceFile = cFile;
            vm.CloseRequest += delegate {
                window.Close();
            };
            
            window.ShowDialog();
        }

        /// <summary>
        /// 加载取证信息节点;
        /// </summary>
        /// <param name="adCFile"></param>
        public void LoadForensicUnit(AndroidDeviceCaseFile adCFile) {
            var frService = ServiceLocator.Current.GetInstance<ICommonForensicService>();
            //加载取证分析节点;
            var fUnit = frService?.AddForensicUnit(adCFile);
            if (fUnit == null) {
                RemainingMessageBox.Tell($"{nameof(fUnit)} can't be null!");
                return;
            }

            foreach (var infoKind in PinKindsDefinitions.ForensicClassTypes) {
                if (fUnit.Children.FirstOrDefault(p => p is PinTreeUnit pinUnit && pinUnit.ContentId == infoKind) == null) {
                    fUnit.Children.Add(
                        new PinTreeUnit(infoKind, fUnit) {
                            Label = PinKindsDefinitions.GetClassLabel(infoKind)
                        }
                    );
                }
            }
        }

    }
}
