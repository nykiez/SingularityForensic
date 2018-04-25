using SingularityForensic.Contracts.Hex;
using System.IO;
using System.Windows.Controls;

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
