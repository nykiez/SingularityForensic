using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.FileExplorer.Models {
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
                        return FindResourceString("SearchMContent");
                    case SearchMethod.JustFileName:
                        return FindResourceString("SearchMJustFileName");
                }
                return string.Empty;
            }
        }
    }
}
