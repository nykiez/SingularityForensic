using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing.Events {
    /// <summary>
    /// 案件正在加载事件;
    /// </summary>
    public class CaseLoadingEvent : PubSubEvent<ICase> {

    }
}
