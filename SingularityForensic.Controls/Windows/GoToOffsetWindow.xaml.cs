using CDFCControls.Controls;
using CDFCMessageBoxes.MessageBoxes;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Controls.MessageBoxes;
using System.Windows;

using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Controls.Windows {
    /// <summary>
    /// Interaction logic for GoToOffsetWindow.xaml
    /// </summary>
    public partial class GoToOffsetWindow : CorneredWindow {
        public GoToOffsetWindow() {
            InitializeComponent();
        }
        public EscapteMethod EscapeMethod {
            get {
                if (rbStart.IsChecked == true) {
                    return EscapteMethod.FromStart;
                }
                else if(rbCurrent.IsChecked == true) {
                    return EscapteMethod.Current;
                }
                else if(rbBackFromCurrent.IsChecked == true) {
                    return EscapteMethod.CurrentBackFrom;
                }
                else {
                    return EscapteMethod.BackFrom;
                }
            }
        }
        public long? Offset {
            get {
                long offset;
                if(long.TryParse(txbOffset.Text,out offset)) {
                    return offset;
                }
                return null;
            }
        }

        public bool Confirmed { get; set; }         //是否确定了;

        private void ConfirmButton_Click(object sender, RoutedEventArgs e) {
            if(Offset == null) {
                CDFCMessageBox.Show( ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("IncorrectPara"));
                return;
            }
            else {
                Confirmed = true;
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        
    }

}
