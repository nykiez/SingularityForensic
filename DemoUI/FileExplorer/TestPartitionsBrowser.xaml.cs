using SingularityForensic.Contracts.Helpers;
using System.Windows.Controls;

namespace DemoUI.FileExplorer {
    /// <summary>
    /// Interaction logic for TestPartitionsBrowser.xaml
    /// </summary>
    public partial class TestPartitionsBrowser : UserControl {
        public TestPartitionsBrowser() {
            InitializeComponent();
            this.Loaded += delegate {
                RegionHelper.RequestNavigate(
                    SingularityForensic.Contracts.MainPage.Constants.MainPageDocumentRegion,
                    SingularityForensic.Contracts.Document.Constants.DocumentTabsView
                );
                //var file = FileSystemService.Current.MountStream(System.IO.File.OpenRead("E://anli/Fat32_Test.img"), "mmp", null, null);
                //var unit = TreeUnitFactory.CreateNew(SingularityForensic.FileExplorer.Constants.TreeUnitGUID_FileSystem);
                //unit.SetInstance(file, SingularityForensic.Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
                //PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
                //PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
                //PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
            };
            
        }
    }
}
