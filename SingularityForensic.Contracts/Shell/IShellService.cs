using SingularityForensic.Contracts.Common;
using System.Windows.Input;

namespace SingularityForensic.Contracts.Shell {
    public interface IShellService {
        /// <summary>
        /// 更改标题栏文字;
        /// </summary>
        /// <param name="word"></param>
        /// <param name="saveBrandName">是否保留软件名称</param>
        void SetTitle(string word, bool saveBrandName = true);
        /// <summary>
        /// 聚焦;
        /// </summary>
        void Focus();
        void ChangeLoadState(bool isLoading, string word = null);
        /// <summary>
        /// 添加热键绑定;
        /// </summary>
        /// <param name="command"></param>
        /// <param name="key"></param>
        /// <param name="modifier"></param>
        void AddKeyBinding(ICommand command, Key key, ModifierKeys modifier = ModifierKeys.None);
        /// <summary>
        /// 显示窗体(初始化时使用);
        /// </summary>
        void Show();
        /// <summary>
        /// 关闭窗体;
        /// </summary>
        void Close();
        /// <summary>
        /// 窗体;
        /// </summary>
        object Shell { get; }
    }

    public class ShellService : GenericServiceStaticInstance<IShellService> {

    }
}
