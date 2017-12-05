using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Singularity.Contracts.Converters {
    public class IsNullToVisibilityConverter : IValueConverter {
        private static IsNullToVisibilityConverter staticInstance;
        public static IsNullToVisibilityConverter StaticInstance {
            get {
                return staticInstance ??
                    (staticInstance = new IsNullToVisibilityConverter());
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
