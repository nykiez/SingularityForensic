using SingularityForensic.Contracts.App;
using Telerik.Windows.Controls;

namespace SingularityForensic.Controls {
    /// <summary>
    /// Telerik语言服务适配器;
    /// </summary>
    public class LanguageServiceToTelerikAdapter : LocalizationManager {
        public override string GetStringOverride(string key) {
            return LanguageService.Current?.FindResourceString(key);
        }
    }
}
