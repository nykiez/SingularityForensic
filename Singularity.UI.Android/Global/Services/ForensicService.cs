using Microsoft.Practices.ServiceLocation;
using Singularity.UI.FileSystem.Models;
using Singularity.UI.Info.Android.ViewModels;
using Singularity.UI.Info.Views;
using System.ComponentModel.Composition;

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
        

    }
}
