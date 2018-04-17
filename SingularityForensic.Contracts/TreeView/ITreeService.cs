using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.TreeView {
    /// <summary>
    /// 树形节点服务契约;
    /// </summary>
    public interface ITreeService {
        //void AddUnit(TreeUnit unit);
        void AddUnit(ITreeUnit parentUnit, ITreeUnit unit);

        void RemoveUnit(ITreeUnit unit);

        IEnumerable<ITreeUnit> CurrentUnits { get; }
        ITreeUnit SelectedUnit { get; }
        void ClearNodes();
    }
    
}
