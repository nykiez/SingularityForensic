using CDFCUIContracts.Models;
using Singularity.Contracts.Common;

namespace SingularityForensic.Modules.MainPage.Models {
    /// <summary>
    /// 泛型树形节点类型,特性为可拓展类型;;
    /// </summary>
    /// <typeparam name="TData">相关数据类型Context</typeparam>
    public class ExtTreeUnit<TData> : PinTreeUnit, IHaveData<TData> {
        public ExtTreeUnit(TData data, TreeUnit parent, string pinKind = null) :
            base(pinKind ?? $"{data.GetType().Name}Unit", parent) {
            this.Data = data;
        }
        public TData Data { get; set; }
    }

    /// <summary>
    /// 通过字符串作为唯一类型标识的节点类型;
    /// </summary>
    public class PinTreeUnit : TreeUnit, IHavePinKind {
        public PinTreeUnit(string pinKind, TreeUnit parent) : base(parent) {
            this.ContentId = pinKind;
        }
        //节点类型标识;
        public string ContentId { get; }
        public const string PinSpliter = "\\";
    }

}
