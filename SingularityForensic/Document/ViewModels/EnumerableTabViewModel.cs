using Prism.Mvvm;
using SingularityForensic.Contracts.Document;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Document.ViewModels {
    [Export]
    public class EnumerableTabViewModel:BindableBase {
        /// <summary>
        /// 主区域视图;
        /// </summary>
        private object _mainView;
        public object MainView {
            get => _mainView;
            set => SetProperty(ref _mainView, value);
        }

        public ObservableCollection<IDocumentBase> DocumentTabs { get; set; } = 
            new ObservableCollection<IDocumentBase>();
        
        private IDocumentBase _selectedDocument;
        public IDocumentBase SelectedDocument {
            get => _selectedDocument;
            set {
                SetProperty(ref _selectedDocument, value);
                SelectedDocumentChanged?.Invoke(this, _selectedDocument);
            } 
        }

        public event EventHandler<IDocumentBase> SelectedDocumentChanged;
    }
}
