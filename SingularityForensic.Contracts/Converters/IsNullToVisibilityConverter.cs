using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SingularityForensic.Contracts.Converters {
    public class IsNullToVisibilityConverter : IValueConverter {
        public static readonly IsNullToVisibilityConverter StaticInstance = new IsNullToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
