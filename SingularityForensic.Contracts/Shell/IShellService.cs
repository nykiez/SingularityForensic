using SingularityForensic.Contracts.Common;
using System.Windows.Input;

namespace SingularityForensic.Contracts.Shell {
    public interface IShellService {
        //更改标题栏文字;
        void SetTitle(string word, bool saveBrandName = true);
        void Focus();
        void ChangeLoadState(bool isLoading, string word = null);
        void AddKeyBinding(ICommand command, Key key, ModifierKeys modifier = ModifierKeys.None);
    }

    public class ShellService : GenericServiceStaticInstance<IShellService> {

    }
}
