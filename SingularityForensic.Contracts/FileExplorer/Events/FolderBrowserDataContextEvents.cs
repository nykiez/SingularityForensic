using Prism.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer.Events {
    /// <summary>
    /// 目录/资源浏览器模型被创建事件;
    /// </summary>
    public class FolderBrowserDataContextCreatedEvent : 
        PubSubEvent<IFolderBrowserDataContext> {
        
    }

    public interface IFolderBrowserDataContextCreatedEventHandler : IEventHandler<IFolderBrowserDataContext> {

    }

    /// <summary>
    /// 被卸载事件;
    /// </summary>
    public class FolderBrowserViewModelUnLoadedEvent : PubSubEvent<IFolderBrowserDataContext> {

    }

    public interface IFolderBrowserViewModelUnLoadedEventHandler : IEventHandler<IFolderBrowserDataContext> {

    }
}
