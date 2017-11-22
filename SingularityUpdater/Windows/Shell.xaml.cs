using SingularityUpdater.ViewModels;
using MahApps.Metro.Controls;

namespace SingularityUpdater {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : MetroWindow {
        private ShellViewModel vm;
        public Shell() {
            InitializeComponent();
            vm = new ShellViewModel();
            vm.CloseRequired += (sender, e) => {
                this.Close();
            };
            this.DataContext = vm;
        }
    }
}
