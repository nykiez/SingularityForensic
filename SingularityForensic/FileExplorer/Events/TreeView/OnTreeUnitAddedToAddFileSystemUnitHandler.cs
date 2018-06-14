using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 案件文件节点加入时加入文件系统节点;
    /// </summary>
    /// <param name="unit"></param>
    [Export(typeof(ITreeUnitAddedEventHandler))]
    class OnTreeUnitAddedAddFileSystemUnitHandler : ITreeUnitAddedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle((ITreeUnit unit, ITreeService treeService) tuple) {
            if (tuple.treeService != Contracts.MainPage.MainTreeService.Current) {
                return;
            }

            if (tuple.unit == null) {
                return;
            }

            if (!(tuple.unit.TypeGuid == Contracts.Casing.Constants.TreeUnitType_CaseEvidence)) {
                return;
            }

            var csEvidence = tuple.unit.GetInstance<ICaseEvidence>(Contracts.Casing.Constants.TreeUnitTag_CaseEvidence);
            if (csEvidence == null) {
                LoggerService.WriteCallerLine($"{nameof(csEvidence)} can't be null.");
                return;
            }

            var fileTuple = FileSystemService.Current.MountedUnits?.FirstOrDefault(p => p.XElem.GetXElemValue(nameof(ICaseEvidence.EvidenceGUID)) == csEvidence.EvidenceGUID);
            if (fileTuple == null) {
                LoggerService.WriteCallerLine($"{nameof(fileTuple)} can't be null.");
                return;
            }

            var fsUnit = TreeUnitFactory.CreateNew(Contracts.FileExplorer.Constants.TreeUnitType_FileSystem);
            fsUnit.Icon = IconResources.FileSystemIcon;
            fsUnit.Label = LanguageService.Current?.FindResourceString(Constants.TreeUnitLabel_FileSystem);
            fsUnit.SetInstance(fileTuple.File, Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);

            var bufferLength = 10;
            var bufferStacks = new AddUnitStack[bufferLength];
            var index = 0;
            //递归添加子节点：
            void TraverseAddChildren(ITreeUnit tUnit, IHaveFileCollection haveCollection) {
                foreach (var file in haveCollection.Children) {
                    if (!(file is IHaveFileCollection cHaveCollection)) {
                        continue;
                    }

                    //忽略备份文件夹;
                    if (file is IDirectory dir && (dir.IsBack || dir.IsLocalBackUp)) {
                        continue;
                    }

                    var cUnit = TreeUnitFactory.CreateNew(Contracts.FileExplorer.Constants.TreeUnitType_InnerFile);
                    cUnit.Label = file.Name;
                    cUnit.SetInstance(file, Contracts.FileExplorer.Constants.TreeUnitTag_InnerFile);

                    if (file is IDirectory) {
                        cUnit.Icon = IconResources.DirectoryUnitIcon;
                    }
                    else if (file is IPartition part) {
                        cUnit.Label = FileExtensions.GetPartFixAndName(part);
                        cUnit.Icon = IconResources.PartUnitIcon;
                    }

                    TraverseAddChildren(cUnit, cHaveCollection);

                    if (index == bufferLength) {
                        ThreadInvoker.UIInvoke(() => {
                            foreach (var unit in bufferStacks) {
                                Contracts.MainPage.MainTreeService.Current.AddUnit(unit.ParentUnit, unit.ChildUnit);
                            }
                        });
                        index = 0;
                        Thread.Sleep(1);
                    }

                    bufferStacks[index].ParentUnit = tUnit;
                    bufferStacks[index].ChildUnit = cUnit;

                    index++;
                    //tUnit.Children.Add(cUnit);
                }
            }

            if (fileTuple.File is IHaveFileCollection haveCollection2) {
                TraverseAddChildren(fsUnit, haveCollection2);
                ThreadInvoker.UIInvoke(() => {
                    for (int i = 0; i < index; i++) {
                        Contracts.MainPage.MainTreeService.Current.AddUnit(bufferStacks[i].ParentUnit, bufferStacks[i].ChildUnit);
                    }
                });
            }

            Contracts.MainPage.MainTreeService.Current.AddUnit(tuple.unit, fsUnit);
        }
    }

    struct AddUnitStack {
        public ITreeUnit ParentUnit { get; set; }
        public ITreeUnit ChildUnit { get; set; }
    }
}
