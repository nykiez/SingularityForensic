using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document {
    //文档tab服务契约;
    public interface IDocumentTabService {
        void AddTab(IDocumentTab tabModel);
        //正在添加事件;
        event EventHandler<IDocumentTab> TabAdding;
        //已经添加事件;
        event EventHandler<IDocumentTab> TabAdded;
        
        IEnumerable<IDocumentTab> CurrentTabs { get; }

        void CloseAllTabs();

        //正在清除事件;
        event EventHandler<CancelEventArgs> TabsClearing;
        //已经清除事件;
        event EventHandler TabsCleared;

        void CloseTab(IDocumentTab tabModel);
        //正在关闭事件;
        event EventHandler<(IDocumentTab tab,CancelEventArgs e)> TabClosing;
        //已经关闭事件;
        event EventHandler<IDocumentTab> TabClosed;

        IDocumentTab SelectedTab { get; set; }
        //活跃文档变更事件;
        event EventHandler<IDocumentTab> SelectedTabChanged;
    }

    public class DocumentTabService : GenericServiceStaticInstance<IDocumentTabService> {

    }
}
