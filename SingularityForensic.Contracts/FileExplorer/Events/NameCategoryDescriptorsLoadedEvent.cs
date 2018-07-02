using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer.Events {
    /// <summary>
    /// 命名类别文件被加载事件;
    /// </summary>
    public class NameCategoryDescriptorsLoadedEvent:PubSubEvent {
    }

    public interface INameCategoryDescriptorsLoadedEventHandler : IEventHandler {

    }
}
