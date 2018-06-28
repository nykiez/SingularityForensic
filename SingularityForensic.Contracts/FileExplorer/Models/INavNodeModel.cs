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
    public interface INavNodeModel:IExtensible {
        ICollection<INavNodeModel> Children { get;  }

        string Name { get; set; }
    }

    public interface INavNodeModelFactory {
        INavNodeModel CreateNew();
    }

    public class NavNodeFactory : GenericServiceStaticInstance<INavNodeModelFactory> {
        public static INavNodeModel CreateNew() => Current?.CreateNew();
    }
}
