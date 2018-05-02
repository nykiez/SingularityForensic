using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 创建打开方式右键菜单;
    /// </summary>
    [Export(typeof(IFolderBrowserViewModelCreatedEventHandler))]
    public class OnFolderBrowserViewModelCreatedOnContextCommandOpenFileWithHandler : 
        IFolderBrowserViewModelCreatedEventHandler {
        public int Sort => 8;

        public bool IsEnabled => true;

        public void Handle(IFolderBrowserViewModel vm) {
            var cmi = CommandItemFactory.CreateNew(null);
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_OpenFileWith);
            var cmiChildren = CreateOpenFileWithCommandItems(vm);
            if (cmiChildren == null) {
                
            }

            foreach (var cmiChild in cmiChildren) {
                cmi.AddChild(cmiChild);
            }
            
        }

        private static IEnumerable<ICommandItem> CreateOpenFileWithCommandItems(IFolderBrowserViewModel vm) {
            return null;
        }

        /// <summary>
        /// 其它程序打开命令;
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private static ICommandItem CreateOpenFileWithAnotherProCommandItem(IFolderBrowserViewModel vm, ICommandItem parentCmi) {
            var comm = new DelegateCommand(() => {
                var viewerFileName = DialogService.Current.OpenFile(
                    $"({LanguageService.FindResourceString("Executable")})|*.exe");

                if (string.IsNullOrEmpty(viewerFileName)) {
                    return;
                }
                
                var regFile = vm.SelectedFile?.File as IRegularFile;

                //using (var stream = regFile.GetStream()) {
                //    ThreadInvoker.BackInvoke(() => {
                //        //WatchRequired?.Invoke(this, new TEventArgs<ViewerProgramMessage>(
                //            //new ViewerProgramMessage(dialog.FileName, regFile.Name, stream)));
                //    });

                //    var proName = IOPathHelper.GetFileNameFromUrl(dialog.FileName);
                //    if (proName != null) {
                //        //ViewerProgramHelper.AddProgram(dialog.FileName, proName);
                //        //LoadViewers();
                //    }
                //}
            });

            var cmi = CommandItemFactory.CreateNew(comm);
            cmi.Sort = 128;
            return cmi;
        }


    }


}
