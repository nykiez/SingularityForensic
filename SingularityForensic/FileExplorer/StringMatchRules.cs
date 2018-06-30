using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileExplorer;
using System;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer {
    /// <summary>
    /// 头部包含匹配规则;
    /// </summary>
    [Export(typeof(IStringMatchRule))]
    class StartsWithStringMatchRule : IStringMatchRule {
        public string Type => Constants.StringMatchRuleType_StartsWith;

        public string RuleName => LanguageService.FindResourceString(Constants.StringMatchRuleName_StartsWith);
        
        public bool Match(string keyContent, string stringValue) {
            if(keyContent == null) {
                throw new ArgumentNullException(nameof(keyContent));
            }
            if(stringValue == null) {
                throw new ArgumentNullException(nameof(stringValue));
            }

            return stringValue.StartsWith(keyContent);
        }
    }

    /// <summary>
    /// 尾部包含匹配规则;
    /// </summary>
    [Export(typeof(IStringMatchRule))]
    class EndsWithStringMatchRule : IStringMatchRule {
        public string Type => Constants.StringMatchRuleType_EndsWith;

        public string RuleName => LanguageService.FindResourceString(Constants.StringMatchRuleName_EndsWith);
        
        public bool Match(string keyContent, string stringValue) {
            if (keyContent == null) {
                throw new ArgumentNullException(nameof(keyContent));
            }
            if (stringValue == null) {
                throw new ArgumentNullException(nameof(stringValue));
            }

            return stringValue.EndsWith(keyContent);
        }
    }

    /// <summary>
    /// 包含匹配规则;
    /// </summary>
    [Export(typeof(IStringMatchRule))]
    class ContainsStringMatchRule : IStringMatchRule {
        public string Type => Constants.StringMatchRuleType_Contains;

        public string RuleName => LanguageService.FindResourceString(Constants.StringMatchRuleName_Contains);

        public bool Match(string keyContent, string stringValue) {
            if (keyContent == null) {
                throw new ArgumentNullException(nameof(keyContent));
            }
            if (stringValue == null) {
                throw new ArgumentNullException(nameof(stringValue));
            }

            return stringValue.Contains(keyContent);
        }
    }

    /// <summary>
    /// 全字匹配规则;
    /// </summary>
    [Export(typeof(IStringMatchRule))]
    class IsEqualToMatchRule : IStringMatchRule {
        public string Type => Constants.StringMatchRuleType_IsEqualTo;

        public string RuleName => Constants.StringMatchRuleName_IsEqualTo;

        public bool Match(string keyContent, string stringValue) {
            if (keyContent == null) {
                throw new ArgumentNullException(nameof(keyContent));
            }
            if (stringValue == null) {
                throw new ArgumentNullException(nameof(stringValue));
            }

            return stringValue == keyContent;
        }
    }
}
