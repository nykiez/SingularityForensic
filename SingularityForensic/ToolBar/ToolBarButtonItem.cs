using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.ToolBar;
using SingularityForensic.ToolBar.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace SingularityForensic.ToolBar {
    public class ToolBarButtonItem : IToolBarButtonItem {
        public ToolBarButtonItem(ICommand command,string guid) {
            this.Command = command;
            this.GUID = guid;
            InitializeViewModel();
            InitializeView();
        }

        public ICommand Command { get; }

        public Uri Icon {
            get => _vm.Icon;
            set => _vm.Icon = value;
        }
        public string ToolTip {
            get => _vm.ToolTip;
            set => _vm.ToolTip = value;
        }

        public int Sort { get; set; }

        public string GUID { get; }

        private void InitializeViewModel() {
            _vm = new ToolBarButtonViewModel {
                Command = Command
            };
        }

        private void InitializeView() {
            if (_uiObject != null) {
                return;
            }
            _uiObject = ViewProvider.CreateView(Constants.ToolBarButtonView, _vm);
            
        }
        
        private ToolBarButtonViewModel _vm;
        private object _uiObject;
        public object UIObject => _uiObject;
    }
}
