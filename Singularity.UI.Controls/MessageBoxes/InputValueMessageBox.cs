using Singularity.UI.MessageBoxes.Windows;

namespace Singularity.UI.MessageBoxes.MessageBoxes {
    /// <summary>
    /// 输入字符串对话框;
    /// </summary>
    public class InputValueMessageBox {
        /// <summary>
        /// 请求输入;
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="des">对话框内容</param>
        /// <returns></returns>
        public static string Show(string des = "",string title = "",string val = "") {
            var window = new InputStringWindow(title, des);
            window.Val = val;
            window.ShowDialog();
            if(window.InputResult == true) {
                return window.Val;
            }
            else {
                return null;
            }
        }
    }
}
