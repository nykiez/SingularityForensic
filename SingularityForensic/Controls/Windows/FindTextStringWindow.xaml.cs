using CDFCControls.Controls;
using SingularityForensic.Contracts.App;
using System.Windows;
using System.Windows.Input;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Controls.Windows {
    /// <summary>
    /// Interaction logic for FindTextStringWindow.xaml
    /// </summary>
    public partial class FindTextWindow : CorneredWindow {
        public FindTextWindow() {
            InitializeComponent();
        }

        public string Text {
            get {
                return txbText.Text;
            }
            set {
                txbText.Text = value;
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
                if (chbSearchBlock.IsChecked == true) {
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
            if (chbSearchBlock.IsChecked == true) {
                if (SearchBlockSize == null) {
                    MsgBoxService.Show($"{FindResourceString("InvalidBlockSize")}");
                    return false;
                }
                return true;
            }
            return true;
        }
        private bool CheckBlockOffset() {
            if (chbSearchBlock.IsChecked == true) {
                if (SearchBlockOffset == null) {
                    MsgBoxService.Show($"{FindResourceString("InvalidBlockOffset")}");
                    return false;
                }
                else if (SearchBlockOffset > (Text?.Length ?? 0) + SearchBlockSize) {
                    MsgBoxService.Show($"{FindResourceString("BlockOffsetOutOfRange")}");
                    return false;
                }
                else {
                    return true;
                }
            }
            return true;
        }
        #endregion

        public bool Canceld { get; private set; }               //是否取消;

        private void btnSure_Click(object sender, RoutedEventArgs e) {
            if(string.IsNullOrEmpty(Text)){
                MsgBoxService.Show($"{FindResourceString("InvalidText")}");
            }
            else if (CheckBlockSize() && CheckBlockOffset()) {
                Canceld = false;
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Canceld = true;
            Close();
        }

        private void Grid_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                btnSure_Click(null, null);
            }
            else if (e.Key == Key.Escape) {
                btnCancel_Click(null, null);
            }
        }
    }
}
