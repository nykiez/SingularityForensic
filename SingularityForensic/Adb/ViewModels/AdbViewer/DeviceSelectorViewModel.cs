using CDFCUIContracts.Abstracts;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using EventLogger;
using CDFCMessageBoxes.MessageBoxes;
using static CDFCCultures.Managers.ManagerLocator;
using Prism.Commands;
using SingularityForensic.Adb.Models.AdbViewer;
using Cflab.DataTransport.Tools.Adb.Devices;
using System.Collections.Generic;
using Cflab.DataTransport.Modules.Transport.Model;
using static CDFCUIContracts.Helpers.ApplicationHelper;
using Cflab.DataTransport;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Adb.ViewModels.AdbViewer {
    public partial class DeviceSelectorViewModel:PageModelBase,IDisposable {
        static DeviceSelectorViewModel() {
            void InitAndTrack() {
                ThreadPool.QueueUserWorkItem(cb => {
                    var res = DeviceManager.Init(err => {
                        Logger.WriteLine($"{nameof(AdbViewerViewModel)}->{err.Code}:{err.Message}");
                    });

                    if (res) {
                        //部署设备检测器;
                        if (_deviceTracker == null) {
                            _deviceTracker = new DeviceTracker();
                            _deviceTracker.BeginTrackDevices(devices => {
                                _trackedDevices = devices;
                                PopTrackDevices();
                                //若静态常量为空,则不设置;
                                if (_devSelectorVM != null) {
                                    _devSelectorVM.PopupDevices(devices);
                                }
                            }, err => {
                                InitAndTrack();
                                RemainingMessageBox.Tell($"{FindResourceString("FailedToDetectAdbDevices")}:" + err.Message);
                            });
                        }
                    }
                });
                
            };
            InitAndTrack();
        }
        
        private static List<Device> _trackedDevices;
        private static void PopTrackDevices() {
            _devSelectorVM?.PopupDevices(_trackedDevices);
        }

        private static DeviceTracker _deviceTracker;
        private static DeviceSelectorViewModel _devSelectorVM;

        public DeviceSelectorViewModel() {
            _devSelectorVM = this;
            PopTrackDevices();
        }
        public void RefreshDevices() {
            IsLoading = true;
            TipWord = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("AdbDeviceRefreshing");
            Application.Current?.Dispatcher.Invoke(() => {
                Devices.Clear();
            });
            var devices = Cflab.DataTransport.DeviceManager.GetDevices(err => {
                Logger.WriteLine($"{nameof(RefreshDevices)}->{err.Code}-{err.Message}");
            });
            IsLoading = false;
            PopupDevices(devices);
            
            //if(res.Type != ResultType.Success || Devices.Count == 0) {
            //    TipWord = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("PleaseConnectToPhone");
            //    Logger.WriteLine($"{nameof(DeviceSelectorViewModel)}->{nameof(RefreshDevices)}:No device or failed,type-{res.Type},message-{res.Message}.");
            //}
            
            RefreshCommand.RaiseCanExecuteChanged();
            ConfirmCommand.RaiseCanExecuteChanged();
        }

        public void PopupDevices(List<Device> devices) {
            AppInvoke(() => {
                Devices.Clear();
                try {
                    if (devices == null || devices.Count == 0) {
                        TipWord = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("PleaseConnectToPhone");
                        return;
                    }
                    devices.ForEach(p => {
                        var newDev = new AdbDeviceModel { Name = p.Disply, Device = p };
                        Devices.Add(newDev);
                    });
                    SelectedDevice = Devices[0];
                    TipWord = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("ChooseDeviceDetected");
                }
                catch(Exception ex) {
                    Logger.WriteLine($"{nameof(DeviceSelectorViewModel)}->{ nameof(PopupDevices)}:{ ex.Message}");
                    RemainingMessageBox.Tell($"{nameof(PopupDevices)}:{ex.Message}");
                }
            });
        }

        public void Dispose() {
            _devSelectorVM = null;
            
        }

        public ObservableCollection<AdbDeviceModel> Devices { get; set; } = new ObservableCollection<AdbDeviceModel>();

        private AdbDeviceModel _selectedDevice;
        public AdbDeviceModel SelectedDevice {
            get {
                if(_selectedDevice == null && Devices.Count != 0) {
                    _selectedDevice = Devices[0];
                }
                return _selectedDevice;
            }
            set {
                SetProperty(ref _selectedDevice, value);
                ConfirmCommand.RaiseCanExecuteChanged();
            }
        }

        private string _tipWord = "Connect To Your Phone";
        public string TipWord {
            get {
                return _tipWord;
            }
            set {
                SetProperty(ref _tipWord, value);
            }
        }

        private bool _isLoading;
        public bool IsLoading {
            get {
                return _isLoading;
            }
            set {
                SetProperty(ref _isLoading, value);
                RefreshCommand.RaiseCanExecuteChanged();
            }
        }
    }
    public partial class DeviceSelectorViewModel {
        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(
                () => {
                    ThreadPool.QueueUserWorkItem(cb => {
                        RefreshDevices();
                    });
                },
                () => !IsLoading
            ));

        public event EventHandler<AdbDeviceModel> DeviceConfirmed;
        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??
            (_confirmCommand = new DelegateCommand(
                () => {
                    DeviceConfirmed?.Invoke(this, SelectedDevice);
                },
                () => !IsLoading && SelectedDevice != null
            ));

        
    }

}
