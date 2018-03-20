using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common.Commands {
    /// <summary>
    /// 打开资源管理器,定位路径的命令;
    /// </summary>
    public class OpenPathCommand:DelegateCommand {
        public OpenPathCommand(string path):base(CreateExecute(path)) {
            
        }

        private static Action CreateExecute(string path) {
            var direct = Path.GetFullPath(path);
            return () => Process.Start("explorer.exe",direct);
        }
    }
}
