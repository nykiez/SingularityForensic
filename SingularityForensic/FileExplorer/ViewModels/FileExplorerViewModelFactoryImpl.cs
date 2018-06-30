using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.ViewModel {
    [Export(typeof(IFileExplorerDataContextFactory))]
    public class FileExplorerViewModelFactoryImpl : IFileExplorerDataContextFactory {
        [ImportingConstructor]
        public FileExplorerViewModelFactoryImpl([ImportMany]IEnumerable<IFolderBrowserDataContextCreatedEventHandler> folderBrowserViewModelCreatedEventHandlers) {
            _folderBrowserViewModelCreatedEventHandlers = folderBrowserViewModelCreatedEventHandlers;
        }

        private readonly IEnumerable<IFolderBrowserDataContextCreatedEventHandler> _folderBrowserViewModelCreatedEventHandlers;

        public IFolderBrowserDataContext CreateFolderBrowserDataContext(IHaveFileCollection haveFileCollection) {
            var dataContext = new FolderBrowserDataContext(haveFileCollection);
            try {
                PubEventHelper.PublishEventToHandlers(dataContext as IFolderBrowserDataContext, _folderBrowserViewModelCreatedEventHandlers);
                PubEventHelper.GetEvent<FolderBrowserDataContextCreatedEvent>().Publish(dataContext);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }

            //try {
            //    dataContext.Initialize();
            //}
            //catch(Exception ex) {
            //    LoggerService.WriteException(ex);
            //}
            
            return dataContext;
        }
        public IPartitionsBrowserViewModel CreatePartitionsBrowserViewModel(IDevice device) {
            var vm = new PartitionsBrowserViewModel(device);
            PubEventHelper.GetEvent<PartitionsBrowserViewModelCreatedEvent>().Publish(vm);
            return vm;
        }

        public INavMenuDataContext CreateNavMenuDataContext() => new NavMenuDataContext();

        /// <summary>
        /// 获取所有的FolderViewModel;
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IFolderBrowserDataContext> GetAllFolderBrowserDataContext() {
            var docs = DocumentService.MainDocumentService?.CurrentDocuments;
            if (docs == null) {
                yield break;
            }

            foreach (var doc in docs) {
                var vm = doc.GetInstance<IFolderBrowserDataContext>(Contracts.FileExplorer.Constants.DocumentTag_FolderBrowserDataContext);
                if (vm != null) {
                    yield return vm;
                }
            }
        }
    }
}
