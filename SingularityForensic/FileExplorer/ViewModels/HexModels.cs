using CDFCUIContracts.Abstracts;
using System;
using System.IO;
using static CDFCCultures.Managers.ManagerLocator;
using CDFCUIContracts.Events;
using Prism.Commands;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.App;
using SingularityForensic.Controls.ViewModels;
using System.Windows.Media;
using System.Collections.ObjectModel;
using SingularityForensic.Hex.ViewModels;
using SingularityForensic.Contracts.FileSystem;

namespace SingularityForensic.FileExplorer.ViewModels {
    //十六进制文件查看器视图模型;
    public abstract class FileHexTabViewModel : TabHexStreamEditorViewModel {
        public FileHexTabViewModel(FileBase file) {
            this.File = file;
            //this.Stream = CreateStreamByFile(file);
        }

        private FileBase file;                                     //当前十六进制描述的文件;
        public FileBase File {
            get {
                return file;
            }
            set {

                if (file != value && value != null) {
                    //Stream = CreateStreamByFile(value);
                }
                file = value;
            }
        }
    }

    //十六进制文件查看器试图模型;(针对设备)
    public class DeviceHexTabViewModel : FileHexTabViewModel {
        public DeviceHexTabViewModel(Device device) : base(device) { }
        public override string Header {
            get {
                return LanguageService.Current?.FindResourceString("DeviceTab");
            }
        }
    }

    //十六进制文件查看器试图模型;(针对分区)
    public class PartitionHexTabViewModel : FileHexTabViewModel {
        public PartitionHexTabViewModel(Partition partition) : base(partition) {
            IsFileSystemHex = partition != null;
        }
        public override string Header {
            get {
                return LanguageService.Current?.FindResourceString("PartitionTab");
            }
        }

        public event EventHandler<TEventArgs<long>> FindFsPositionRequired;
        private DelegateCommand _findFSPositionCommand;
        public DelegateCommand FindFSPositionCommand =>
            _findFSPositionCommand ?? (_findFSPositionCommand = new DelegateCommand(
                () => {
                    if (FocusPosition != -1) {
                        FindFsPositionRequired?.Invoke(this, new TEventArgs<long>(FocusPosition));
                    }
                }
            ));
    }

    ////十六进制文件查看器试图模型;(针对内部文件)
    public class InternalFileHexTabViewModel : FileHexTabViewModel {
        public InternalFileHexTabViewModel(FileBase file) : base(file) { }
        public override string Header {
            get {
                return LanguageService.Current?.FindResourceString("File");
            }
        }
    }

    //Tab十六进制流查看器模型;
    public abstract class TabHexStreamEditorViewModel : HexStreamEditorViewModel, IHexDataContext, ITabModel {
        public object Data { get; set; }
        public abstract string Header { get; }
        public ObservableCollection<(long index, long length, Brush background)> CustomBackgroundBlocks {
            get; set;
        } = new ObservableCollection<(long index, long length, Brush background)>();
    }

}
