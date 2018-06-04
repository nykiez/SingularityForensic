using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Contracts.Controls {
    public class GridChildLength {
        public GridLength GridLength { get; set; }
        public double MinLength { get; set; }
        public double MaxLength { get; set; }
        
        public GridChildLength(GridLength length, double min = 0.0, double max = double.PositiveInfinity) {
            GridLength = length;
            MinLength = min;
            MaxLength = max;
        }

        private static GridChildLength _auto;
        public static GridChildLength Auto => _auto ?? (_auto = new GridChildLength(GridLength.Auto));
    }


}
