using System.Windows.Input;

namespace SingularityForensic.Modules.Shell.Models {
    public interface IShell {
        //加入快捷键绑定命令;
        void AddInputBinding(InputBinding ib);
        bool Focus();
    }
}
