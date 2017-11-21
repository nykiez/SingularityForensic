using System;
using System.Globalization;
using System.Windows.Data;

namespace Singularity.UI.Themes.Converters {
    public class ProgressValueToValueConverter : IValueConverter {
        private static ProgressValueToValueConverter _staticInstance;
        public static ProgressValueToValueConverter StaticInstance => _staticInstance ?? (_staticInstance = new ProgressValueToValueConverter());
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                var val = (double)value;
                if(val == 0) {
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
