using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.App {
    [Export(typeof(ILanguageService))]
    public class LanguageService : ILanguageService {
        public string FindResourceString(string keyName) {
            return CDFCCultures.Managers.ManagerLocator.FindResourceString(keyName);
        }

        public void ReloadLanguage(string languageName) {
            throw new NotImplementedException();
        }
    }
}
