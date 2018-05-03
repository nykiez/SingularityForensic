using MahApps.Metro.Controls;
using System.ComponentModel.Composition;
using SingularityForensic.Contracts.Shell;
using System.Windows;

namespace SingularityForensic.Shell.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(Constants.ShellView, typeof(FrameworkElement))]
    public partial class Shell : MetroWindow {
        public Shell() {
            
            InitializeComponent();
        }
    }


}
