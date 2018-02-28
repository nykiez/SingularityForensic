using CDFCUIContracts.Abstracts;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Controls.ViewModels;
using SingularityForensic.Hex.ViewModels;
using System.IO;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Adb.ViewModels.AdbGrid {
    public class AdbFileHexStreamTabViewModel : HexStreamEditorViewModel, ITabModel {
        public AdbFileHexStreamTabViewModel(Stream stream = null)  {
            this.Stream = stream;
        }

        public string Header => ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("AdbTabHex");
    }
}
