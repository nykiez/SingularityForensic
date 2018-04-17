using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Hex.Models {
    /// <summary>
    /// 搜索方式;
    /// </summary>
    public enum SearchMethod {
        JustFileName,
        Content
    }
    /// <summary>
    /// 搜索选项;
    /// </summary>
    public class SearchKeyOption {

        public SearchMethod Method { get; set; }
        public string SearchWord {
            get {
                switch (Method) {
                    case SearchMethod.Content:
                        return  ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("SearchMContent");
                    case SearchMethod.JustFileName:
                        return  ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("SearchMJustFileName");
                }
                return string.Empty;
            }
        }
    }
}
