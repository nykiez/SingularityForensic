using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hex.Events {
    /// <summary>
    /// 当Hex 上下文被加载时发生;
    /// </summary>
    public class HexDataContextLoadedEvent : PubSubEvent<IHexDataContext>{
    }

    public interface IHexDataContextLoadedEventHandler:IEventHandler<IHexDataContext> {

    }
}
