using MahApps.Metro.Controls;
using SingularityForensic.ViewModels.Shell;
using System.ComponentModel.Composition;
using SingularityForensic.Modules.Shell.Models;
using System.Windows.Input;

namespace SingularityForensic.Modules.Shell.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(IShell))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class Shell : MetroWindow, IShell {
        [ImportingConstructor]
        public Shell(ShellViewModel vm) {
            InitializeComponent();
            this.DataContext = vm;
        }
        
        public void AddInputBinding(InputBinding ib) {
            this.InputBindings.Add(ib);
        }
    }
}
