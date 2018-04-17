using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document {
    //文档实体结构;
    public interface IDocument : IUIObjectProvider,IInstanceExtensible {
        string Title { get; set; }
        IList<ICommandItem> CustomCommands { get; }
        object UIObject { get; set; }

    }
    
}
