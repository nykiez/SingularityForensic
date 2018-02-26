using Singularity.UI.MessageBoxes.Models;
using Singularity.UI.MessageBoxes.Windows;

namespace Singularity.UI.MessageBoxes.MessageBoxes {
    public class SignSearchMessageBox {
        public SignSearchSetting Show() {
            var setting = new SignSearchSetting();
            var window = new SignSearchWindow();
            window.ShowDialog();
            if (window.DialResult == true) {
                setting.SectorSize = window.SecSize??512;
                setting.MaxSize = (window.MaxSize ?? 10) * 1024 * 1024;
                setting.KeyWord = window.HexValue;
                setting.SecStartLBA = window.SecStartLBA??0;
                setting.FileExtension = window.ExtenName;
                setting.AlignToSec = window.AlignToSector;

                return setting;
            }
            return null;
        }
    }
    
}
