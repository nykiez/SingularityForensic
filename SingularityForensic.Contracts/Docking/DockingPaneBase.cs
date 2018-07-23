using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Docking;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Contracts.Docking {
    public abstract class DockingPaneBase : IDockingPane {
        public abstract string InitPaneGroupGUID { get; }

        public abstract string RegionName { get; }

        public abstract string GUID { get; }

        public virtual bool DisposeWhenClosed { get ; set ; }

        private string _header;
        public virtual string Header {
            get => _header;
            set {
                if(_header ==  value) {
                    return;
                }
                _header = value;
                HeaderChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string DefaultlViewName { get ; set ; }
        
        public virtual bool CanUserClose { get ; set ; }

        public virtual Visibility PaneHeaderVisibility { get ; set; }

        private bool _isHidden;

        public event EventHandler HeaderChanged;
        public event EventHandler IsHiddenChanged;

        public virtual bool IsHidden {
            get => _isHidden;
            set {
                if(_isHidden == value) {
                    return;
                }
                _isHidden = true;
                IsHiddenChanged?.Invoke(this, EventArgs.Empty);
                //try {
                //    CommonEventHelper.GetEvent<DockingPaneIsHiddenChangedEvent>().Publish(this);
                //    CommonEventHelper.PublishEventToHandlers<IDockingPaneIsHiddenChangedEventHandler,IDockingPane>(this);
                //}
                //catch(Exception ex) {
                //    LoggerService.WriteException(ex);
                //}
            }
        }



        public virtual double InitialWidth => double.NaN;

        public virtual double InitialHeight => double.NaN;
    }
}
