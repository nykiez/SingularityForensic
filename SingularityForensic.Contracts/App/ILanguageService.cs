using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.App {
    //语言服务;
    public interface ILanguageService {
        //找寻资源字符串;
        string FindResourceString(string keyName);
        //重新加载语言;
        void ReloadLanguage(string languageName);
    }

    public class LanguageService: GenericServiceStaticInstance<ILanguageService> {
        public static string FindResourceString(string keyName) => Current?.FindResourceString(keyName);
    }
}
