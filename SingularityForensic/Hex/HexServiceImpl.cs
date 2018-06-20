using SingularityForensic.Contracts.Hex;
using System;
using System.ComponentModel.Composition;
using System.IO;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex.Events;
using System.Collections.Generic;

namespace SingularityForensic.Hex {
    [Export(typeof(IHexService))]
    public class HexServiceImpl : IHexService {
        [ImportingConstructor]
        public HexServiceImpl([ImportMany]IEnumerable<IHexDataContextLoadedEventHandler> hexDataContextLoadedEventHandlers) {
            this._hexDataContextLoadedEventHandlers = hexDataContextLoadedEventHandlers;
        }
        private IEnumerable<IHexDataContextLoadedEventHandler> _hexDataContextLoadedEventHandlers;

        public IHexDataContext CreateNewHexDataContext(Stream stream = null) {
            return new HexDataContext(stream);
        }

        public void LoadHexDataContext(IHexDataContext hexDataContext) {
            if(hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }

            PubEventHelper.PublishEventToHandlers(hexDataContext, _hexDataContextLoadedEventHandlers);

            PubEventHelper.GetEvent<HexDataContextLoadedEvent>().Publish(hexDataContext);
        }
    }
}
