using System;
using System.Globalization;
using System.Windows.Data;

namespace SingularityForensic.Contracts.Converters {
    public class DateTimeConverter : IValueConverter {
        public static readonly DateTimeConverter StaticInstance = new DateTimeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
           DateTime? dateTime = (DateTime?)value;
           if (dateTime != null && dateTime.HasValue) {
                var val = dateTime.Value;
                return string.Format("{0:D4}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2}",
                    val.Year, val.Month, val.Day, val.Hour, val.Minute, val.Second);
            }
            else {
                return "N/A";// string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        public static DateTime IniDateTime { get; } = DateTime.Parse("1970/01/01");
        //从纪元时间的时间戳转换;
        public static DateTime ConvertFromIniTS(long tick) {
            try {
                return IniDateTime.AddSeconds(tick);
            }
            catch(Exception ex) {
                
                return default;
            }
        }
    }
}
