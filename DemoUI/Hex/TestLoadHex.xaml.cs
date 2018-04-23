using SingularityForensic.Contracts.Hex;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace DemoUI.Hex {
    /// <summary>
    /// Interaction logic for TestHexLoaded.xaml
    /// </summary>
    public partial class TestLoadHex : UserControl {
        public TestLoadHex() {
            InitializeComponent();
            this.Loaded += delegate {
                var dataContext = HexService.Current.CreateNewHexDataContext(File.OpenRead("E://anli/FAT32.img"));
                HexService.Current.LoadHexDataContext(dataContext);
                cControl.Content = dataContext.UIObject;
            };
            
        }
    }
}
