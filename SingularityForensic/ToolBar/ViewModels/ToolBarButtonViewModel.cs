using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingularityForensic.ToolBar.ViewModels {
    public class ToolBarButtonViewModel:BindableBase {
        private ICommand _command;
        public ICommand Command {
            get => _command;
            set => SetProperty(ref _command, value);
        }

        private Uri _icon;
        public Uri Icon {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }


        private string  _toolTip;
        public string  ToolTip {
            get => _toolTip;
            set => SetProperty(ref _toolTip, value);
        }

    }
}
