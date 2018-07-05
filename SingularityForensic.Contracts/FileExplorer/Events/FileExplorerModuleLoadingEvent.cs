using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer.Events {
    /// <summary>
    /// 资源管理器模块正在加载事件;
    /// </summary>
    public class FileExplorerModuleLoadingEvent : PubSubEvent {

    }

    
    public interface IFileExplorerModuleLoadingEventHandler : IEventHandler {
        
    }
}
