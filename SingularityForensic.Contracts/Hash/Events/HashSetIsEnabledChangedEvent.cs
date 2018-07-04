using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash.Events {
    /// <summary>
    /// 哈希集可用状态发生变化时启用;
    /// </summary>
    public class HashSetIsEnabledChangedEvent:PubSubEvent<IHashSet> {

    }

    public interface IHashSetIsEnabledChangedEventHandler : IEventHandler<IHashSet> {

    }
}
