using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Document.Events {
    [Export(typeof(IDocumentAddedEventHandler))]
    class DocumentAddedOnNavigateToDocumentsViewHandler : IDocumentAddedEventHandler {
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle((IDocumentBase tab, IDocumentService owner) args) {
            RegionHelper.RequestNavigate(
                    Contracts.MainPage.Constants.MainPageDocumentRegion,
                    Contracts.Document.Constants.DocumentTabsView
            );
        }
    }
}
