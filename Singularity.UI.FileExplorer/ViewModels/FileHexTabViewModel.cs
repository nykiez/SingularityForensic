using CDFCUIContracts.Abstracts;
using System;
using System.IO;
using CDFC.Parse.Contracts;
using CDFC.Parse.Abstracts;
using static CDFC.Parse.IO.StreamExtensions;
using static CDFCCultures.Managers.ManagerLocator;
using CDFCUIContracts.Events;
using Prism.Commands;
using Singularity.UI.Controls.ViewModels;
using Singularity.Contracts.Hex;

namespace Singularity.UI.FileExplorer.ViewModels {
    //十六进制文件查看器视图模型;
    public abstract class FileHexTabViewModel : TabHexStreamEditorViewModel {
        public FileHexTabViewModel(IFile file) : base(CreateStreamByFile(file)) {
            this.File = file;
        }

        private IFile file;                                     //当前十六进制描述的文件;
        public IFile File {
            get {
                return file;
            }
            set {
                
                if (file != value && value != null) {
                    Stream = CreateStreamByFile(value);
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
                return FindResourceString("DeviceTab");
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
                return FindResourceString("PartitionTab");
            }
        }
        
        public event EventHandler<TEventArgs<long>> FindFsPositionRequired;
        private DelegateCommand _findFSPositionCommand;
        public DelegateCommand FindFSPositionCommand =>
            _findFSPositionCommand ?? (_findFSPositionCommand = new DelegateCommand(
                () => {
                    if(FocusPosition != -1) {
                        FindFsPositionRequired?.Invoke(this,new TEventArgs<long>(FocusPosition));
                    }
                }
            ));
    }

    ////十六进制文件查看器试图模型;(针对内部文件)
    public class InternalFileHexTabViewModel:FileHexTabViewModel {
        public InternalFileHexTabViewModel(IFile file) : base(file) { }
        public override string Header {
            get {
                return FindResourceString("File");
            }
        }
    }
    
    //Tab十六进制流查看器模型;
    public abstract class TabHexStreamEditorViewModel : HexStreamEditorViewModel, IHexDataContext, ITabModel {
        public TabHexStreamEditorViewModel(Stream stream) : base(stream) {
        }

        public abstract string Header { get; }
    }

}
