using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace SingularityForensic.Hex.Events {
    /// <summary>
    /// 加入十六进制默认右键以及快捷键绑定;
    /// </summary>
    [Export(typeof(IHexDataContextLoadedEventHandler))]
    public class OnHexDataContextLoadedOnContextCommandsHandler : IHexDataContextLoadedEventHandler {
        public int Sort => 128;

        public bool IsEnabled => true;
        
        public void Handle(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }
            
            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateSetAsStartCommandItem(hexDataContext));
            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateSetAsEndCommandItem(hexDataContext));
            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateCopyToNewFileCommandItem(hexDataContext));
            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateCopyToClipBoardCommandItem(hexDataContext));
            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateCopyToCopyHexToCBoardCommandItem(hexDataContext));
            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateCopyAsProCodeCommandItem(hexDataContext));
        }

        private void AddKeyBindingsToHexDataContext(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }

            hexDataContext.AddKeyBinding(HexDataContextCommandFactory.CreateCopyToClipBoardCommand(hexDataContext), Key.C, ModifierKeys.Control);
            hexDataContext.AddKeyBinding(HexDataContextCommandFactory.CreateCopyToCopyHexToCBoardCommand(hexDataContext), Key.C, ModifierKeys.Control | ModifierKeys.Shift);
        }

        private static void AddContextCommandsToHexDataContext(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }

            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateSetAsStartCommandItem(hexDataContext));
            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateSetAsEndCommandItem(hexDataContext));
            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateCopyToNewFileCommandItem(hexDataContext));
            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateCopyToClipBoardCommandItem(hexDataContext));
            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateCopyToCopyHexToCBoardCommandItem(hexDataContext));
            hexDataContext.AddContextCommand(HexDataContextCommandFactory.CreateCopyAsProCodeCommandItem(hexDataContext));

        }
    }
}
