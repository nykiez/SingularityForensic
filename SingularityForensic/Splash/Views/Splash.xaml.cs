using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;

namespace SingularityForensic.Splash.Views {
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    [Export]
    public partial class Splash : Window {
        public Splash() {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }
    }
}
