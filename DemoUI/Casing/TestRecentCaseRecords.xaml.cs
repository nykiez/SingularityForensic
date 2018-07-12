using Prism.Regions;
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

namespace DemoUI.Casing {
    /// <summary>
    /// Interaction logic for TestRecentCaseRecords.xaml
    /// </summary>
    public partial class TestRecentCaseRecords : UserControl {
        public TestRecentCaseRecords() {
            InitializeComponent();
            this.Loaded += delegate {
                RegionHelper.RequestNavigate("CsMainRegion", SingularityForensic.Contracts.Casing.Constants.RecentCaseRecordsView);
                //RegionHelper.RegisterViewWithRegion("CsMainRegion",typeof(SingularityForensic.Casing.Views.RecentCaseRecords));
            };
        }
    }
}
