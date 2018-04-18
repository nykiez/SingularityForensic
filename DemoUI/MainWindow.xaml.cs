using Microsoft.Practices.ServiceLocation;
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
                ServiceLocator.Current.GetInstance<MainWindow>();
            };
        }

        
    }
    
         
}
