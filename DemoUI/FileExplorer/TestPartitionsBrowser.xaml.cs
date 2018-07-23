using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using System.Linq;
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
                
                
                SingularityForensic.Contracts.Casing.CaseService.Current.LoadCase("E:\\Cases\\001\\001\\001.sfproj");



                //"J://Z0176-809.dd"
                //"E://anli/FAT32.img"
                //var cs = SingularityForensic.Contracts.Casing.CaseService.Current.CreateNewCase();
                //foreach (var evi in SingularityForensic.Contracts.Casing.CaseService.Current.CurrentCase.CaseEvidences.ToList()) {
                //    SingularityForensic.Contracts.Casing.CaseService.Current.CurrentCase.RemoveCaseEvidence(evi);
                //}

                //SingularityForensic.Contracts.Imaging.ImgService.Current.AddImg("E://anli/FAT32.img");
                var file = FileSystemService.Current.MountedUnits.First().File;

                //var file = FileSystemService.Current.MountStream(System.IO.File.OpenRead(), "mmp", null, null);
                var unit = TreeUnitFactory.CreateNew(SingularityForensic.Contracts.FileExplorer.Constants.TreeUnitType_FileSystem);
                unit.SetInstance(file, SingularityForensic.Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
                CommonEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
                CommonEventHelper.PublishEventToHandlers((unit, MainTreeService.Current), GenericServiceStaticInstances<ITreeUnitSelectedChangedEventHandler>.Currents);
                CommonEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
                CommonEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));

            };
            
        }
    }
}
