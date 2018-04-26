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
        void Initialize();

        /// <summary>
        /// 创建一个Tab;
        /// </summary>
        /// <returns></returns>
        IDocument CreateNewDocument();

        /// <summary>
        /// 创建一个多级的Tab;
        /// </summary>
        /// <returns></returns>
        IEnumerableDocument CreateNewEnumerableDocument();

        /// <summary>
        /// 当前所有的文档;
        /// </summary>
        IEnumerable<IDocumentBase> CurrentDocuments { get; }

        /// <summary>
        /// 关闭所有文档;
        /// </summary>
        void CloseAllDocuments();

        /// <summary>
        /// 选定的文档;
        /// </summary>
        IDocumentBase SelectedDocument { get; set; }

        /// <summary>
        /// 移除Tab;
        /// </summary>
        /// <param name="tab"></param>
        void RemoveDocument(IDocumentBase doc);


        /// <summary>
        /// 添加文档页;
        /// </summary>
        /// <param name="tab"></param>
        void AddDocument(IDocumentBase doc);
    }

    public static class DocumentService {
        private static IDocumentService _service;
        public static IDocumentService MainDocumentService => _service ?? (_service = ServiceProvider.Current?.GetInstance<IDocumentService>(Constants.MainDocumentService));
    }
}
