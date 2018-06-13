using Microsoft.Practices.ServiceLocation;
using SingularityForensic.Contracts.Common;
using System.ComponentModel.Composition;
using System.Windows;

namespace DemoUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export]
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            this.Loaded += delegate {
                ServiceProvider.Current.GetInstance<MainWindow>();
            };
        }

        
    }
    
         
}
