using Cflab.DataTransport.Modules.Transport.Model;
using EventLogger;
using Singularity.Contracts.TabControl;
using Singularity.UI.AdbViewer.Helpers;
using Singularity.UI.AdbViewer.ViewModels;
using Singularity.UI.AdbViewer.ViewModels.AdbGrid;
using Singularity.UI.AdbViewer.Views;
using Singularity.UI.AdbViewer.Views.AdbGrid;
using Singularity.UI.Info.ViewModels;
using Singularity.UI.Info.Views;
using System;
using System.Text;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.AdbViewer.TabModels {
    public class AdbTabModel : TabModel {
        public AdbTabModel(AdbTabViewModel vm) {
            Content = new AdbTab { DataContext = vm };
            this.AdbTabViewModel = vm;
            Title = $"{AdbTabViewModel.Device?.Disply}-{MInfoTypeHelper.GetInfoTypeWord(AdbTabViewModel.Container.InfoType)}"; 
        }
        
        public AdbTabViewModel AdbTabViewModel { get; }
        protected override bool ConfirmToClose() {
            AdbTabViewModel.Close();
            return true;
        }
    }

    public class AdbBasicTabModel : TabModel {
        public AdbBasicTabModel(Basic basic) {

            var vm = new InfoBasicViewModel();

            try {
                var sb = new StringBuilder();
                foreach (var prop in basic.GetType().GetProperties()) {
                    sb.AppendLine(FindResourceString($"AdbBasic{prop.Name}") +
                        $":{ prop.GetValue(basic)}");
                }
                vm.BasicText = sb.ToString();
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(AdbBasicTabModel)}:{ex.Message}");
                vm.BasicText = ex.Message;
            }
            Content = new InfoBasicView() {
                DataContext = vm
            };
            this.Basic = basic;
        }
        
        public Basic Basic { get; }
    }

    public class AdbMainTabModel : TabModel {
        public AdbMainTabModel(InfoMainViewModel vm) {
            Content = new InfoMain {
                DataContext = vm
            };
        }
        
    }

    public class AdbGridTabModel : TabModel {
        public AdbGridTabModel(AdbGridViewModel vm) {
            Content = new AdbGrid {
                DataContext = vm
            };
        }
        
    }
}
