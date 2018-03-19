using System;
using System.Collections.Generic;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Controls.FileExplorer.Views;

namespace SingularityForensic.FileExplorer.ViewModels {
    public class FileBrowserTabModel : IDocumentTab,IDisposable {
        public FileBrowserTabModel(FileBrowserViewModel vm){
            UIObject = new FileBrowser() { DataContext = vm };
            Title = vm.Header;
            this.FileBrowserViewModel = vm;
        }
        
        public FileBrowserViewModel FileBrowserViewModel { get; }

        public string Title { get; }

        public List<CommandItem> Commands => null;

        public object UIObject { get; }

        
        public void Dispose() {
            FileBrowserViewModel.Dispose();
        }

    }
}
