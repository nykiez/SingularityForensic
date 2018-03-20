using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.TreeView;
using System.Collections.Generic;

namespace SingularityForensic.Contracts.MainPage {
    /// <summary>
    /// 树形节点服务契约;
    /// </summary>
    public interface INodeService {
        //void AddUnit(TreeUnit unit);
        void AddUnit(TreeUnit parentUnit, TreeUnit unit);
        
        void RemoveUnit(TreeUnit unit);

        IEnumerable<TreeUnit> CurrentUnits { get; }
        TreeUnit SelectedUnit { get; }
        void ClearNodes();
    }

    public class NodeService:GenericServiceStaticInstance<INodeService> {

    }
}
