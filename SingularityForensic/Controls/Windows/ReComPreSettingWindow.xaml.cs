using CDFCControls.Controls;
using SingularityForensic.Contracts.App;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Controls.Windows {
    /// <summary>
    /// 文件重组扫描预设定窗体;
    /// </summary>
    public partial class ReComPreSettingWindow  {
        public ReComPreSettingWindow() {
            InitializeComponent();
        }

        public bool? FilterResult { get; private set; }

        public string[] Extensions {
            get {
                var sb = new StringBuilder();
                for (int i = 0; i < griCondition.Children.Count; i++) {
                    var chb = griCondition.Children[i] as CheckBox;
                    if(chb != null && chb.IsChecked == true) {
                        sb.Append((chb.Tag?.ToString() + ";")??string.Empty);
                    }
                }
                var list = sb.ToString().Split(';').ToList();
                list.RemoveAll(p => string.IsNullOrEmpty(p));
                return list.ToArray();
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e) {
            if(Extensions == null || Extensions.Length == 0) {
                MsgBoxService.Show($"{FindResourceString("PleaseSpecifyType")}");
            }
            else {
                this.FilterResult = true;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
