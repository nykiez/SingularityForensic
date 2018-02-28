using System.Windows.Controls;

namespace SingularityForensic.Adb.Views {
    /// <summary>
    /// Interaction logic for InfoMain.xaml
    /// </summary>
    //[Export(nameof(InfoMain))]
    public partial class InfoMain : UserControl {
        public InfoMain() {
            InitializeComponent();
        }

        //[Import]
        //InfoMainViewModel VM {
        //    set {
        //        this.DataContext = value;
        //    }
        //}
    }
}
