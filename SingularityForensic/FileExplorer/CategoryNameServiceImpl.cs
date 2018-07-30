using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.Helpers;

namespace SingularityForensic.FileExplorer {
    [Export(typeof(INameCategoryService))]
    class NameCategoryServiceImpl : INameCategoryService {
        [ImportingConstructor]
        public NameCategoryServiceImpl([ImportMany]IEnumerable<IStringMatchRule> stringMatchRules) {
            this._stringRecognizers =  stringMatchRules.Select(p => new NameCategoryRecognizer(p)).ToArray();
        }

        private readonly IEnumerable<NameCategoryRecognizer> _stringRecognizers;

        public ICategoryDescriptor GetNameCategory(string name) {
            if(_stringRecognizers == null) {
                return null;
            }
            foreach (var recognizer in _stringRecognizers) {
                try {
                    var descriptor = recognizer.CategoryDescriptors.FirstOrDefault(p => recognizer.Rule.Match(p.Key, name));
                    if(descriptor != null) {
                        return descriptor;
                    }
                }
                catch(Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            return null;
        }

        public void LoadDescriptorsFromFile(string fileName) {
            try {
                LoadDescriptorInternal(fileName);
                CommonEventHelper.GetEvent<NameCategoryDescriptorsLoadedEvent>().Publish();
                CommonEventHelper.PublishEventToHandlers(GenericServiceStaticInstances<INameCategoryDescriptorsLoadedEventHandler>.Currents);
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                throw;
            }
        }
        
        private void LoadDescriptorInternal(string fileName) {
            if (string.IsNullOrEmpty(fileName)) {
                throw new ArgumentNullException(nameof(fileName));
            }
            
            if (!File.Exists(fileName)) {
                throw new FileNotFoundException($"Can't find file:{fileName}");
            }

            if (_stringRecognizers == null) {
                LoggerService.WriteCallerLine($"{nameof(_stringRecognizers)} can't be null.");
                return;
            }

            ClearDescriptors();

            StreamReader sr = null;
            string line = null;
            string caption = null;
            NameCategoryRecognizer currentRecognizer = null;

            try {
                sr = new StreamReader(fileName, AppService.Current.AppEncoding);
                //逐行读取至文件尾;
                while ((line = sr.ReadLine()) != null) {
                    //若为概述类别声明行,检查头部包含且不相等;
                    if (line.StartsWith(Constants.TextLine_CategoryCaptionPrefix) &&
                        line.Length != Constants.TextLine_CategoryCaptionPrefix.Length) {
                        caption = line.Substring(Constants.TextLine_CategoryCaptionPrefix.Length);
                        continue;
                    }

                    //若为-开头，为规则声明起始行,直到下一个规则声明行前,均为类型描述行(若以\t开头);
                    if (line.StartsWith(Constants.TextLine_CategoryRulePrefix)) {
                        var tryRecognizer = _stringRecognizers.FirstOrDefault(p => p.Rule.Type == line.Substring(1)?.Trim());
#if DEBUG
                        var ss = line.Substring(1);
                        var tryRecognizer2 = _stringRecognizers.FirstOrDefault(p => p.Rule.Type == ss);
#endif
                        if (tryRecognizer == null) {
                            LoggerService.WriteCallerLine($"{line} is not matched.");
                        }
                        else {
                            currentRecognizer = tryRecognizer;
                        }
                        continue;
                    }

                    //若为类型描述行;尝试解析为键,类型名,添加内容;
                    if (line.StartsWith(Constants.TextLine_CategoryDetailPrefix)) {
                        //若尚无规则声明起始行,则跳过;
                        if (currentRecognizer == null) {
                            LoggerService.WriteCallerLine($"Can't deal with {line} while {nameof(currentRecognizer)} is null");
                            continue;
                        }
                        //若尚无概述类别声明起始行,则跳过;
                        if (string.IsNullOrEmpty(caption)) {
                            LoggerService.WriteCallerLine($"Can't deal with {line} while {nameof(caption)} is null or empty");
                            continue;
                        }

                        var spaceIndex = line.IndexOf(' ');
                        if (spaceIndex < 2) {
                            continue;
                        }
                        //如若以空格符结尾,则表示空格符后无字符,跳过;
                        if (spaceIndex == line.Length - 1) {
                            continue;
                        }
                        var categoryName = line.Substring(spaceIndex + 1);
                        var key = line.Substring(1, spaceIndex - 1);

                        var descriptor = new CategoryDescriptor(key, categoryName, caption);
                        currentRecognizer.CategoryDescriptors.Add(descriptor);
                    }

                }

            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
            finally {
                sr?.Dispose();
            }
        }

        /// <summary>
        /// 清除描述单位;
        /// </summary>
        private void ClearDescriptors() {
            if (_stringRecognizers == null) {
                LoggerService.WriteCallerLine($"{nameof(_stringRecognizers)} can't be null.");
                return;
            }

            foreach (var recognizer in _stringRecognizers) {
                recognizer.CategoryDescriptors.ForEach(p => p.IsExpired = true);
                recognizer.CategoryDescriptors.Clear();
            }

        }

        public void Initialize() {
            try {
                //从配置文件中读取有关内容;
                var descriptorFile = AppService.Current.GetSettingValue(Constants.ConfigKey_CategoryDescriptorFile);
                //若无值或值指向地址不存在，则加载默认值;
                if(descriptorFile == null || !File.Exists(descriptorFile)) {
                    descriptorFile = $"{AppService.AppResourceFolder}\\{Constants.ConfigValue_CategoryDescriptorDefaultFile}";
                    if (!File.Exists(descriptorFile)) {
                        LoggerService.WriteCallerLine($"{nameof(descriptorFile)} doesn't exist.");
                        return;
                    }
                    //设定默认值;
                    AppService.Current.SetSettingValue(Constants.ConfigKey_CategoryDescriptorFile, descriptorFile);
                    LoadDescriptorInternal(descriptorFile);
                }
                else{
                    LoadDescriptorInternal(descriptorFile);
                }
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
        }

    }

    /// <summary>
    /// 识别者;
    /// </summary>
    class NameCategoryRecognizer {
        public NameCategoryRecognizer(IStringMatchRule rule) {
            this.Rule = rule??throw new ArgumentNullException(nameof(rule));
        }

        public IStringMatchRule Rule { get; }
        /// <summary>
        /// 所有分类描述节点;
        /// </summary>
        public List<CategoryDescriptor> CategoryDescriptors { get; } =  new List<CategoryDescriptor>();
    }

    /// <summary>
    /// 键-类别实体类;
    /// </summary>
    class CategoryDescriptor : ICategoryDescriptor {
        public CategoryDescriptor(string key, string categoryName,string categoryCaption) {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
            this.CategoryName = categoryName ?? throw new ArgumentNullException(nameof(categoryName));
            this.CategoryCaption = categoryCaption ?? throw new ArgumentNullException(nameof(categoryCaption));
        }

        public string Key { get; }

        public string CategoryName { get; }

        public string CategoryCaption {get;}

        public bool IsExpired { get; internal set; }
    }
}
