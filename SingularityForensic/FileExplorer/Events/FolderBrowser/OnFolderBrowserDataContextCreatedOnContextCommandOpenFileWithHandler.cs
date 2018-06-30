using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 创建打开方式右键菜单;
    /// </summary>
    [Export(typeof(IFolderBrowserDataContextCreatedEventHandler))]
    public class OnFolderBrowserDataContextCreatedOnContextCommandOpenFileWithHandler : 
        IFolderBrowserDataContextCreatedEventHandler {
        public int Sort => 8;

        public bool IsEnabled => true;

        public void Handle(Contracts.FileExplorer.IFolderBrowserDataContext dataContext) {
            if(dataContext == null) {
                return;
            }

            var vm = dataContext.FolderBrowserViewModel;
            if(vm == null) {
                return;
            }

            var cmi = CommandItemFactory.CreateNew(null,Constants.CommandItemGUID_OpenFileWith);
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_OpenFileWith);
            cmi.Sort = 48;

            var cmiChildren = CreateOpenFileWithCommandItems(vm);
            if (cmiChildren != null) {
                foreach (var cmiChild in cmiChildren) {
                    cmi.AddChild(cmiChild);
                }
            }
            
            var cmiOther = CreateOpenFileWithAnotherProCommandItem(vm);
            if(cmiOther != null) {
                cmi.AddChild(cmiOther);
            }

            vm.AddContextCommand(cmi);
        }

        private static IEnumerable<ICommandItem> CreateOpenFileWithCommandItems(IFolderBrowserViewModel vm) {
            var viewers = ViewerService.GetAllViewers();
            if(viewers == null) {
                yield break;
            }

            foreach (var viewer in viewers) {
                if(viewer == null) {
                    continue;
                }
                yield return CreateOpenFileWithProCommandItem(viewer.Value.viewerName, viewer.Value.path, vm);
            }
        }

        /// <summary>
        /// 创建打开文件的命令;
        /// </summary>
        /// <param name="viewerName"></param>
        /// <param name="viewerPath"></param>
        /// <param name="vm"></param>
        /// <returns></returns>
        private static ICommandItem CreateOpenFileWithProCommandItem(string viewerName,string viewerPath, Contracts.FileExplorer.ViewModels.IFolderBrowserViewModel vm) {
            var comm = CreateOpenFileWithProCommand(viewerPath, vm);
            var cmi = CommandItemFactory.CreateNew(comm);
            cmi.Name = viewerName;
            return cmi;
        }
        
        private static DelegateCommand CreateOpenFileWithProCommand(string viewerPath, Contracts.FileExplorer.ViewModels.IFolderBrowserViewModel vm) {
            var comm = new DelegateCommand(() => {
                OpenFileWithPro(viewerPath, vm);
            });

            vm.SelectedFileChanged += delegate  {
                comm.RaiseCanExecuteChanged();
            };

            return comm;
        }

        /// <summary>
        ///使用指定程序打开视图模型中选定的文件;
        /// </summary>
        /// <param name="viewerPath"></param>
        /// <param name="vm"></param>
        private static void OpenFileWithPro(string viewerPath, Contracts.FileExplorer.ViewModels.IFolderBrowserViewModel vm) {
            if (vm.SelectedFile?.File == null) {
                return;
            }

            var fileName = SaveFileToTemp(vm.SelectedFile.File);
            if (string.IsNullOrEmpty(fileName)) {
                LoggerService.WriteCallerLine($"{nameof(fileName)} can't be null.");
                return;
            }

            if (!File.Exists(fileName)) {
                LoggerService.WriteCallerLine($"The file {fileName} can't be found.");
                return;
            }

            ViewerService.Current.OpenFileWith(fileName,viewerPath);
        }

        /// <summary>
        /// 保存文件到临时目录;
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static string SaveFileToTemp(IFile file) {
            if(file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (string.IsNullOrEmpty(file.Name)) {
                LoggerService.WriteCallerLine($"{nameof(file.Name)} can't be null.");
                return string.Empty;
            }

            var tempFolder = AppService.AppTempFolder;
            var filePath = $"{AppService.AppTempFolder}\\{ file.Name}";
            var fileIndex = 0;

            //若已经存在同名文件,则累加文件后缀序列号,直至找到可用的不同名;
            while (File.Exists(filePath)) {
                filePath = $"{AppService.AppTempFolder}\\{Path.GetFileNameWithoutExtension(file.Name)}~{fileIndex++}{System.IO.Path.GetExtension(file.Name)}";
            }
            
            var inputStream = file.GetInputStream();

            if(inputStream == null || string.IsNullOrEmpty(filePath)) {
                return string.Empty;
            }
            
            using (var fs = File.Create(filePath)) {
                inputStream.CopyTo(fs);
            }
            
            inputStream.Dispose();
            
            return filePath;
        }

        /// <summary>
        /// 其它程序打开命令;
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private static ICommandItem CreateOpenFileWithAnotherProCommandItem(Contracts.FileExplorer.ViewModels.IFolderBrowserViewModel vm) {
            var comm = CreateOpenFileWithAnotherProCommand(vm);
            var cmi = CommandItemFactory.CreateNew(comm);
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_OpenFileWithAnotherPro);
            cmi.Sort = 128;
            return cmi;
        }

        public static DelegateCommand CreateOpenFileWithAnotherProCommand(Contracts.FileExplorer.ViewModels.IFolderBrowserViewModel vm) {
            var comm = new DelegateCommand(() => {
                var viewerPath = DialogService.Current.OpenFile(
                    $"({LanguageService.FindResourceString("Executable")})|*.exe");

                if (string.IsNullOrEmpty(viewerPath)) {
                    return;
                }

                //添加查看器;
                var viewerName = Path.GetFileName(viewerPath);
                ViewerService.Current.AddViewer(viewerName , viewerPath);

                //对所有的视图模型上下文添加命令项;
                var vms = GetAllFolderViewModels();
                if (vms != null) {
                    foreach (var fvm in vms) {
                        var openWithCmi = fvm.ContextCommands?.FirstOrDefault(p => p.GUID == Constants.CommandItemGUID_OpenFileWith);
                        if (openWithCmi == null) {
                            continue;
                        }

                        var cmi = CreateOpenFileWithProCommandItem(viewerName, viewerPath, fvm);
                        openWithCmi.AddChild(cmi);
                    }
                }
                
                //执行查看命令;
                OpenFileWithPro(viewerPath, vm);
            });

            return comm;
        }

        /// <summary>
        /// 获取所有的FolderViewModel;
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<IFolderBrowserViewModel> GetAllFolderViewModels() {
            var docs = DocumentService.MainDocumentService?.CurrentDocuments;
            if (docs == null) {
                yield break;
            }

            foreach (var doc in docs) {
                var vm = doc.GetInstance<Contracts.FileExplorer.ViewModels.IFolderBrowserViewModel>(Contracts.FileExplorer.Constants.DocumentTag_FolderBrowserDataContext);
                if (vm != null) {
                    yield return vm;
                }
            }
        }
    }
    
    
}
