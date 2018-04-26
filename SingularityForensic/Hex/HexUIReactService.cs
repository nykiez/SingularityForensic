using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace SingularityForensic.Hex {
    [Export]
    public partial class HexUIReactService : IUIReactService{
        public void Initialize() {
            RegisterEvents();
        }

        private void RegisterEvents() {
           

            PubEventHelper.GetEvent<HexDataContextLoadedEvent>().Subscribe(hexDataContext => {
                if(hexDataContext == null) {
                    return;
                }
                AddKeyBindingsToHexDataContext(hexDataContext);
                AddContextCommandToHexDataContext(hexDataContext);
            });
        }

        private void AddKeyBindingsToHexDataContext(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }

            hexDataContext.AddKeyBinding(HexDataContextCommandFactory.CreateCopyToClipBoardCommand(hexDataContext), Key.C, ModifierKeys.Control);
            hexDataContext.AddKeyBinding(HexDataContextCommandFactory.CreateCopyToCopyHexToCBoardCommand(hexDataContext), Key.C, ModifierKeys.Control | ModifierKeys.Shift);
        }

        private static void AddContextCommandToHexDataContext(IHexDataContext hexDataContext) {
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
