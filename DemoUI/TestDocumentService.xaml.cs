using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoUI {
    /// <summary>
    /// Interaction logic for TestDocumentExtensionService.xaml
    /// </summary>
    public partial class TestDocumentService : UserControl {
        public TestDocumentService() {
            InitializeComponent();
            this.Loaded += TestDocumentService_Loaded;
            
        }

        private void TestDocumentService_Loaded(object sender, RoutedEventArgs e) {
            RegionHelper.RequestNavigate(
                SingularityForensic.Contracts.MainPage.Constants.MainPageDocumentRegion,
                Constants.DocumentTabsView
            );
            var doc = DocumentService.MainDocumentService.CreateNewDocument();
            doc.Title = "Test Title";
            DocumentService.MainDocumentService.AddDocument(doc);

            var enumDoc = DocumentService.MainDocumentService.CreateNewEnumerableDocument();
            enumDoc.Title = "Enum Doc";

            var innerDoc = enumDoc.CreateNewDocument();
            innerDoc.Title = "Inner Doc";
            innerDoc.UIObject = "313";
            enumDoc.AddDocument(innerDoc);
            DocumentService.MainDocumentService.AddDocument(enumDoc);
        }
    }
}
