using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document {
    //文档tab服务契约;
    public interface IDocumentService {
        /// <summary>
        /// 添加文档页;
        /// </summary>
        /// <param name="tab"></param>
        void AddDocument(IDocument tab);

        /// <summary>
        /// 创建并添加一个Tab;
        /// </summary>
        /// <returns></returns>
        IDocument AddNewDocument();

        /// <summary>
        /// 创建一个多级的Tab;
        /// </summary>
        /// <returns></returns>
        IEnumerableDocument AddNewEnumerableDocument();

        /// <summary>
        /// 当前所有的文档;
        /// </summary>
        IEnumerable<IDocument> CurrentDocuments { get; }

        /// <summary>
        /// 关闭所有Tab;
        /// </summary>
        void CloseAllDocuments();

        /// <summary>
        /// 选定的Tab;
        /// </summary>
        IDocument SelectedDocument { get; set; }

        /// <summary>
        /// 移除Tab;
        /// </summary>
        /// <param name="tab"></param>
        void RemoveDocument(IDocument tab);

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

    public static class DocumentService {
        private static IDocumentService _service;
        public static IDocumentService MainDocumentService => _service ?? (_service = ServiceProvider.Current?.GetInstance<IDocumentService>(Constants.MainDocumentService));
    }
}
