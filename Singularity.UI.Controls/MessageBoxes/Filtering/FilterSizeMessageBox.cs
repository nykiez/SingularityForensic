using Singularity.UI.Controls.Models.Filtering;
using Singularity.UI.Controls.Windows.Filtering;

namespace Singularity.UI.Controls.MessageBoxes.Filtering {
    public static class FilterSizeMessageBox {
        public static bool? Show(ref FilterSizeModel fzModel) {
            if (fzModel == null) {
                fzModel = new FilterSizeModel();
            }

            var window = new FilterSizeWindow(fzModel);
            window.ShowDialog();
            return window.FilterResult;
        }
    }
}
