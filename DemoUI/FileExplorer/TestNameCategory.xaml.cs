using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using System.Windows;
using System.Windows.Controls;

namespace DemoUI.FileExplorer {
    /// <summary>
    /// Interaction logic for TestNameCategory.xaml
    /// </summary>
    public partial class TestNameCategory : UserControl {
        public TestNameCategory() {
            InitializeComponent();
            this.Loaded += TestNameCategory_Loaded;
        }

        private void TestNameCategory_Loaded(object sender, RoutedEventArgs e) {
            RegionHelper.RequestNavigate(
                    SingularityForensic.Contracts.MainPage.Constants.MainPageDocumentRegion,
                    SingularityForensic.Contracts.Document.Constants.DocumentTabsView
            );
            var file = FileSystemService.Current.MountStream(System.IO.File.OpenRead("E://anli/FAT32.img"), "mmp", null, null);
            var unit = TreeUnitFactory.CreateNew(SingularityForensic.Contracts.FileExplorer.Constants.TreeUnitType_FileSystem);
            unit.SetInstance(file, SingularityForensic.Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
            CommonEventHelper.PublishEventToHandlers((unit, MainTreeService.Current), GenericServiceStaticInstances<ITreeUnitSelectedChangedEventHandler>.Currents);
            CommonEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var file = DialogService.Current.OpenFile();
            if (string.IsNullOrEmpty(file)) {
                return;
            }

            //NameCategoryService.LoadDescriptorsFromFile("E://anli/File Type Categories.txt");
            NameCategoryService.LoadDescriptorsFromFile(file);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            NameCategoryService.LoadDescriptorsFromFile("E://anli/1.txt");
        }
    }
}
