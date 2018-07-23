using Prism.Mvvm;
using SingularityForensic.Contracts.Docking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.MainPage.Models {
    class PaneModel:BindableBase {
        public PaneModel(IDockingPane pane) {
            this.Pane = pane ?? throw new ArgumentNullException(nameof(pane));
            pane.HeaderChanged += (sender,e) => RaisePropertyChanged(nameof(Header));
            pane.IsHiddenChanged += (sender, e) => RaisePropertyChanged(nameof(IsHidden));
            
        }

        public IDockingPane Pane { get; }
        
        public string Header {
            get => Pane.Header;
            set {
                Pane.Header = value;
                RaisePropertyChanged(nameof(Header));
            }
        }
        
        private bool _isHidden;
        public bool IsHidden {
            get => _isHidden;
            set => SetProperty(ref _isHidden, value);
        }

    }
}
