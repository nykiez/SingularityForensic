namespace Singularity.UI.MessageBoxes.Models {
    ////左方的树形存储节点;
    //public class StorageTreeUnit {
    //    /// <summary>
    //    /// 通过文件创建树形节点;
    //    /// </summary>
    //    /// <param name="file">文件本体</param>
    //    /// <returns></returns>
    //    public static StorageTreeUnit CreateIFileUnit(IFile file) {
    //        StorageTreeUnit unit = new StorageTreeUnit();
    //        if (file.FileType == FileType.BlockDeviceFile) {
    //            var blockDevice = file as BlockDeviceFile;
    //            if (blockDevice != null) {
    //                unit.Label = blockDevice.Name;
    //            }
    //            if (file is Partition) {
    //                unit.UnitType = TreeUnitType.Partition;
    //            }
    //            else if (file is Device) {
    //                unit.UnitType = TreeUnitType.Device;
    //            }
    //        }
    //        else if (file.FileType == FileType.Directory) {
    //            unit.UnitType = TreeUnitType.Directory;
    //            var directory = file as Directory;
    //            if (directory != null) {
    //                unit.Label = directory.Name;
    //            }
    //        }
    //        else {
    //            unit.UnitType = TreeUnitType.Unknown;
    //        }
    //        //if(unit.Label == string.Empty) {

    //        //}
    //        unit.File = file;
    //        return unit;
    //    }

    //    public IFile File { get; private set; }
    //    public TreeUnitType UnitType { get; private set; }
    //    public string Label { get; private set; }

    //    private ObservableCollection<StorageTreeUnit> children;
    //    public ObservableCollection<StorageTreeUnit> Children {
    //        get {
    //            if (children == null) {
    //                children = new ObservableCollection<StorageTreeUnit>();
    //                if (UnitType == TreeUnitType.Device) {
    //                    var itrFile = File as IIterableFile;
    //                    if (itrFile != null) {
    //                        itrFile.Children.ForEach(p => {
    //                            children.Add(CreateIFileUnit(p));
    //                        });
    //                    }
    //                }
    //                else if (UnitType == TreeUnitType.Directory || UnitType == TreeUnitType.Partition) {
    //                    if (UnitType == TreeUnitType.Partition) {

    //                    }
    //                    var itrFile = File as IIterableFile;
    //                    if (itrFile != null) {
    //                        itrFile.Children.ForEach(p => {
    //                            var itrSelf = p as IIterableFile;
    //                            if (itrSelf != null) {
    //                                if (!itrSelf.Children.Contains(p) && !itrSelf.Children.Contains(p.Parent) && itrSelf.Name != ".." && itrSelf.Name != ".") {
    //                                    var direct = CreateIFileUnit(p);
    //                                    direct.DirectoryLevel = DirectoryLevel != null ? DirectoryLevel + 1 : 0;
    //                                    children.Add(direct);
    //                                }

    //                            }

    //                        });
    //                    }
    //                }
    //            }

    //            return children;
    //        }
    //        set {
    //            children = value;
    //        }
    //    }

    //    public int? DirectoryLevel { get; set; }
    //}

    ////树形节点类型;
    //public enum TreeUnitType {
    //    Unknown,                //未知类型;
    //    Case,                   //案件类型;
    //    Device,                 //设备/镜像类型;
    //    Partition,              //分区类型;
    //    Directory               //目录类型;
    //}
}
