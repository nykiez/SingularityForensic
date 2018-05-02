using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.TreeView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Drive.ViewModels {
    [Export]
    class DriveItemsWindowViewModel:BindableBase {
        public DriveItemsWindowViewModel() {

        }
        
        /// <summary>
        /// 初始化;
        /// </summary>
        /// <param name="reset">是否重置</param>
        public void Initialize(bool reset = false) {
            if(_comObject == null) {
                InitilizeCore();
            }
            else if (reset) {
                InitilizeCore();
            }
        }
        
        //初始化核心;
        private void InitilizeCore() {
            IsLoading = true;

            ThreadInvoker.BackInvoke(() => {
                try {
                    _comObject = ComObject.Current;
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                    return;
                }

                var units = GetUnitsFromObject();

                ThreadInvoker.UIInvoke(() => {
                    DriveUnits.AddRange(units);
                });
                IsLoading = false;
            });
        }

        /// <summary>
        /// 根据ComObject获得Units;
        /// </summary>
        private IEnumerable<ITreeUnit> GetUnitsFromObject() {
            List<ITreeUnit> units = new List<ITreeUnit>();

            //各种检查为空;
            if (_comObject == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(_comObject)} can't be null.");
                return Enumerable.Empty<ITreeUnit>();
            }

            if (_comObject.LocalHdds == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(_comObject.LocalHdds)} can't be null.");
                return Enumerable.Empty<ITreeUnit>();
            }

            //遍历添加子节点;
            foreach (var hdd in _comObject.LocalHdds) {
                var hddUnit = TreeUnitFactory.CreateNew(Constants.DriveType_LocalHDD);
                hddUnit.Label = hdd.SerialNumber;

                hddUnit.SetInstance(hdd, Constants.TreeUnitTag_LocalHDD);
                if (hdd.Volumes == null) {
                    LoggerService.Current?.WriteCallerLine($"{nameof(hdd.Volumes)} can't be null.");
                    continue;
                }

                foreach (var volume in hdd.Volumes) {
                    var volUnit = TreeUnitFactory.CreateNew(Constants.DriveType_LocalVolume);
                    volUnit.Label = $"{volume.Sign}:";
                    volUnit.SetInstance(volume, Constants.TreeUnitTag_LocalVolume);
                    hddUnit.Children.Add(volUnit);
                }

                units.Add(hddUnit);
            }

            return units;
        }

        private ComObject _comObject;
        public InteractionRequest<Notification> CloseRequest { get; private set; } = new InteractionRequest<Notification>();

        private ITreeUnit _selectedUnit;
        public ITreeUnit SelectedUnit {
            get => _selectedUnit;
            set => SetProperty(ref _selectedUnit, value);
        }

        public ObservableCollection<ITreeUnit> DriveUnits { get; set; } = new ObservableCollection<ITreeUnit>();

        //当前算选定的设备;
        public (string driveType,object drive)? SelectedDriveTuple { get; private set; }

        private bool _isLoading;
        public bool IsLoading {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        //加载文字;
        private string _loadingText;
        public string LoadingText {
            get => _loadingText;
            set => SetProperty(ref _loadingText, value);
        }

        //是否选择了确定;
        private bool _confirmed;
        /// <summary>
        /// 确认命令;
        /// </summary>
        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??
            (_confirmCommand = new DelegateCommand(
                () => {
                    if(SelectedUnit.TypeGuid == Constants.DriveType_LocalHDD) {
                        SelectedDriveTuple = (SelectedUnit.TypeGuid, SelectedUnit.GetIntance<LocalHDD>(Constants.TreeUnitTag_LocalHDD));
                    }
                    else if(SelectedUnit.TypeGuid == Constants.TreeUnitTag_LocalVolume) {
                        SelectedDriveTuple = (SelectedUnit.TypeGuid, SelectedUnit.GetIntance<LocalVolume>(Constants.TreeUnitTag_LocalVolume));
                    }
                    
                    _confirmed = true;
                    CloseRequest.Raise(new Notification());
                },
                () => SelectedUnit != null).
            ObservesProperty(() => SelectedUnit));

        //窗体已经关闭的命令;
        private DelegateCommand _closedCommand;
        public DelegateCommand ClosedCommand => _closedCommand ??
            (_closedCommand = new DelegateCommand(
                () => {
                    if (!_confirmed) {
                        SelectedDriveTuple = null;
                    }
                    _confirmed = false;
                }
            ));


        /// <summary>
        /// 视图显示后开始初始化;
        /// </summary>
        private DelegateCommand _renderedCommand;
        public DelegateCommand RenderedCommand => _renderedCommand ??
            (_renderedCommand = new DelegateCommand(
                () => {
                    Initialize();
                }
            ));

        ~DriveItemsWindowViewModel(){
            _comObject?.Dispose();
            _comObject = null;
        }


    }
}
