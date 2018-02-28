using Singularity.UI.MessageBoxes.Models.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.MessageBoxes.MessageBoxes.Filtering {
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
