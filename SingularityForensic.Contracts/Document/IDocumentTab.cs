using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document {
    //文档实体结构;
    public interface IDocumentTab : IUIObjectProvider {
        string Title { get; }
        List<CommandItem> Commands { get; }
    }
    
}
