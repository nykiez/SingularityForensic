using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Controls.Hex.Models {
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
