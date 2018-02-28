using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SingularityForensic.Adb.Views.AdbViewer {
    /// <summary>
    /// Interaction logic for DeviceSelector.xaml
    /// </summary>
    public partial class DeviceSelector : UserControl {
        public DeviceSelector() {
            InitializeComponent();
        }

        private void Button_PreviewMouseMove(object sender, MouseEventArgs e) {
            var btn = sender as Button;
            if (e.LeftButton == MouseButtonState.Pressed){
                DataObject data = new DataObject(typeof(Button), btn);
                DragDrop.DoDragDrop(btn, data, DragDropEffects.Move);
            }
        }
    }
}
