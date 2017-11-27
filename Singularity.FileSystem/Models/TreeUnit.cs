using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCUIContracts.Commands;
using CDFCUIContracts.Models;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Singularity.Interfaces;
using Singularity.UI.Case.Contracts;
using Singularity.UI.Case.Global.Services;
using Singularity.UI.Case.Models;
using Singularity.UI.FileSystem.Global.Services;
using Singularity.UI.FileSystem.Interfaces;
using Singularity.UI.FileSystem.Resources;
using System;
using System.Collections.ObjectModel;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.FileSystem.Models {

    //案件文件类型契约;
    //public interface IHaveCaseFile<out TCaseFile> where TCaseFile : ICaseFile {
    //    TCaseFile CaseFile { get; }
    //}

    

    //文件系统节点(针对设备);
    public class FileSystemUnit : TreeUnit {
        public FileSystemUnit(ICaseFile itrCFile,ITreeUnit parent,IFileSystemServiceProvider FsServiceProvider):base(parent) {
            if(FsServiceProvider == null) {
                throw new ArgumentNullException(nameof(FsServiceProvider));
            }

            this.CaseFile = itrCFile;
            Label = FindResourceString("FileSystem");
            this.FsServiceProvider = FsServiceProvider;
        }
        public ICaseFile CaseFile { get; }
        public IFileSystemServiceProvider FsServiceProvider { get; }
        //子文件为分区
        private ObservableCollection<ITreeUnit> _children;
        public override ObservableCollection<ITreeUnit> Children {
            get {
                if (_children == null) {
                    //子节点为分区案件文件节点;
                    _children = new ObservableCollection<ITreeUnit>();
                    if (CaseFile is IHaveGroup<ICaseFile> itrCFile) {
                        if (itrCFile.Members != null) {
                            foreach (var cFile in itrCFile.Members) {
                                if (cFile is PartitionCaseFile pcFile) {
                                    var pUnit = new CaseFileUnit<PartitionCaseFile>(pcFile, this) {
                                        Label = cFile.Name,
                                        Level = 2,
                                        Icon = IconSources.PartUnitIcon
                                    };
                                    var children = pUnit.Children ?? (pUnit.Children = new ObservableCollection<ITreeUnit>());
                                    foreach (var file in pcFile.Data.Children) {
                                        if (file is Directory dir && !dir.IsBackFile() && !dir.IsBackUpFile()) {
                                            children.Add(new StorageTreeUnit(file, pUnit,FsServiceProvider));
                                        }
                                    }
                                    pUnit.ContextCommands = new ObservableCollection<ICommandItem> {
                                        new CommandItem {
                                                Command = new DelegateCommand(() => {
                                                    var fsNodeService = ServiceLocator.Current.GetInstance<ICaseService>();
                                                    fsNodeService?.ShowCaseFileProperty(cFile);
                                                }),
                                                CommandName = FindResourceString("Properties")
                                            },
                                        new CommandItem {
                                            Command = new DelegateCommand(() => {
                                                ServiceLocator.Current.GetInstance<IFSNodeService>()?.ExpandFile(pcFile.Data);
                                            }),
                                            CommandName = FindResourceString("ExploreRecursively")
                                        },
                                        //未实现;
                                        new CommandItem {
                                            CommandName = FindResourceString("RecoverFileSystemLog"),
                                            Command = new DelegateCommand(() =>{ },() => false)
                                        },
                                        new CommandItem {
                                            CommandName = FindResourceString("RecoverQQAndWechat"),
                                            Command = new DelegateCommand(() =>{ },() => false)
                                        },
                                        new CommandItem {
                                            CommandName = FindResourceString("RecoverSMSOrContancts"),
                                            Command = new DelegateCommand(() =>{ },() => false)
                                }
                                    };
                                    _children.Add(pUnit);
                                }
                            }
                        }
                    }


                }
                return _children;
            }
            set => _children = value;
        }
        public override ObservableCollection<ICommandItem> ContextCommands { get; set; } = new ObservableCollection<ICommandItem>();
    }


    //文件树形存储节点;
    public class StorageTreeUnit : TreeUnit {
        /// <summary>
        /// 通过文件创建树形节点;
        /// </summary>
        /// <param name="file">文件本体</param>
        /// <returns></returns>
        public StorageTreeUnit(IFile file,TreeUnit parent,IFileSystemServiceProvider fsProvider):base(parent) {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if(fsProvider == null) {
                throw new ArgumentNullException(nameof(fsProvider));
            }

            FSProvider = fsProvider;
            File = file;
            Label = file.Name;

            if (File is Partition) {
                Icon = IconSources.PartUnitIcon;
            }
            else if (File is Device) {
                Icon = IconSources.DeviceUnitIcon;
            }
            else if (File.FileType == FileType.Directory) {
                Icon = IconSources.DirectoryUnitIcon;
            }
            else {
                Icon = IconSources.UnknownIcon;
            }
        }

        public IFile File { get; private set; }
        
        public IFileSystemServiceProvider FSProvider { get; }

        private ObservableCollection<ITreeUnit> children;
        public override ObservableCollection<ITreeUnit> Children {
            get {
                if (children == null) {
                    children = new ObservableCollection<ITreeUnit>();
                    try {
                        if (File is IIterableFile) {
                            if (File is IIterableFile itrFile) {
                                itrFile.Children.ForEach(p => {
                                    //目录子节点;
                                    if (p.FileType == FileType.Directory) {
                                        var dir = p as CDFC.Parse.Abstracts.Directory;
                                        if (!dir.IsBackFile() && !dir.IsBackUpFile()) {
                                            children.Add(new StorageTreeUnit(dir, this,FSProvider));
                                        }
                                    }
                                });
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
