using CDFCUIContracts.Abstracts;
using Singularity.UI.Controls.ViewModels;
using System.IO;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.AdbViewer.ViewModels.AdbGrid {
    public class AdbFileHexStreamTabViewModel : HexStreamEditorViewModel, ITabModel {
        public AdbFileHexStreamTabViewModel(Stream stream = null) : base(stream) {

        }

        public string Header => FindResourceString("AdbTabHex");
    }
}
