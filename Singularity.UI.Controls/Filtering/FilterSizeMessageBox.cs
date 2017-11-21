using Singularity.UI.MessageBoxes.Models.Filtering;
using Singularity.UI.MessageBoxes.Windows.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.MessageBoxes.MessageBoxes.Filtering {
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
