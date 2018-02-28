using Singularity.UI.MessageBoxes.Models.Filtering;
using Singularity.UI.MessageBoxes.Windows.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.MessageBoxes.MessageBoxes.Filtering {
    //过滤字符串讯息;
    public static class FilterStringMessageBox {
        public static bool? Show(ref FilterStringModel fsModel) {
            if(fsModel == null) {
                fsModel = new FilterStringModel();
            }

            var window = new FilterStringWindow(fsModel);
            window.ShowDialog();
            fsModel.IsEnabled = window.FilterResult == true;
            return window.FilterResult;
        }
    }
}
