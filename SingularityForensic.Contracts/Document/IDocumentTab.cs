using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document {
    public interface IDocumentTab : IUIObjectProvider {
        string Title { get; }
        List<CommandItem> Commands { get; }
    }

    public interface IEnumerableDocumentTab : IDocumentTab {
        IEnumerable<IDocumentTab> Children { get; }
    }


    
}
