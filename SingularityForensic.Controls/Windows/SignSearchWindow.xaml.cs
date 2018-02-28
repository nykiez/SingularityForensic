using CDFCControls.Controls;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Windows;
using static CDFCCultures.Helpers.StringHelpers;
using static CDFCCultures.Managers.ManagerLocator;
namespace SingularityForensic.Controls.Windows {
    /// <summary>
    /// Interaction logic for SignSearchWindow.xaml
    /// </summary>
    public partial class SignSearchWindow : CorneredWindow {
        public SignSearchWindow() {
            InitializeComponent();
            this.txbSecSize.Text = 512.ToString() ;
            this.txbMaxSize.Text = 10.ToString();
            this.txbStartLBA.Text = 0.ToString();

            this.chbSecAlign.Checked += delegate { ChangeAlignSecState(); };
            this.chbSecAlign.Unchecked += delegate { ChangeAlignSecState(); };
        }

        public bool? DialResult { get; private set; }

        public byte[] HexValue {
            get {
                if (!string.IsNullOrEmpty(txbHex.Text)) {
                    var bs = txbHex.Text.HexToByte();
                    return bs;
                }
                return null;
            }
        }
        
        //public long EndLBA {
        //    get {
        //        long edLBA = 0;
        //        if(long.TryParse(txbEndLBA.Text,out edLBA)) {
        //            return edLBA;
        //        }
        //        return 0;
        //    }
        //    set {
        //        txbEndLBA.Text = value.ToString();
        //    }
        //}
        public string ExtenName {
            get {
                if (!string.IsNullOrEmpty(txbExt.Text)) {
                    if (txbExt.Text.StartsWith(".")) {
                        return txbExt.Text.Substring(1);
                    }
                    return txbExt.Text;
                }
                return string.Empty;
            }
        }

        public int? SecSize => ParseIntFunc(txbSecSize.Text);

        public int? MaxSize => ParseIntFunc(txbMaxSize.Text);

        public int? SecStartLBA => ParseIntFunc(txbStartLBA.Text);

        public bool AlignToSector => chbSecAlign.IsChecked == true;

        private Func<string,int?> ParseIntFunc = str => {
            int val = 0;
            if (int.TryParse(str, out val)) {
                return val;
            }
            return null;
        };

        #region 各检查输入方法;
        private bool CheckHex() {
            if(HexValue == null) {
                txbSignError.Text = HexValue == null ? ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("InvalidHex") : string.Empty;
                return false;
            }
            else {
                txbSignError.Text = string.Empty;
                return true;
            }
        }

        private bool CheckSecSize() {
            if (SecSize != null && SecSize >= 512 && SecSize <= 4096) {
                txbSecSizeError.Text = string.Empty;
                return true;
            }
            else {
                txbSecSizeError.Text = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("InvalidSecSize");
                return false;
            }
        }

        private bool CheckSecOffset() {
            if (SecStartLBA != null && SecStartLBA >= 0 && SecStartLBA < ( SecSize ?? 512 ) ) {
                txbStartLBAError.Text = string.Empty;
                return true;
            }
            else {
                txbStartLBAError.Text = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("InvalidSecOffset");
                return false;
            }
        }

        private bool CheckMaxSize() {
            if(MaxSize != null && MaxSize >= 1 && MaxSize <= 1024){
                txbMaxSizeError.Text = string.Empty;
                return true;
            }
            else {
                txbMaxSizeError.Text = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("InvalidMaxFileSize");
                return false;
            }
        }

        #endregion

        private void btnConfirm_Click(object sender, RoutedEventArgs e) => DoConfirm();

        //确认输入;
        private void DoConfirm() {
            var s = (!CheckHex()) |
                (!CheckSecOffset()) |
                (!CheckMaxSize()) |
                (!CheckSecSize());
            if ((!CheckHex()) |
                (!CheckSecOffset()) |
                (!CheckMaxSize()) |
                (!CheckSecSize())) {

                return;
            }
            else {
                DialResult = true;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
            this.Close();
        }

        private void CorneredWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if(e.Key == System.Windows.Input.Key.Enter) {
                DoConfirm();
            }
        }

        private void ChangeAlignSecState() {
            if(txbSecSize != null) {
                txbSecSize.IsEnabled = chbSecAlign.IsChecked == true;
            }

            if (txbStartLBA != null) {
                txbStartLBA.IsEnabled = chbSecAlign.IsChecked == true;
            }
            
        }
            
    }
}
