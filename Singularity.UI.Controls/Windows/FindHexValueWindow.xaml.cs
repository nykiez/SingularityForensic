using CDFCControls.Controls;
using CDFCMessageBoxes.MessageBoxes;
using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Input;

using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.MessageBoxes.Windows {
    /// <summary>
    /// Interaction logic for FindHexValueWindow.xaml
    /// </summary>
    public partial class FindHexValueWindow : CorneredWindow {
        public FindHexValueWindow() {
            InitializeComponent();
            
        }

        

        public byte[] HexValue {
            get {
                return HexToByte(txbText.Text);
            }
            set { 
                if(value != null) {
                    txbText.Text = ByteToHex(value)?.ToUpper()??string.Empty;
                }
            }
        }

        //是否是区域检索;
        public bool IsBlockSearch {
            get {
                return chbSearchBlock.IsChecked == true;
            }
            set {
                chbSearchBlock.IsChecked = value;
            }
        }

        //区域检索大小;
        public int? SearchBlockSize {
            get {
                if(chbSearchBlock.IsChecked == true) {
                    int _bSize = 0;
                    if (int.TryParse(txbBlockSize.Text, out _bSize)) {
                        return _bSize;
                    }
                }
                return null;
            }
            set {
                txbBlockSize.Text = (value?.ToString()) ?? 512.ToString();
            }
        }

        //区域偏移;
        public int? SearchBlockOffset {
            get {
                if (chbSearchBlock.IsChecked == true) {
                    int _blockOffset = 0;
                    if (int.TryParse(txbBlockOffset.Text, out _blockOffset)) {
                        return _blockOffset;
                    }
                }
                return null;
            }
            set {
                txbBlockOffset.Text = (value?.ToString()) ?? 0.ToString();
            }
        }

        #region 检查输入方法;
        private bool CheckBlockSize() {
            if(chbSearchBlock.IsChecked == true) {
                if (SearchBlockSize == null) {
                    CDFCMessageBox.Show($"{FindResourceString("InvalidBlockSize")}");
                    return false;
                }
                return true;
            }
            return true;   
        }
        private bool CheckBlockOffset() {
            if (chbSearchBlock.IsChecked == true) {
                if (SearchBlockOffset == null) {
                    CDFCMessageBox.Show($"{FindResourceString("InvalidBlockOffset")}");
                    return false;
                }
                else if (SearchBlockOffset > (HexValue?.Length ?? 0) + SearchBlockSize) {
                    CDFCMessageBox.Show($"{FindResourceString("BlockOffsetOutOfRange")}");
                    return false;
                }
                else {
                    return true;
                }
            }
            return true;
        }
        #endregion

        public bool Canceld { get; private set; } = true;              //是否取消;
        
        private void btnSure_Click(object sender, RoutedEventArgs e) {
            if(HexValue == null || HexValue.Length == 0) {
                CDFCMessageBox.Show(FindResourceString("InvalidInput"));
            }
            else if(CheckBlockSize() && CheckBlockOffset()){
                Canceld = false;
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Canceld = true;
            Close();
        }

        public static bool HexToByte(string hex, out byte b) {
            return byte.TryParse(hex, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out b);
        }
        /// <summary>
        /// Converts the hex string to an byte array. The hex string must be separated by a space char ' '. If there is any invalid hex information in the string the result will be null.
        /// </summary>
        public static byte[] HexToByte(string hex) {
            if (string.IsNullOrEmpty(hex))
                return null;

            hex = hex.Trim();
            if (hex.Length % 2 != 0 || string.IsNullOrEmpty(hex)) {
                return null;
            }
            var byteArray = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length / 2; i ++) {
                var hexValue = hex.Substring(i * 2, 2);

                byte b;
                var isByte = HexToByte(hexValue, out b);
                if (!isByte)
                    return null;
                byteArray[i] = b;
            }

            return byteArray;
        }

        public static string ByteToHex(byte[] bs) {
            if (bs == null) {
                throw new ArgumentNullException(nameof(bs));
            }

            var sb = new StringBuilder();
            foreach (var item in bs) {
                sb.Append(Convert.ToString(item, 16));
            }
            return sb.ToString();
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Enter) {
                btnSure_Click(null, null);
            }
            else if(e.Key == Key.Escape) {
                btnCancel_Click(null, null);
            }
           
        }
    }
}
