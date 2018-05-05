using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Contracts.Controls {
    public struct GridChildLength {
        public GridLength GridLength { get; set; }
        public double MinLength { get; set; }
        public double MaxLength { get; set; }

        public GridChildLength(GridLength length, double min = double.NaN, double max = double.NaN) {
            GridLength = length;
            MinLength = min;
            MaxLength = max;
        }
    }


}
