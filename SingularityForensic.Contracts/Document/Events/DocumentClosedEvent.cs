using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document.Events {
    public class DocumentClosedEvent:PubSubEvent<(IDocumentBase doc,IDocumentService owner)> {
    }

    public interface IDocumentClosedEventHandler:IEventHandler<(IDocumentBase doc, IDocumentService owner)> {

    }
}
