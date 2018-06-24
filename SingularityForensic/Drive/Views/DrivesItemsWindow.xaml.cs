using CDFCControls.Controls;
using System.Windows;

namespace SingularityForensic.Drive.Views {
    /// <summary>
    /// Drive项选择Window;
    /// </summary>
    public partial class DrivesItemsWindow : CorneredWindow {
        public DrivesItemsWindow() {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            SelectedItem = e.NewValue;
        }



        public object SelectedItem {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object),
                typeof(DrivesItemsWindow),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    }
}
