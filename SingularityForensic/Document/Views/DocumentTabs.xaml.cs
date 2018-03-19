using SingularityForensic.ViewModels.Modules.MainPage.ViewModels;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace SingularityForensic.Document.Views {
    /// <summary>
    /// Interaction logic for FolderBrowser.xaml
    /// </summary>
    [Export(nameof(DocumentTabs))]
    public partial class DocumentTabs : UserControl {
        public DocumentTabs() {
            InitializeComponent();
        }
    }
}
