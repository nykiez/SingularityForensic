using CDFCUIContracts.Abstracts;
using System;
using System.Collections.ObjectModel;
using System.IO;
using EventLogger;
using CDFCMessageBoxes.MessageBoxes;
using Singularity.Previewers;
using CDFC.Info.Infrastructure;
using Prism.Mvvm;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Linq;
using Cflab.DataTransport.Modules.Transport.Model;
using CDFC.Info.Adb;
using Singularity.UI.Case;

namespace Singularity.UI.AdbViewer.ViewModels.AdbGrid {
    [Export]
    public partial class AdbGridViewModel:BindableBase {
        public AdbGridViewModel(IDefaultPhoneInfoContainer container) {
            this.Container = container;
            SelectedTabModel = InfoDetailTabModel;

            TabViewModels.Add(InfoDetailTabModel);
            SelectedTabModel = InfoDetailTabModel;

            if (MInfoTypeHelper.GetMInfoTypeBox(container.InfoType) == MInfoTypeBox.AdbFile) {
                TabViewModels.Add(FileHexTabViewModel);
                if (container.InfoType == MInfoType.Image) {
                    TabViewModels.Add(AdbPreviewViewModel);
                }
            }
        }

        public void LoadInfoes(IEnumerable<InfoModel> infoModels) {
            AdbDataGridViewModel.Infoes.Clear();
            if (infoModels != null && infoModels.Count() == 0) {
                Logger.WriteLine($"{nameof(AdbGridViewModel)}->{nameof(LoadInfoes)}:{nameof(infoModels)}:can't be null.");
            }

            foreach (var item in infoModels) {
                switch (item.InfoType) {
                    case MInfoType.Audio:
                        AdbDataGridViewModel.Infoes.Add(item as AdbFileAudioModel);
                        break;
                    case MInfoType.Video:
                        AdbDataGridViewModel.Infoes.Add(item as AdbFileVideoModel);
                        break;
                    case MInfoType.Image:
                        AdbDataGridViewModel.Infoes.Add(item as AdbFileImageModel);
                        break;
                }
            }
        }
        
        public IDefaultPhoneInfoContainer Container { get; }
        
        //网格视图模型;
        private AdbDataGridViewModel _adbDataGridViewModel;
        public AdbDataGridViewModel AdbDataGridViewModel {
            get {
                if(_adbDataGridViewModel == null) {
                    var adgVM = new AdbDataGridViewModel(Container);
                    adgVM.SelectedAdbModelChanged += (sender, e) => {
                        if (e.Target is IAdbInfoModel<IInfo>) {
                            InfoDetailTabModel.AdbModel = e.Target;
                        }
                        else if (e.Target is IAdbInfoModel<IInfo>) {
                            //InfoDetailTabModel.AdbModel = (e.Target as IAdbInfoModel<IInfo>).;
                        }

                        //若为Adb文件,则将显示十六进制;
                        if(e.Target is IAdbInfoModel<UrlInfo> urlModel) {
                            FileHexTabViewModel.Stream?.Close();
                            FileHexTabViewModel.Stream = null;
                           
                            var path = $"{SingularityCase.Current.Path}/AdbDevices/{Container.Parent.Device.Serial}/{urlModel.Info.Url}";
                            try {
                                var fs = File.OpenRead(path);
                                FileHexTabViewModel.Stream = fs;
                            }
                            catch (Exception ex) {
                                Logger.WriteLine($"{nameof(AdbGridViewModel)}->{nameof(AdbDataGridViewModel)}:{ex.Message}");
                                RemainingMessageBox.Tell(ex.Message);
                            }

                            //若为图像,则显示预览图;
                            if (e.Target is IAdbInfoModel<Image> imageModel) {
                                try {
                                    AdbPreviewViewModel.Previewer?.Dispose();
                                    AdbPreviewViewModel.Previewer = new OutsideInPreviewer(path);
                                }
                                catch (Exception ex) {
                                    Logger.WriteLine($"{nameof(AdbGridViewModel)}->{nameof(AdbDataGridViewModel)}(Previewing Image):{ex.Message}");
                                }
                            }
                        }
                    };


                    _adbDataGridViewModel = adgVM;
                }
                return _adbDataGridViewModel;
            }
        }

        public void Close() {
            try {
                FileHexTabViewModel.Stream?.Close();
                FileHexTabViewModel.Stream = null;
            }
            catch(Exception ex) {
                Logger.WriteLine($"{nameof(AdbGridViewModel)}->{nameof(Close)}:{ex.Message}");
                RemainingMessageBox.Tell(ex.Message);
            }
        }
    }

    public partial class AdbGridViewModel {
        public ObservableCollection<ITabModel> TabViewModels { get; set; } = new ObservableCollection<ITabModel>();
        //详细Tab视图模型;
        private AdbInfoDetailTabViewModel _infoDetailTabModel;
        public AdbInfoDetailTabViewModel InfoDetailTabModel {
            get {
                if(_infoDetailTabModel == null) {
                    _infoDetailTabModel = new AdbInfoDetailTabViewModel() { InfoType = Container.InfoType };
                }
                return _infoDetailTabModel;
            }
        }

        //十六进制视图模型;
        private AdbFileHexStreamTabViewModel _fileHexTabViewModel;
        public AdbFileHexStreamTabViewModel FileHexTabViewModel {
            get {
                if(_fileHexTabViewModel == null) {
                    _fileHexTabViewModel = new AdbFileHexStreamTabViewModel();
                }
                return _fileHexTabViewModel;
            }
        }
        
        //预览视图模型;
        private AdbPreviewViewModel _adbPreviewViewModel;
        public AdbPreviewViewModel AdbPreviewViewModel {
            get {
                if(_adbPreviewViewModel == null) {
                    _adbPreviewViewModel = new AdbPreviewViewModel();
                }
                return _adbPreviewViewModel;
            }
        }

        //当前选定的Tab模型;
        private ITabModel _selectedTabModel;
        public ITabModel SelectedTabModel {
            get {
                return _selectedTabModel;
            }
            set {
                SetProperty(ref _selectedTabModel, value);
            }
        }
    }
}
