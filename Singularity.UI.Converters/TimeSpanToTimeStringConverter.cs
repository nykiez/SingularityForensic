using System;
using System.Globalization;
using System.Windows.Data;

namespace Singularity.UI.Converters {
    public class TimeSpanToTimeStringConverter : IValueConverter {
        private static TimeSpanToTimeStringConverter staticInstance;
        public static TimeSpanToTimeStringConverter StaticInstance {
            get {
                return staticInstance ??
                    (staticInstance = new TimeSpanToTimeStringConverter());
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            TimeSpan ts = (TimeSpan)value;
            return (ts.Days == 0 ? string.Empty : (ts.Days + "-")) + string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
