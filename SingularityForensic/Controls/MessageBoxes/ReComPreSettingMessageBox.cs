using SingularityForensic.Controls.Windows;

namespace SingularityForensic.Controls.MessageBoxes {
    public class ReComPreSettingMessageBox {
        public static ReComPreSetting Show() {
            var window = new ReComPreSettingWindow();
            window.ShowDialog();
            if(window.FilterResult == true) {
                return new ReComPreSetting { Extesions = window.Extensions };
            }
            return null;
        }
    }
    public class ReComPreSetting {
        public string[] Extesions { get; set; }
    }
}
