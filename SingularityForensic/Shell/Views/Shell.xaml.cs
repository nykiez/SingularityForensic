using MahApps.Metro.Controls;
using System.ComponentModel.Composition;
using System.Windows.Input;
using SingularityForensic.Contracts.Shell;

namespace SingularityForensic.Shell.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(IShell))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class Shell : MetroWindow, IShell {
        public Shell() {
            InitializeComponent();
        }
        
        public void AddInputBinding(InputBinding ib) {
            this.InputBindings.Add(ib);
        }
    }
}
