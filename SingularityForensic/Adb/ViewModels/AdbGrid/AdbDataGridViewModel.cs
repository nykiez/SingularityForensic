using System;
using System.Collections.ObjectModel;
using System.Linq;
using EventLogger;
using CDFCUIContracts.Events;
using CDFCMessageBoxes.MessageBoxes;
using System.IO;
using Ookii.Dialogs.Wpf;
using CDFCCultures.Helpers;
using Cflab.DataTransport.Modules.Transport.Model;
using Prism.Commands;
using Prism.Mvvm;
using CDFC.Info.Adb;
using SingularityForensic.Adb.Contracts;
using SingularityForensic.Adb.Helpers;
using SingularityForensic.Contracts.Info;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Adb.ViewModels.AdbGrid {
    //Adb文件网格视图模型;
    public partial class AdbDataGridViewModel:BindableBase {
        public AdbDataGridViewModel(IDefaultPhoneInfoContainer container) {
            this.Container = container;
        }
        public MInfoType InfoType => Container.InfoType;
        public IDefaultPhoneInfoContainer Container { get; }

        private ObservableCollection<IAdbModel> _infoes;
        public ObservableCollection<IAdbModel> Infoes {
            get {
                if (_infoes == null) {
                    _infoes = new ObservableCollection<IAdbModel>();
                    try {
                        if (MInfoTypeHelper.GetMInfoTypeBox(InfoType) == MInfoTypeBox.AdbFile) {
                            var fileContainer = Container as IAdbMultiInfoContainer<IInfo,InfoModel>;
                            fileContainer.Infoes.ForEach(p => {
                                switch (InfoType) {
                                    case MInfoType.Audio:
                                        _infoes.Add(new AdbFileAudioModel(p as Audio));
                                        break;
                                    case MInfoType.Video:
                                        _infoes.Add(new AdbFileVideoModel(p as Video));
                                        break;
                                    case MInfoType.Image:
                                        _infoes.Add(new AdbFileImageModel(p as Image));
                                        break;
                                }
                            });
                        }
                        else {
                            var infoContainer = Container as IAdbMultiInfoContainer<IInfo,InfoModel>;

                            infoContainer.Infoes.ForEach(p => {
                                switch (InfoType) {
                                    case MInfoType.Calllog:
                                        _infoes.Add(new AdbCalllogModel(p as CallLog));
                                        break;
                                    case MInfoType.GPS:
                                        _infoes.Add(new AdbGpsModel(p as Gps));
                                        break;
                                    case MInfoType.Package:
                                        _infoes.Add(new AdbPackageModel(p as Package));
                                        break;
                                    case MInfoType.Contact:
                                        _infoes.Add(new AdbContactModel(p as Contact));
                                        break;
                                    case MInfoType.Sms:
                                        _infoes.Add(new AdbSmsModel(p as Sms,null));
                                        break;
                                }

                            });
                        }
                    }
                    catch (Exception ex) {
                        Logger.WriteLine($"{nameof(AdbDataGridViewModel)}->{nameof(Infoes)}|:{ex.Message}");
                    }
                }
                return _infoes;
            }
        }

        public event EventHandler<TEventArgs<IAdbModel>> SelectedAdbModelChanged;
        private IAdbModel _selectedAdbModel;
        public IAdbModel SelectedAdbModel {
            get {
                return _selectedAdbModel;
            }
            set {
                SetProperty(ref _selectedAdbModel, value);
                
                SelectedAdbModelChanged?.Invoke(this, new TEventArgs<IAdbModel>(_selectedAdbModel));
                RecOrCopyCommand.RaiseCanExecuteChanged();
            }
        }

        public MInfoTypeBox InfoBoxType => MInfoTypeHelper.GetMInfoTypeBox(InfoType);

        public Type RowType {
            get {
                return Infoes.FirstOrDefault()?.GetType();
            }
        }
    }
    public partial class AdbDataGridViewModel {
        private DelegateCommand _recOrCopyCommand;
        public DelegateCommand RecOrCopyCommand => _recOrCopyCommand ??
            (_recOrCopyCommand = new DelegateCommand(
                () => {
                    var dialog = new VistaSaveFileDialog();
                    var oriUrl = (SelectedAdbModel as IAdbInfoModel<UrlInfo>).Info.Url;
                    dialog.FileName = IOPathHelper.GetFileNameFromUrl(oriUrl);

                    if(dialog.ShowDialog() == true) {
                        try {
                            ProgressMessageBox msg = new ProgressMessageBox();
                            msg.WindowTitle = LanguageService.Current?.FindResourceString("AdbFileBeingCopied");
                            msg.Word = $"{LanguageService.FindResourceString("AdbFileBeingCopied")}:{IOPathHelper.GetFileNameFromUrl(oriUrl)}";
                            var succeed = false;
                            
                            msg.DoWork += (sender, e) => {
                                FileStream fs = null;
                                FileStream desFs = null;
                                try {
                                    desFs = File.Create(dialog.FileName);
                                    fs = File.OpenRead($"{ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase.Path}/AdbDevices/{Container.Parent.Device.Serial}/{(SelectedAdbModel as IAdbInfoModel<UrlInfo>).Info.Url}");
                                    var buffer = new byte[81920];
                                    var read = 0;
                                    long readSize = 0;

                                    while((read = fs.Read(buffer,0,buffer.Length)) != 0) {
                                        desFs.Write(buffer, 0, read);

                                        readSize += read;
                                        var per = (int) (readSize * 100 / fs.Length);
                                        msg.ReportProgress(per<=100?per:100);
                                    }
                                    succeed = true;
                                }
                                catch(Exception ex) {
                                    Logger.WriteLine($"{nameof(AdbDataGridViewModel)}->{nameof(RecOrCopyCommand)}:{ex.Message}");
                                    CDFCUIContracts.Helpers.ApplicationHelper.AppInvoke(() => {
                                        RemainingMessageBox.Tell(ex.Message);
                                    });
                                    succeed = false;
                                }
                                finally {
                                    fs?.Close();
                                    desFs?.Close();
                                }
                            };
                            msg.RunWorkerCompleted += (sender, e) => {
                                if (succeed) {
                                    CDFCMessageBox.Show($"导出文件{IOPathHelper.GetFileNameFromUrl(oriUrl)}成功!");
                                }
                            };
                            msg.RunWorkerCompleted += delegate {

                            };
                            msg.ShowDialog();
                        }
                        catch (Exception ex) {
                            RemainingMessageBox.Tell($"{ex.Message}");
                        }
                    }
                },
                () => SelectedAdbModel is IAdbInfoModel<UrlInfo>
            ));
    }
}
