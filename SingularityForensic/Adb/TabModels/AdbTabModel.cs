using Cflab.DataTransport.Modules.Transport.Model;
using EventLogger;
using SingularityForensic.Adb.Helpers;
using SingularityForensic.Adb.ViewModels;
using SingularityForensic.Adb.ViewModels.AdbGrid;
using SingularityForensic.Adb.Views;
using SingularityForensic.Adb.Views.AdbGrid;
using SingularityForensic.Controls.Info.ViewModels;
using SingularityForensic.Controls.Info.Views;
using System;
using System.Text;
using static CDFCCultures.Managers.ManagerLocator;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Common;
using System.Collections.Generic;

namespace SingularityForensic.Adb.TabModels {
    public class AdbTabModel : IDocumentTab {
        public AdbTabModel(AdbTabViewModel vm) {
            _adbTab = new AdbTab { DataContext = vm };
            this.AdbTabViewModel = vm;
            
            _title = $"{AdbTabViewModel.Device?.Disply}-{MInfoTypeHelper.GetInfoTypeWord(AdbTabViewModel.Container.InfoType)}"; 
        }
        
        public AdbTabViewModel AdbTabViewModel { get; }

        private string _title;
        public string Title => _title;

        public List<CommandItem> Commands => null;

        private AdbTab _adbTab;
        public object UIObject => _adbTab ;
    }

    public class AdbBasicTabModel : IDocumentTab {
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
            _view = new InfoBasicView() {
                DataContext = vm
            };
            this.Basic = basic;
        }
        
        public Basic Basic { get; }

        private string _title;
        public string Title => _title;

        public List<CommandItem> Commands => null;

        private InfoBasicView _view;
        public object UIObject => _view;
    }

    public class AdbMainTabModel : IDocumentTab {
        public AdbMainTabModel(InfoMainViewModel vm) {
            UIObject = new InfoMain {
                DataContext = vm
            };
        }

        public string Title => string.Empty;

        public List<CommandItem> Commands => null;

        public object UIObject { get; }
    }

    public class AdbGridTabModel : IDocumentTab {
        public AdbGridTabModel(AdbGridViewModel vm) {
            UIObject = new AdbGrid {
                DataContext = vm
            };
        }

        public string Title => string.Empty;

        public List<CommandItem> Commands => null;

        public object UIObject { get; }
    }
}
