using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SingularityForensic.Document.Views {
    /// <summary>
    /// Interaction logic for FolderBrowser.xaml
    /// </summary>
    [Export(Contracts.Document.Constants.DocumentTabsView)]
    public partial class DocumentTabs : UserControl {
        public DocumentTabs() {
            InitializeComponent();
        }
    }
}
