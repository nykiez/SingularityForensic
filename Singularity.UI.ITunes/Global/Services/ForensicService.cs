using Microsoft.Practices.ServiceLocation;
using Singularity.UI.Info.Views;
using Singularity.UI.ITunes.Models;
using Singularity.UI.ITunes.ViewModels;
using System.ComponentModel.Composition;

namespace Singularity.UI.ITunes.Global.Services {
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class ForensicService {
        /// <summary>
        /// 开始对某个案件文件取证;
        /// </summary>
        /// <param name="cFile"></param>
        public void StartForensic(ITunesBackUpCaseFile csFile) {
            var window = new StartForensicWindow();
            var vm = ServiceLocator.Current.GetInstance<ITunesStartForensicWindowViewModel>();

            window.DataContext = vm;
            vm.DeviceFile = csFile;
            vm.CloseRequest += delegate {
                window.Close();
            };

            window.ShowDialog();
            //ITunesBackUpCaseFile cFile
        }
    }
}
