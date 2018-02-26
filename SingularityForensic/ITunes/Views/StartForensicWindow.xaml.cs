using CDFCControls.Controls;
using Singularity.UI.ITunes.ViewModels;
using System.ComponentModel.Composition;

namespace Singularity.UI.ITunes.Views {
    /// <summary>
    /// Interaction logic for StartForensicWindow.xaml
    /// </summary>
    public partial class StartForensicWindow : CorneredWindow {
        [ImportingConstructor]
        public StartForensicWindow(ITunesStartForensicWindowViewModel vm) {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
