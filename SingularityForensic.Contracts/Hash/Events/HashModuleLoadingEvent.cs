using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash.Events {
    public class HashModuleLoadingEvent:PubSubEvent {
    }

    public interface IHashModuleLoadingEventHandler:IEventHandler {

    }
}
