using SingularityForensic.ViewModels.Modules.MainPage.ViewModels;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SingularityForensic.Modules.MainPage.Views {
    /// <summary>
    /// Interaction logic for FolderBrowser.xaml
    /// </summary>
    [Export(nameof(DocumentTab))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DocumentTab : UserControl {
        public DocumentTab() {
            InitializeComponent();
        }
        [Import]                        //浏览器tab页;
        private DocumentTabsViewModel _browserTabViewModel {
            set {
                DataContext = value;
            }
        }
    }
}
