using Singularity.UI.Controls.Models.Filtering;
using Singularity.UI.Controls.Windows.Filtering;

namespace Singularity.UI.Controls.MessageBoxes.Filtering {
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
