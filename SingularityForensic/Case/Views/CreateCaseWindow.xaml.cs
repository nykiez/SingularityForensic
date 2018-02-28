using CDFCControls.Controls;
using SingularityForensic.Case.ViewModels;
using System.Windows;

namespace SingularityForensic.Case.Views {
    /// <summary>
    /// Interaction logic for CreateCaseWindow.xaml
    /// </summary>
    public partial class CreateCaseWindow : CorneredWindow {
        private CreateCaseWindowViewModel vm;
        public CreateCaseWindow(CreateCaseWindowViewModel viewModel) {
            vm = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
            //this.Resources.MergedDictionaries.LoadLanguage("CDFCMessageBoxes");
        }
        private void MetroWindow_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) {
            bool res = false;
            if (bool.TryParse(e.NewValue.ToString(), out res)) {
                if (!res) {
                    this.Close();
                }
            }
        }
        
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (vm.IsEnabled) {
                this.DialogResult = false;
            }
            else {
                this.DialogResult = true;
            }
        }
        

        private void closeBtn_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
