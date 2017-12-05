using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCUIContracts.Commands;
using CDFCUIContracts.Models;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using Singularity.Contracts.FileExplorer;
using Singularity.Contracts.FileSystem;
using System;
using System.Collections.ObjectModel;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.FileExplorer.Models {

    //案件文件类型契约;
    //public interface IHaveCaseFile<out TCaseFile> where TCaseFile : ICaseFile {
    //    TCaseFile CaseFile { get; }
    //}

    

    //文件系统节点(针对设备);
    public class FileSystemUnit : TreeUnit {
        public FileSystemUnit(ICaseEvidence itrCFile,ITreeUnit parent, IFileExplorerServiceProvider FsServiceProvider):base(parent) {
            if(FsServiceProvider == null) {
                throw new ArgumentNullException(nameof(FsServiceProvider));
            }

            this.CaseFile = itrCFile;
            Label = FindResourceString("FileSystem");
            this.FsServiceProvider = FsServiceProvider;
        }
        public ICaseEvidence CaseFile { get; }
        public IFileExplorerServiceProvider FsServiceProvider { get; }
        //子文件为分区
        private ObservableCollection<ITreeUnit> _children;
        public override ObservableCollection<ITreeUnit> Children {
            get {
                if (_children == null) {
                    //子节点为分区案件文件节点;
                    _children = new ObservableCollection<ITreeUnit>();
                    if (CaseFile is IHaveCaseFiles itrCFile) {
                        if (itrCFile.InnerCaseFiles != null) {
                            foreach (var cFile in itrCFile.InnerCaseFiles) {
                                if (cFile is PartitionCaseFile pcFile) {
                                    var pUnit = new CaseEvidenceUnit<PartitionCaseFile>(pcFile, this) {
                                        Label = cFile.Name,
                                        //Level = 2,
                                        Icon = IconResources.PartUnitIcon
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
                                                    var fsNodeService = ServiceProvider.Current.GetInstance<ICaseService>();
                                                    fsNodeService?.ShowCaseFileProperty(cFile);
                                                }),
                                                CommandName = FindResourceString("Properties")
                                            },
                                        new CommandItem {
                                            Command = new DelegateCommand(() => {
                                                ServiceProvider.Current.GetInstance<IFSNodeService>()?.ExpandFile(pcFile.Data);
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

    

    
}
