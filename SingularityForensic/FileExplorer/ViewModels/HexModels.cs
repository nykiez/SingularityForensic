using CDFCUIContracts.Abstracts;
using System;
using CDFCUIContracts.Events;
using Prism.Commands;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.App;
using System.Windows.Media;
using System.Collections.ObjectModel;
using SingularityForensic.Hex.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Common;
using System.Collections.Generic;

namespace SingularityForensic.FileExplorer.ViewModels {
    ////十六进制文件查看器视图模型;
    //public abstract class FileHexTabViewModel : TabHexStreamEditorViewModel {
    //    public FileHexTabViewModel(FileBase file) {
    //        this.File = file;
    //        //this.Stream = CreateStreamByFile(file);
    //    }

    //    private FileBase file;                                     //当前十六进制描述的文件;
    //    public FileBase File {
    //        get {
    //            return file;
    //        }
    //        set {

    //            if (file != value && value != null) {
    //                //Stream = CreateStreamByFile(value);
    //            }
    //            file = value;
    //        }
    //    }
    //}

    ////十六进制文件查看器试图模型;(针对设备)
    //public class DeviceHexTabViewModel : FileHexTabViewModel {
    //    public DeviceHexTabViewModel(Device device) : base(device) { }
    //}

    ////十六进制文件查看器试图模型;(针对分区)
    //public class PartitionHexTabViewModel : FileHexTabViewModel {
    //    public PartitionHexTabViewModel(Partition partition) : base(partition) {
    //        IsFileSystemHex = partition != null;
    //    }

    //    public event EventHandler<TEventArgs<long>> FindFsPositionRequired;
    //    private DelegateCommand _findFSPositionCommand;
    //    public DelegateCommand FindFSPositionCommand =>
    //        _findFSPositionCommand ?? (_findFSPositionCommand = new DelegateCommand(
    //            () => {
    //                if (FocusPosition != -1) {
    //                    FindFsPositionRequired?.Invoke(this, new TEventArgs<long>(FocusPosition));
    //                }
    //            }
    //        ));
    //}

    //////十六进制文件查看器试图模型;(针对内部文件)
    //public class InternalFileHexTabViewModel : FileHexTabViewModel {
    //    public InternalFileHexTabViewModel(FileBase file) : base(file) { }
        
    //}

    //Tab十六进制流查看器模型;
    public class TabHexStreamEditorViewModel :
        HexStreamEditorViewModel,
        IHexDataContext,
        IDocumentTab {
        public object Tag { get; set; }
        public string Title { get; }
        public ObservableCollection<(long index, long length, Brush background)> CustomBackgroundBlocks {
            get; set;
        } = new ObservableCollection<(long index, long length, Brush background)>();

        public List<CommandItem> Commands {get;set;}

        public object UIObject { get; set; }
    }

}
