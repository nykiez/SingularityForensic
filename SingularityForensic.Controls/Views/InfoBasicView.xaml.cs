using System.Windows.Controls;

namespace SingularityForensic.Controls.Info.Views {
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
