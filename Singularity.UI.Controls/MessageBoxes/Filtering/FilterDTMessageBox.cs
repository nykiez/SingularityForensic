using Singularity.UI.Controls.Models.Filtering;
using Singularity.UI.Controls.Windows.Filtering;

namespace Singularity.UI.Controls.Filtering {
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
