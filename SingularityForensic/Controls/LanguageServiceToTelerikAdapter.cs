using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace SingularityForensic.Controls
{
    /// <summary>
    /// Telerik语言服务适配器;
    /// </summary>
    public class LanguageServiceToTelerikAdapter : LocalizationManager {
        public override string GetStringOverride(string key) {
            return LanguageService.Current?.FindResourceString(key);
        }
    }
}
