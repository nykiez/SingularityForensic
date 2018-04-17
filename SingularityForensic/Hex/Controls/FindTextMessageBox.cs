using SingularityForensic.Controls.Windows;

namespace SingularityForensic.Hex.Controls {
    public class FindTextMessageBox {
        FindTextWindow window;
        public FindTextStringSetting Show(ref FindTextStringSetting setting) {
            if(setting == null) {
                setting = new FindTextStringSetting();
            }

            window = new FindTextWindow();
            window.Text = setting.FindText;
            window.SearchBlockOffset = setting.BlockOffset;
            window.IsBlockSearch = setting.IsBlockSearch;
            window.SearchBlockSize = setting.BlockSize;

            window.ShowDialog();
            if (!window.Canceld) {
                setting.FindText = window.Text ;
                setting.IsBlockSearch = window.IsBlockSearch;

                if (setting.IsBlockSearch) {
                    setting.BlockSize = window.SearchBlockSize;
                    setting.BlockOffset = window.SearchBlockOffset;
                }

                return setting;
            }
            return null;
        }
    }
    //查找字符设定;
    public class FindTextStringSetting {
        //查找字符串;
        public string FindText { get; set; }
        //是否是区域扫描;     
        public bool IsBlockSearch { get; set; }
        //区域大小;
        public int? BlockSize { get; set; }
        //区域偏移;
        public int? BlockOffset { get; set; }
    }
}
