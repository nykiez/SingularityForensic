using Singularity.UI.Controls.Controls.FilterableDataGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.Controls.Controls.FilterableDataGrid.MessageBoxes {
    public class FilterDTMessageBox {
        public static bool? Show(ref FilterDateModel fdtModel) {
            if(fdtModel == null) {
                fdtModel = new FilterDateModel();
            }

            var window = new FilterDateTimeWindow(fdtModel);
            window.ShowDialog();
            fdtModel.IsEnabled = window.FilterResult == true; 
            return window.FilterResult;
        }
    }
}
