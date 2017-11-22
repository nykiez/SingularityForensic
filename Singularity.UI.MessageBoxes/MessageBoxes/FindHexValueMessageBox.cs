using Singularity.UI.MessageBoxes.Windows;

namespace Singularity.UI.MessageBoxes.MessageBoxes {
    public class FindHexValueMessageBox {
        FindHexValueWindow window;
        public FindHexValueSetting Show(ref FindHexValueSetting setting) {
            if(setting == null) {
                setting = new FindHexValueSetting();
            }

            window = new FindHexValueWindow();
            window.SearchBlockOffset = setting.BlockOffset;
            window.SearchBlockSize = setting.BlockSize;
            window.IsBlockSearch = setting.IsBlockSearch;
            window.HexValue = setting.HexBytes;

            window.ShowDialog();
            if (!window.Canceld) {
                setting.HexBytes = window.HexValue;
                setting.IsBlockSearch = window.IsBlockSearch;

                if (window.IsBlockSearch) {
                    setting.BlockSize = window.SearchBlockSize;
                    setting.BlockOffset = window.SearchBlockOffset;
                }
                
                return setting;
            }
            return null;
        }
    }

    //十六进制搜寻设定;
    public class FindHexValueSetting {
        //字节数组;
        public byte[] HexBytes { get; set; }  
        //是否是区域扫描;     
        public bool IsBlockSearch { get; set; }     
        //区域大小;
        public int? BlockSize { get; set; }
        //区域偏移;
        public int? BlockOffset { get; set; }
    }
}
