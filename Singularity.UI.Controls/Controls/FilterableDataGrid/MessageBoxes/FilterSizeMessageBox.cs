using Singularity.UI.Controls.Controls.FilterableDataGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.Controls.Controls.FilterableDataGrid.MessageBoxes {
    public static class FilterSizeMessageBox {
        public static bool? Show(ref FilterSizeModel fzModel) {
            if (fzModel == null) {
                fzModel = new FilterSizeModel();
            }

            var window = new FilterSizeWindow(fzModel);
            window.ShowDialog();
            fzModel.IsEnabled = window.FilterResult == true;
            return window.FilterResult;
        }
    }
}
