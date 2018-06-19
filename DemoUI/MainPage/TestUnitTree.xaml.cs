using SingularityForensic.Contracts.Helpers;
using System.Windows.Controls;

namespace DemoUI.MainPage {
    /// <summary>
    /// Interaction logic for TestUnitTree.xaml
    /// </summary>
    public partial class TestUnitTree : UserControl {
        public TestUnitTree() {
            InitializeComponent();
            this.Loaded += delegate {
                RegionHelper.RequestNavigate(SingularityForensic.Contracts.MainPage.Constants.NodeTreeRegion, SingularityForensic.Contracts.MainPage.Constants.UnitTreeView);

                SingularityForensic.Contracts.Casing.CaseService.Current.LoadCase("D://SingularitySolution/SingularityShell/bin/Debug/Cases/案件名/案件名/案件名.sfproj");
                //var unit = TreeUnitFactory.CreateNew(string.Empty);
                //unit.Label = "Unit1";
                //MainTreeService.Current.AddUnit(null,unit);

                //var cmi = CommandItemFactory.CreateNew(null, string.Empty);
                //cmi.Name = "dada";

                //MainTreeService.Current.AddContextCommand(cmi);
            };

            
        }
    }
}
