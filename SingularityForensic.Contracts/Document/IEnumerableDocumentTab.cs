using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document {
    //具有多个子项的文档;
    public interface IEnumerableDocumentTab : IDocumentTab {
        IEnumerable<IDocumentTab> Children { get; }

        /// <summary>
        /// 添加Tab;
        /// </summary>
        /// <param name="tab"></param>
        void AddTab(IDocumentTab tab);

        /// <summary>
        /// 移除Tab;
        /// </summary>
        /// <param name="tab"></param>
        void RemoveTab(IDocumentTab tab);

        /// <summary>
        /// 选定的Tab;
        /// </summary>
        IDocumentTab SelectedTab { get; }
    }

}
