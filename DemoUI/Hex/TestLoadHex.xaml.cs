using SingularityForensic.Contracts.Hex;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace DemoUI.Hex {
    /// <summary>
    /// Interaction logic for TestHexLoaded.xaml
    /// </summary>
    public partial class TestLoadHex : UserControl {
        public TestLoadHex() {
            InitializeComponent();
            this.Loaded += delegate {
                _dataContext = HexService.Current.CreateNewHexDataContext(File.OpenRead("E://anli/FAT32.img"));
                HexService.Current.LoadHexDataContext(_dataContext);
                cControl.Content = _dataContext.UIObject;
            };
            
            
        }

        private IHexDataContext _dataContext;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e) {
            _dataContext.CustomBackgroundBlocks.Add(CustomBackgroundBlockFactory.CreateNewBackgroundBlock(1, 1024, Brushes.AliceBlue));
            _dataContext.UpdateCustomBackgroundBlocks();
        }
    }
}
