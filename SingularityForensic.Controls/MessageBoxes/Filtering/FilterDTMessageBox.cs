using SingularityForensic.Controls.Models.Filtering;
using SingularityForensic.Controls.Windows.Filtering;

namespace SingularityForensic.Controls.Filtering {
    public class FilterDTMessageBox {
        public static bool? Show(ref FilterDateModel fdtModel) {
            if(fdtModel == null) {
                fdtModel = new FilterDateModel();
            }

            var window = new FilterDateTimeWindow(fdtModel);
            window.ShowDialog();
            return window.FilterResult;
        }
    }
}
