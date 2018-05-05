using CDFC.Info.Adb;
using CDFCUIContracts.Abstracts;
using Prism.Commands;
using SingularityForensic.Contracts.App;
using System;
using System.ComponentModel;

namespace SingularityForensic.Adb.ViewModels.AdbViewer {
    public partial class AdbViewerViewModel : BindableBaseTemp {
        public EventHandler Closed;
        private PageModelBase _curPageModel;
        public PageModelBase CurPageModel {
            get {
                if (_curPageModel == null) {
                    _curPageModel = DeviceSelectorViewModel;
                }
                return _curPageModel;
            }
            set {
                SetProperty(ref _curPageModel, value);
                OnPropertyChanged(nameof(CanGoBack));
                GoBackCommand.RaiseCanExecuteChanged();
            }
        }

        private DeviceSelectorViewModel _deviceSelectorViewModel;
        public DeviceSelectorViewModel DeviceSelectorViewModel {
            get {
                if (_deviceSelectorViewModel == null) {
                    _deviceSelectorViewModel = new DeviceSelectorViewModel();
                    _deviceSelectorViewModel.DeviceConfirmed += (sender, e) => {
                        if(e.Device != null) {
                            CurPageModel = InfoCheckerViewModel;
                            InfoCheckerViewModel.Device = e.Device;
                        }
                        else {
                            MsgBoxService.Show("设备不可为空");
                        }
                    };
                }
                return _deviceSelectorViewModel;
            }
        }

        private AdbInfoesCheckedViewModel _infoCheckerViewModel;
        public AdbInfoesCheckedViewModel InfoCheckerViewModel {
            get {
                if(_infoCheckerViewModel == null) {
                    _infoCheckerViewModel = new AdbInfoesCheckedViewModel();
                    _infoCheckerViewModel.AquireDone += (sender, e) => {
                        FullPhoneInfoContainer = new PhoneFullInfoContainer(DeviceSelectorViewModel.SelectedDevice.Device,e);
                        Closed?.Invoke(this, new EventArgs());
                    };
                }
                return _infoCheckerViewModel;
            }
        }
    }

    public partial class AdbViewerViewModel {
        public bool CanGoBack => CurPageModel != DeviceSelectorViewModel;
        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand => _goBackCommand ??
            (_goBackCommand = new DelegateCommand(
                () => {
                    if(CurPageModel == InfoCheckerViewModel) {
                        if (InfoCheckerViewModel.IsAquiring) {
                            MsgBoxService.Show(LanguageService.FindResourceString("WaitUntilAuqiringDone"));
                            return;
                        }
                    }
                    CurPageModel = DeviceSelectorViewModel;
                },
                () => CanGoBack
        ));

        //退出时命令;
        private DelegateCommand<CancelEventArgs> _closingCommand;
        public DelegateCommand<CancelEventArgs> ClosingCommand =>
            _closingCommand ?? (_closingCommand = new DelegateCommand<CancelEventArgs>(
                e => {
                    if(CurPageModel == InfoCheckerViewModel && InfoCheckerViewModel.IsAquiring) {
                        if(MsgBoxService.Show(
                            LanguageService.FindResourceString("ConfirmToExitAdbViewer"),
                            MessageBoxButton.YesNo)
                        == MessageBoxResult.No) {
                            e.Cancel = true;
                        }
                    }
                    InfoCheckerViewModel.Dispose();
                    DeviceSelectorViewModel.Dispose();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            ));

        public PhoneFullInfoContainer FullPhoneInfoContainer { get; private set; }
        ~AdbViewerViewModel() {

        }
    }
}
