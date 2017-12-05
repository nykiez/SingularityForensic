using CDFC.Util;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Singularity.Contracts.Converters {
    public class BooleanReverseConverter : GenericStaticInstance<BooleanReverseConverter>,IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                return !(bool)value;
            }
            catch {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
