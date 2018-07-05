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
        /// <summary>
        /// 语言名称(比如简体中文);
        /// </summary>
        public string LanguageName { get; }
        /// <summary>
        /// 类型;比如(zh_CN,en_US)
        /// </summary>
        public string Type { get; }
    }

    /// <summary>
    /// 语言服务契约;
    /// </summary>
    public interface ILanguageService {
        //找寻资源字符串;
        string FindResourceString(string keyName);
        
        /// <summary>
        /// 当前语言类型;
        /// </summary>
        LanguageProvider CurrentProvider { get; set; }

        /// <summary>
        /// 所有语言;
        /// </summary>
        IEnumerable<LanguageProvider> AllProviders { get; }

        /// <summary>
        /// 初始化;
        /// </summary>
        void Initialize();
    }

    /// <summary>
    /// 由于测试项目中无Application.Current对象,故单独抽象出一个接口提供被操作的语言资源字典;
    ///  被操作的语言相关资源字典对象;
    /// </summary>
    public interface ILanguageDict {
        string this[string keyName] { get; }
        /// <summary>
        /// 清除所有合并后字典;
        /// </summary>
        void ClearMergedDictionaries();
        /// <summary>
        /// 从指定的绝对路径读取资源字典,并合并;
        /// </summary>
        /// <param name="path"></param>
        void AddMergedDictionaryFromPath(string path);
    }
    public class LanguageService : GenericServiceStaticInstance<ILanguageService> {
        public static string FindResourceString(string keyName) => Current?.FindResourceString(keyName);
    }

    public static class LanguageServiceExtensions {
        /// <summary>
        /// 尝试根据指定格式的值与参数获取字符串内容,适用于句势具有动态性的语言查找;
        /// </summary>
        /// <param name="languageService"></param>
        /// <param name="languageFormatKey">语言格式键值(比如"{0}是哲学家")</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string TryGetStringWithFormat(this ILanguageService languageService,string languageFormatKey, params object[] args) {
            if(languageService == null) {
                throw new ArgumentNullException(nameof(languageService));
            }
            
            try {
#if DEBUG
                var format = LanguageService.FindResourceString(languageFormatKey);
                return string.Format(format, args);
#endif
                return string.Format(LanguageService.FindResourceString(languageFormatKey), args);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                return languageService.FindResourceString(languageFormatKey);
            }
        }
    }
}
