using Singularity.UI.Info.Android.ViewModels;
using Singularity.UI.Info.Android.Views;
using Singularity.UI.Info.Models;
using SingularityForensic.Modules.MainPage.Models;
using System;
using System.Collections.Generic;

namespace Singularity.UI.Info.Android.TabModels {
    /// <summary>
    /// 基本列表视图Tab;
    /// </summary>
    public class AndroidGridTabModel<TDbModel> : ExtTabModel<IEnumerable<TDbModel>> where TDbModel:ForensicInfoDbModel  {
        private string tabPinKind;
        private IEnumerable<TDbModel> data;
        
        public AndroidGridTabModel (string pinKind, IEnumerable<TDbModel> dbModels)
            :base(dbModels,pinKind){
            if(dbModels == null) {
                throw new ArgumentNullException(nameof(dbModels));
            }
            Content = new AndroidBasicGrid {
                DataContext = new AndroidGridViewModel<TDbModel>(dbModels)
            };
        }
        
        
    }
}
