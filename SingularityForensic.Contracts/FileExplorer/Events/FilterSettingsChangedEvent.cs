using Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer.Events {
    /// <summary>
    /// 过滤设定变更时响应;
    /// </summary>
    public class FilterSettingsChangedEvent:PubSubEvent<IEnumerable> {
    }
}
