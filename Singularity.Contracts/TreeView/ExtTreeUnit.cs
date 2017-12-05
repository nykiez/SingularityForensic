using CDFCUIContracts.Models;
using Singularity.Contracts.Common;

namespace Singularity.Contracts.TreeView {
    /// <summary>
    /// 泛型树形节点类型,特性为可拓展类型;;
    /// </summary>
    /// <typeparam name="TData">相关数据类型Context</typeparam>
    public class ExtTreeUnit<TData> : PinTreeUnit, IHaveData<TData> {
        public ExtTreeUnit(TData data, ITreeUnit parent, string pinKind = null) :
            base(pinKind ?? $"{data.GetType().Name}Unit", parent) {
            this.Data = data;
        }
        public TData Data { get; set; }
    }
}
