using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer {
    class FolderBrowserDataContext: IFolderBrowserDataContext {
        public FolderBrowserDataContext(IHaveFileCollection haveFileCollection) {
            FolderBrowserViewModel = new FolderBrowserViewModel(haveFileCollection);
            //var vm = FileExplorerDataContextFactory.CreateFolderBrowserDataContext(haveFileCollection);

            var folderBrowser = ViewProvider.CreateView(Constants.FolderBrowserView, FolderBrowserViewModel);
            var folderBrowserUIObject = UIObjectProviderFactory.CreateNew(folderBrowser);
            StackGrid.AddChild(folderBrowserUIObject,
                new GridChildLength(new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)));
        }

        public IFolderBrowserViewModel FolderBrowserViewModel { get; }

        private IStackGrid<IUIObjectProvider> _stackGrid;
        public IStackGrid<IUIObjectProvider> StackGrid {
            get {
                if(_stackGrid == null) {
                    _stackGrid = StackGridFactory.CreateNew<IUIObjectProvider>();
                    _stackGrid.Orientation = System.Windows.Controls.Orientation.Vertical;
                }
                return _stackGrid;
            }
        }

        public object UIObject => StackGrid.UIObject;

        public TInstance GetInstance<TInstance>(string extName) {
            throw new NotImplementedException();
        }

        public void SetInstance<TInstance>(TInstance instance, string extName) {
            throw new NotImplementedException();
        }
    }
}
