using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingularityForensic.Contracts.Shell {
    public interface IShell {
        //加入快捷键绑定命令;
        void AddInputBinding(InputBinding ib);
        bool Focus();
    }
}
