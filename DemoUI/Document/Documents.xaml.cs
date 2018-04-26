using SingularityForensic.Contracts.Helpers;
using System.Windows.Controls;

namespace DemoUI.Document {
    /// <summary>
    /// Interaction logic for Documents.xaml
    /// </summary>
    public partial class Documents : UserControl {
        public Documents() {
            InitializeComponent();
            this.Loaded += delegate {
                RegionHelper.RequestNavigate(
                    SingularityForensic.Contracts.MainPage.Constants.MainPageDocumentRegion,
                    SingularityForensic.Contracts.Document.Constants.DocumentTabsView
                );
            };
            
        }
    }
}
