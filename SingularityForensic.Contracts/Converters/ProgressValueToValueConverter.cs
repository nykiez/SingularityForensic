using CDFC.Util;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SingularityForensic.Contracts.Converters {
    public class ProgressValueToValueConverter : GenericStaticInstance<ProgressValueToValueConverter>, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                var val = (double)value;
                if (val == 0) {
                    return string.Empty;
                }
                else {
                    return $"{val}%";
                }
            }
            catch {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
