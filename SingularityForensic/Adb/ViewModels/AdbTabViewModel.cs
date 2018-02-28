using System;
using SingularityForensic.Adb.ViewModels.AdbGrid;
using Cflab.DataTransport.Modules.Transport.Model;
using Prism.Mvvm;
using CDFC.Info.Adb;
using SingularityForensic.Adb.Contracts;

namespace SingularityForensic.Adb.ViewModels {
    public class AdbTabViewModel : BindableBase {
        public AdbTabViewModel(Device device,IInfoModelContainer container) {
            if (device == null)
                throw new ArgumentNullException(nameof(device));
            if (container == null)
                throw new ArgumentNullException(nameof(device));

            this.Container = container;
            this.Device = device;
        }
        public Device Device { get; }
        public IInfoModelContainer Container { get; }
        
        /// <summary>
        /// 当前显示的内容;   
        /// </summary>
        private BindableBase _adbShowingViewModel;
        public BindableBase AdbShowingViewModel {
            get {
                if(_adbShowingViewModel == null) {
                    if(Container is IDefaultPhoneInfoContainer) {
                        if(Container is AdbSingleInfoContainer<Basic, AdbInfoBasicModel>) {
                            _adbShowingViewModel = new AdbBasicPanelViewModel(Container as AdbSingleInfoContainer<Basic, AdbInfoBasicModel>);
                        }
                        else {
                            _adbShowingViewModel = new AdbGridViewModel(Container as IDefaultPhoneInfoContainer);
                        }
                        
                    }
                    
                }
                return _adbShowingViewModel;
            }
        }

        public void Close() {
            if(AdbShowingViewModel is AdbGridViewModel) {
                (AdbShowingViewModel as AdbGridViewModel).Close();
            }
        }
    }

    
}
