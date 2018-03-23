using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Xml.Linq;

namespace SingularityForensic.App {
    [Export(typeof(ILanguageService))]
    public class LanguageService : ILanguageService {
        private LanguageProvider _currentProvider;
        public LanguageProvider CurrentProvider {
            get => _currentProvider;
            set {
                if(value == _currentProvider) {
                    return;
                }

                _currentProvider = value;
                InitializeLanguageDict();
            }
        }

        private List<LanguageProvider> _allProviders = new List<LanguageProvider>();
        public IEnumerable<LanguageProvider> AllProviders => _allProviders.Select(p => p);
        
        private ResourceDictionaryEx _languageDict;
        public string FindResourceString(string keyName) {
            
            if (string.IsNullOrEmpty(keyName)) {
                throw new ArgumentNullException(nameof(keyName));
            }
 
            if(_languageDict != null) {
                return (_languageDict[keyName] as string)??keyName;
            }

            return keyName;
        }

        public void Initialize() {
            InitializeDocument();
            InitilizeProviders();
            InitilizeCurrentProvider();
        }

        private string ConfigFileName => $"{Environment.CurrentDirectory}/{Constants.LanguageConfigName}";
        //初始化配置文档;
        private void InitializeDocument() {
            //因异常原因还原覆盖语言文件;
            void ResetDoc() {

            };
            
            if (File.Exists(ConfigFileName)) {
                try {
                    _xDoc = XDocument.Load(ConfigFileName);
                }
                catch (Exception ex) {
                    Contracts.App.LoggerService.WriteCallerLine(ex.Message);
                    ResetDoc();
                }
            }
            else {
                ResetDoc();
            }
        }

        private XDocument _xDoc;

        /// <summary>
        /// 初始化语言提供者;
        /// </summary>
        private void InitilizeProviders() {
            if(_xDoc == null) {
                return;
            }
            _allProviders.Clear();
            
            if(_xDoc == null) {
                return;
            }

            var elem = _xDoc.Root.Element(Constants.LanguageProviders);
            if(elem == null) {
                return;
            }

            var providerElems = elem.Elements(Constants.Provider);

            foreach (var pElem in providerElems) {
                var lanName = pElem.GetXElemValue(Constants.ProviderName);
                var lanType = pElem.GetXElemValue(Constants.ProviderType);
                var provider = new LanguageProvider(lanName, lanType);
                _allProviders.Add(provider);
            }
        }

        /// <summary>
        /// 初始化当前语言;
        /// </summary>
        private void InitilizeCurrentProvider() {
            if(_xDoc == null) {
                return;
            }

            var curLan = _xDoc.Root.GetXElemValue(Constants.CurrentLanguage);
            if (string.IsNullOrEmpty(curLan)) {
                return;
            }

            var provider = AllProviders.FirstOrDefault(p => p.Type == curLan);
            if(provider == null) {
                return;
            }

            CurrentProvider = provider;
        }

        /// <summary>
        /// 初始化语言,操作字典等;
        /// </summary>
        private void InitializeLanguageDict() {
            try {
                var languageDict = new DirectoryInfo($"{Environment.CurrentDirectory}/Languages");
                foreach (var direct in languageDict.GetDirectories()) {
                    //_ direct.Name
                } 
            }
            catch(Exception ex) {
                Contracts.App.LoggerService.WriteCallerLine(ex.Message);
                
            }

            if(CurrentProvider == null) {
                return;
            }


            _languageDict = ServiceProvider.Current.GetInstance<ILanguageDictObject>()?.LanguageDict;

            if (_languageDict == null) {
                return;
            }

            _languageDict.MergedDictionaries.Clear();
            var dicts = new List<ResourceDictionary>();
            var providerDirect = $"{Environment.CurrentDirectory}/{Constants.LanguageDirect}/{CurrentProvider.Type}";
            if (!Directory.Exists(providerDirect)) {
                return;
            }

            var di = new DirectoryInfo(providerDirect);
            //遍历添加语言文件;
            foreach (var file in di.GetFiles()) {
                try {
                    using (var rs = File.OpenRead($"{providerDirect}/{file.Name}")) {
                        var res = XamlReader.Load(rs) as ResourceDictionary;
                        _languageDict.MergedDictionaries.Add(res);
                    }
                }
                catch(Exception ex) {
                    Contracts.App.LoggerService.WriteCallerLine(ex.Message);
                }
            }
        }
    }

    //由于测试项目中无Application.Current对象,故单独抽象出一个接口提供被操作的语言资源字典;
    //被操作的语言相关资源字典对象;
    public interface ILanguageDictObject {
        ResourceDictionaryEx LanguageDict { get; }
    }

    [Export(typeof(ILanguageDictObject))]
    public class AppLanguageDictProvider : ILanguageDictObject {
        public ResourceDictionaryEx LanguageDict {
            get {
                return Application.Current.Resources.MergedDictionaries.FirstOrDefault(p => {
                    if (p is ResourceDictionaryEx rsEx) {
                        return rsEx.Name == Constants.LanguageDict;
                    }
                    return false;
                }) as ResourceDictionaryEx;
            }
        }
        
    }
}
