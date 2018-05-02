using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common.Events {
    /// <summary>
    /// 视图被创建事件;
    /// </summary>
    public class ViewCreatedEvent:PubSubEvent<(object uiObject,string viewName)> {
    }

    public interface IViewCreatedEventHandler : IEventHandler<(object uiObject,string viewName)> {

    }
}
