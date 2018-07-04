using CDFCControls.Controls;
using SingularityForensic.App.ViewModels;
using System.Windows;

namespace SingularityForensic.App.Views {
    /// <summary>
    /// Interaction logic for CDFCMessageBox.xaml
    /// </summary>
    public partial class SingularityMessageBoxDialog  {
        private SingularityMessageBoxDialogViewModel vm;
        public SingularityMessageBoxDialog(SingularityMessageBoxDialogViewModel vm) {
            this.vm = vm;
            this.DataContext = vm;
            InitializeComponent();
        }
        
        private void MetroWindow_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) {
            bool res = false;
            if (bool.TryParse(e.NewValue.ToString(), out res)) {
                if (!res) {
                    this.Close();
                }
            }
        }
        
    }
}
