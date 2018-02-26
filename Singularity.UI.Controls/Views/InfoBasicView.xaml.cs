using System.Windows.Controls;

namespace Singularity.UI.Info.Views {
    /// <summary>
    /// Interaction logic for InfoDetailView.xaml
    /// </summary>
    //Export(nameof(InfoBasicView))]
    public partial class InfoBasicView : UserControl {
        public InfoBasicView() {
            InitializeComponent();
        }

        //[Import]
        //InfoBasicViewModel VM {
        //    set {
        //        this.DataContext = value;
        //    }
        //}
    }
}
