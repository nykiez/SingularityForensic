using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document.Events {
    //正在添加Tab事件;
    public class DocumentAddingEvent:PubSubEvent<(IDocument tab,IDocumentService owner)> {
    }
}
