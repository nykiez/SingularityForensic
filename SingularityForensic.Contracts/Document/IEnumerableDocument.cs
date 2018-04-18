using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document {
    /// <summary>
    /// 具有多个子项的文档;
    /// </summary>
    public interface IEnumerableDocument : IDocumentBase, IDocumentService {
        IEnumerable<IDocumentBase> Children { get; }

        /// <summary>
        /// 主文档内容;
        /// </summary>
        object MainUIObject { get; set; }

        ///// <summary>
        ///// 添加Tab;
        ///// </summary>
        ///// <param name="tab"></param>
        //void AddDocument(IDocument tab);

        ///// <summary>
        ///// 移除Tab;
        ///// </summary>
        ///// <param name="tab"></param>
        //void RemoveDocument(IDocument tab);

        
    }

}
