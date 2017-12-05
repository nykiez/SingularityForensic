using CDFCUIContracts.Models;
using System.Collections.Generic;

namespace Singularity.Contracts.MainPage {
    /// <summary>
    /// 树形节点服务契约;
    /// </summary>
    public interface INodeService {
        //void AddUnit(ITreeUnit unit);
        void AddUnitWith<TParentUnit, TNewUnit>(TParentUnit pUnit, TNewUnit nUnit)
            where TParentUnit : ITreeUnit
            where TNewUnit : ITreeUnit;

        void AddUnit<TNewUnit>(TNewUnit nUnit) where TNewUnit : ITreeUnit;

        void RemoveUnit(ITreeUnit unit);
        IEnumerable<ITreeUnit> CurrentUnits { get; }
        ITreeUnit SelectedNode { get; }
        void ClearNodes();
    }
}
