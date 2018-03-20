using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Contracts.App {
    //语言提供者;
    public class LanguageProvider {
        public LanguageProvider(string languageName,string lanType) {
            this.LanguageName = languageName;
            Type = lanType;
        }
        //语言名称(比如简体中文);
        public string LanguageName { get; }
        //类型;比如(zh_CN,en_US)
        public string Type { get; }
    }

    //语言服务;
    public interface ILanguageService {
        //找寻资源字符串;
        string FindResourceString(string keyName);
        
        /// <summary>
        /// 当前语言类型;
        /// </summary>
        LanguageProvider CurrentProvider { get; set; }

        //所有语言;
        IEnumerable<LanguageProvider> AllProviders { get; }

        /// <summary>
        /// 初始化;
        /// </summary>
        void Initialize();

        ////重新加载语言;
        //void ReloadLanguage(string languageName);

        //语言正在重载事件;
        //event EventHandler<string> LanguageReloading;

        //加入字典;
        //void AddResourceDictionary(ResourceDictionary dic);
    }

    public class LanguageService: GenericServiceStaticInstance<ILanguageService> {
        public static string FindResourceString(string keyName) => Current?.FindResourceString(keyName);
    }
}
