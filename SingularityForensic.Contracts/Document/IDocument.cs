using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document {
    /// <summary>
    /// 文档实体结构契约基类;
    /// </summary>
    public interface IDocumentBase:IUIObjectProvider,IExtensible {
        string Title { get; set; }
        IList<ICommandItem> CustomCommands { get; }
    }

    /// <summary>
    /// 文档实体结构;(UIObject可更改);
    /// </summary>
    public interface IDocument : IDocumentBase {
        object UIObject { get; set; }
    }
    
}
