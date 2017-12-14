using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCUIContracts.Commands;
using CDFCUIContracts.Models;
using Singularity.Contracts.FileExplorer;
using Singularity.Contracts.FileSystem;
using System;
using System.Collections.ObjectModel;

namespace Singularity.UI.FileExplorer.Models {
    //文件树形存储节点;
    public class StorageTreeUnit : TreeUnit,IStorageTreeUnit {
        /// <summary>
        /// 通过文件创建树形节点;
        /// </summary>
        /// <param name="file">文件本体</param>
        /// <returns></returns>
        public StorageTreeUnit(IFile file, ITreeUnit parent, IFileExplorerServiceProvider fsProvider) : base(parent) {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (fsProvider == null) {
                throw new ArgumentNullException(nameof(fsProvider));
            }

            FSProvider = fsProvider;
            File = file;
            Label = file.Name;

            if (File is Partition) {
                Icon = IconResources.PartUnitIcon;
            }
            else if (File is Device) {
                Icon = IconResources.DeviceUnitIcon;
            }
            else if (File.Type == FileType.Directory) {
                Icon = IconResources.DirectoryUnitIcon;
            }
            else {
                Icon = IconResources.UnknownIcon;
            }
        }

        public IFile File { get; private set; }

        public IFileExplorerServiceProvider FSProvider { get; }

        private ObservableCollection<ITreeUnit> children;
        public override ObservableCollection<ITreeUnit> Children {
            get {
                if (children == null) {
                    children = new ObservableCollection<ITreeUnit>();
                    try {
                        if (File is IIterableFile) {
                            if (File is IIterableFile itrFile) {
                                foreach (var p in itrFile.Children) {
                                    //目录子节点;
                                    if (p.Type == FileType.Directory) {
                                        var dir = p as CDFC.Parse.Abstracts.Directory;
                                        if (!dir.IsBackFile() && !dir.IsBackUpFile()) {
                                            children.Add(new StorageTreeUnit(dir, this, FSProvider));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex) {
                        EventLogger.Logger.WriteLine($"{nameof(StorageTreeUnit)}->{nameof(Children)}-{ex.Message}");
                    }
                }

                return children;
            }
            set {
                children = value;
            }
        }

        public override ObservableCollection<ICommandItem> ContextCommands { get; set; } = new ObservableCollection<ICommandItem>();
    }
}
