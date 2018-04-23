using SingularityForensic.Contracts.Hex;
using System;
using System.ComponentModel.Composition;
using WpfHexaEditor.Core.Bytes;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;
using System.IO;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex.Events;

namespace SingularityForensic.Hex {
    [Export(typeof(IHexService))]
    public class HexServiceImpl : IHexService {
        public IHexDataContext CreateNewHexDataContext(Stream stream) {
            return new HexDataContext(stream);
        }

        public void LoadHexDataContext(IHexDataContext hexDataContext) {
            if(hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }
            
            PubEventHelper.GetEvent<HexDataContextLoadedEvent>().Publish(hexDataContext);
        }
    }
}
