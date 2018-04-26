using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer.Models {
    /// <summary>
    /// 路径节点;
    /// </summary>
    public interface INavNodeModel {
        IFile File { get; }

        string Name { get; }

        //跳转事件;
        event EventHandler<IFile> EscapeRequired;
    }

    public interface INavNodeModelFactory {
        INavNodeModel CreateNew(IFile file);
    }

    public class NavNodeFactory : GenericServiceStaticInstance<INavNodeModelFactory> {
        public static INavNodeModel CreateNew(IFile file) => Current?.CreateNew(file);
    }
}
