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
    public class FolderBrowserViewModelCreatedEvent : 
        PubSubEvent<IFolderBrowserViewModel> {
        
    }

    public interface IFolderBrowserViewModelCreatedEventHandler : IEventHandler<IFolderBrowserViewModel> {

    }

    /// <summary>
    /// 被卸载事件;
    /// </summary>
    public class FolderBrowserViewModelUnLoadedEvent : PubSubEvent<IFolderBrowserViewModel> {

    }

    public interface IFolderBrowserViewModelUnLoadedEventHandler : IEventHandler<IFolderBrowserViewModel> {

    }
}
