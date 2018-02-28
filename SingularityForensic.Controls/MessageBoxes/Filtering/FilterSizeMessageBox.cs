using SingularityForensic.Controls.Models.Filtering;
using SingularityForensic.Controls.Windows.Filtering;

namespace SingularityForensic.Controls.MessageBoxes.Filtering {
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
