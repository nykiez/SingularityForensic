using Singularity.UI.MessageBoxes.Windows;

namespace Singularity.UI.MessageBoxes.MessageBoxes {
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
    public class GoToOffsetSetting {
        public EscapteMethod EscapteMethod { get; set; }
        public long Offset { get; set; }
    }

    //跳转方法;
    public enum EscapteMethod {
        FromStart,                      //从起始位置开始;
        Current,                        //从当前位置开始;
        CurrentBackFrom,                //从当前位置(往之前)开始;
        BackFrom                        //从末尾位置开始;
    }
}
