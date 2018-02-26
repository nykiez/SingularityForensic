using Singularity.UI.MessageBoxes.Windows;

namespace Singularity.UI.MessageBoxes.MessageBoxes {
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
