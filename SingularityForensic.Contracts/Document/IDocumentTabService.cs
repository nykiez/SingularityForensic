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
        void AddTab(IDocumentTab tab);
        
        IEnumerable<IDocumentTab> CurrentTabs { get; }

        void CloseAllTabs();

        IDocumentTab SelectedTab { get; set; }

        void RemoveTab(IDocumentTab tab);

        ////正在清除事件;
        //event EventHandler<CancelEventArgs> TabsClearing;
        ////已经清除事件;
        //event EventHandler TabsCleared;
        
        ////正在关闭事件;
        //event EventHandler<(IDocumentTab tab,CancelEventArgs e)> TabClosing;

        ////已经关闭事件;
        //event EventHandler<IDocumentTab> TabClosed;
        
        ////活跃文档变更事件;
        //event EventHandler<IDocumentTab> SelectedTabChanged;
    }

    public class DocumentTabService : GenericServiceStaticInstance<IDocumentTabService> {

    }
}
