using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Hex.Events {
    /// <summary>
    /// 加入多种编码的右键菜单;
    /// </summary>
    [Export(typeof(IHexDataContextLoadedEventHandler))]
    class OnHexDataContextLoadedOnEncodingContextCommandHandler : IHexDataContextLoadedEventHandler {
        [ImportingConstructor]
        public OnHexDataContextLoadedOnEncodingContextCommandHandler([ImportMany]IEnumerable<IBytesToCharEncoding> bytesToCharEncodings) {
            this._bytesToCharEncodings = bytesToCharEncodings.OrderBy(p => p.Sort).ToArray();
        }

        private IBytesToCharEncoding[] _bytesToCharEncodings;
        public int Sort => 144;

        public bool IsEnabled => true;

        public void Handle(IHexDataContext hexContext) {
            ThreadInvoker.BackInvoke(() => {
                var cmi = CommandItemFactory.CreateNew(null);
                cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_EncodingSelect);
                foreach (var encoding in _bytesToCharEncodings) {
                    cmi.AddChild(CreateEncodingCommand(hexContext, encoding));
                }
                ThreadInvoker.UIInvoke(() => {
                    hexContext.AddContextCommand(cmi);
                    if (_bytesToCharEncodings.Length != 0) {
                        hexContext.BytesToCharEncoding = _bytesToCharEncodings[0];
                    }
                });
            });
        }


        private static ICommandItem CreateEncodingCommand(IHexDataContext hexContext, IBytesToCharEncoding encoding) {
            var cmi = CommandItemFactory.CreateNew(new DelegateCommand(() => {
                hexContext.BytesToCharEncoding = encoding;
            }));
            cmi.Name = encoding.EncodingName;
            return cmi;
        }
    }
}
