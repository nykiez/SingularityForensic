using SingularityForensic.Hex.Models;
using SingularityForensic.Hex.Views;

namespace SingularityForensic.Hex.Controls {
    public class GoToOffsetMessageBox {
        public GoToOffsetSetting Show() {
            var window = new GoToOffsetWindow();
            window.ShowDialog();

            if (window.Confirmed) {
                var setting = new GoToOffsetSetting {
                    EscapteMethod = window.EscapeMethod,
                    Offset = window.Offset.Value
                };
                return setting;
            }
            else {
                return null;
            }
        }
    }
    
}
