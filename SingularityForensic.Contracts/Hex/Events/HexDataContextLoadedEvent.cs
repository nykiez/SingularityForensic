using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hex.Events {
    public class HexDataContextLoadedEvent : PubSubEvent<IHexDataContext>{
    }

    public interface IHexDataContextLoadedEventHandler:IEventHandler<IHexDataContext> {

    }
}
