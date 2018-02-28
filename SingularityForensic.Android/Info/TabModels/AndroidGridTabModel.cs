using SingularityForensic.Contracts.TabControl;
using SingularityForensic.Android.Info.ViewModels;
using SingularityForensic.Android.Info.Views;
using SingularityForensic.Info.Models;
using System;
using System.Collections.Generic;

namespace SingularityForensic.Android.Info.TabModels {
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
