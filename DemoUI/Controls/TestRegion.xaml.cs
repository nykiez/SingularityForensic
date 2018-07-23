using Prism.Regions;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

namespace DemoUI.Controls {
    /// <summary>
    /// Interaction logic for TestRegion.xaml
    /// </summary>
    public partial class TestRegion : UserControl {
        public TestRegion() {
            InitializeComponent();
            //RegionHelper.RegisterViewWithRegion(TestRegionName,typeof(SEntity));
            //RegionHelper.RegisterViewWithRegion(TestRegionName, typeof(SEntity));
            
            //RegionHelper.RequestNavigate(TestRegionName, TestViewName);
        }


        private const string TestRegionName = nameof(TestRegionName);
        private const string TestViewName = nameof(TestViewName);
        private const string TestViewName2 = nameof(TestViewName2);

        //[Export(TestViewName,typeof(SEntity))]
        [Export(TestViewName,typeof(SEntity))]
        private class SEntity {
            public SEntity() {

            }
            ~SEntity(){

            }
        }

        [Export(TestViewName2,typeof(SEntity2))]
        private class SEntity2 {
            public SEntity2() {

            }
            ~SEntity2() {

            }
        }

        private UIElement ss = null;
        private void Button_Click(object sender, RoutedEventArgs e) {
            bd.Children.Clear();
            var cc = new ContentControl();
            RegionManager.SetRegionName(cc, TestRegionName);
            bd.Children.Add(cc);
            
            if(ss == null) {
                ss = bd.Children[0];
            }
            else {
                bd.Children.Add(ss);
            }
            //RegionHelper.RequestNavigate(TestRegionName, TestViewName);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            bd.Children.Clear();
        }

        private void NavToSEntity2_Click(object sender, RoutedEventArgs e) {
            RegionHelper.RequestNavigate(TestRegionName, TestViewName2);
        }

        private void NavToSEntity_Click(object sender, RoutedEventArgs e) {
            RegionHelper.RequestNavigate(TestRegionName, TestViewName);
        }
    }
}
