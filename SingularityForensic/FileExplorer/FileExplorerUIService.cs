using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.MainPage.Events;
using SingularityForensic.Contracts.TreeView;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.FileExplorer {
    /// <summary>
    /// 文件系统资源管理器UI响应单位;
    /// </summary>
    [Export]
    public class FileExplorerUIService {
        public void Initialize() {
            _uiServiceForDevice.Initialize();
            _uiServiceForPartition.Initialize();
            RegisterEvents();
        }
        
        [Import]
        private FileExplorerUIServiceForDevice _uiServiceForDevice;

        [Import]
        private FileExplorerUIServiceForPartition _uiServiceForPartition;

        private void RegisterEvents() {
            //加入文件系统节点响应(左键);
            PubEventHelper.GetEvent<TreeNodeClickEvent>().Subscribe(OnTreeUnitClickOnFileSystemUnit);
            //为设备案件文件节点加入文件系统子节点;
            PubEventHelper.GetEvent<TreeNodeAddedEvent>().Subscribe(OnTreeUnitAdded);
        }

        /// <summary>
        /// 点击了文件系统节点时响应;
        /// </summary>
        /// <param name="unit"></param>
        private void OnTreeUnitClickOnFileSystemUnit(TreeUnit unit) {
            if (unit == null) {
                return;
            }

            var file = unit.GetIntance<FileBase>(Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
            if (unit.TypeGuid == Constants.FileSystemTreeUnit && file != null) {
                FileExplorerUIHelper.AddFileToDocument(file);
            }
        }
        
        /// <summary>
        /// 案件文件节点加入时加入文件系统节点;
        /// </summary>
        /// <param name="unit"></param>
        private void OnTreeUnitAdded(TreeUnit unit) {
            if (unit == null) {
                return;
            }

            var csFile = unit.GetIntance<CaseEvidence>(Contracts.FileExplorer.Constants.TreeUnitTag_CaseFile);
            if (!(unit.TypeGuid == Contracts.Casing.Constants.CaseEvidenceUnit && csFile != null)) {
                return;
            }

            var fileTuple = FSService.Current.MountedFiles?.FirstOrDefault(p => p.xElem.GetXElemValue(nameof(CaseEvidence.EvidenceGUID)) == csFile.EvidenceGUID);
            if (fileTuple == null) {
                LoggerService.WriteCallerLine($"{nameof(fileTuple)} can't be null.");
                return;
            }

            var fsUnit = new TreeUnit(Constants.FileSystemTreeUnit) {
                Icon = IconResources.FileSystemIcon,
                Label = LanguageService.Current?.FindResourceString("FileSystem")
            };
            fsUnit.SetInstance(fileTuple.Value.file, Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);

            var partIndex = 0;
            //递归添加子节点：
            void TraverseAddChildren(TreeUnit tUnit, IHaveFileCollection haveCollection) {
                foreach (var file in haveCollection.Children) {
                    if (!(file is IHaveFileCollection cHaveCollection)) {
                        continue;
                    }

                    if (file is Directory dir
                            && (dir.IsBack || dir.IsLocalBackUp)) {
                        continue;
                    }

                    var cUnit = new TreeUnit(Contracts.FileExplorer.Constants.TreeUnitType_InnerFile) {
                        Label = file.Name
                    };
                    
                    if (file is Directory) {
                        cUnit.Icon = IconResources.DirectoryUnitIcon;
                    }
                    else {
                        cUnit.Icon = IconResources.RegFileUnitIcon;
                    }

                    TraverseAddChildren(cUnit, cHaveCollection);
                    cUnit.MoveToUnit(tUnit);
                    partIndex++;
                    if (file is Partition) {
                        cUnit.Label = $"分区{partIndex}";
                    }
                }
            }

            if (fileTuple.Value.file is IHaveFileCollection haveCollection2) {
                TraverseAddChildren(fsUnit, haveCollection2);
            }

            fsUnit.MoveToUnit(unit);
        }
    }

    public static class FileExplorerUIHelper {
        /// <summary>
        /// 根据流文件创建一个十六进制Tab;
        /// </summary>
        /// <param name="blockedStream"></param>
        /// <returns></returns>
        internal static (IDocument doc, IHexDataContext hexDataContext)?
            GetBlockedStreamHexDocument(IBlockedStream blockedStream) {

            var mainDocService = DocumentService.MainDocumentService;
            if (mainDocService == null) {
                LoggerService.WriteCallerLine($"{nameof(mainDocService)} can't be null.");
                return null;
            }

            var hexDeviceDoc = mainDocService.AddNewDocument();
            hexDeviceDoc.SetInstance(blockedStream,
                Contracts.FileExplorer.Constants.HexDataContextTag_BlockedStream);

            var hexService = ServiceProvider.Current.GetInstance<IHexService>();
            if (hexService == null) {
                LoggerService.WriteCallerLine($"{nameof(hexService)} can't be null.");
                return null;
            }

            var hexDataContext = hexService.CreateNewHexDataContext(blockedStream?.BaseStream);

            hexDeviceDoc.UIObject = hexDataContext.UIObject;
            return (hexDeviceDoc, hexDataContext);
        }

        /// <summary>
        /// 添加文件(设备/分区)显示到文档区域中;
        /// </summary>
        /// <param name="device"></param>
        internal static void AddFileToDocument(FileBase file) {
            //检查文档区域是否已经被添加了相关文件;
            if (CheckTagAddedToDocument(file)) {
                return;
            }

            var mainDocService = DocumentService.MainDocumentService;
            if (mainDocService == null) {
                LoggerService.WriteCallerLine($"{nameof(mainDocService)} can't be null.");
                return;
            }

            var enumDoc = mainDocService.AddNewEnumerableDocument();
            enumDoc.Title = file.Name;
            enumDoc.SetInstance(file, Constants.DocumentTag_File);
            mainDocService.AddDocument(enumDoc);
            mainDocService.SelectedDocument = enumDoc;
        }

        /// <summary>
        /// 查找文档区域是否已经添加了File相关文档;
        /// </summary>
        /// <param name="tag"></param>
        internal static bool CheckTagAddedToDocument(FileBase file) {
            var mainDocService = DocumentService.MainDocumentService;
            if (mainDocService == null) {
                LoggerService.WriteCallerLine($"{nameof(mainDocService)} can't be null.");
                return true;
            }

            var doc = mainDocService.CurrentDocuments.FirstOrDefault(p =>
            p.GetIntance<FileBase>(Constants.DocumentTag_File) == file);

            if (doc != null) {
                mainDocService.SelectedDocument = doc;
                return true;
            }

            return false;
        }
    }
}
