using Prism.Regions;
using Singularity.Contracts.Common;

namespace Singularity.Contracts.TabControl {
    /// <summary>
    /// /// 可拓展Tab类型;通过字符串作为唯一类型标识;
    /// </summary>
    public class PinTabModel : TabModel, IHavePinKind {
        public PinTabModel(string pinKind) {
            this.ContentId = pinKind;
            //System.Collections.IEnumerable
        }
    }


    /// <summary>
    /// 可拓展Tab类型;具有上下文;
    /// </summary>
    /// <typeparam name="TData">相关数据类型Context</typeparam>
    public class ExtTabModel<TData> : PinTabModel, IHaveData<TData> {
        public ExtTabModel(TData data, string pinKind) : base(pinKind) {
            this.Data = data;
        }

        public TData Data { get; }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => navigationContext.Parameters["pinKind"] as string == ContentId;

    }

}
