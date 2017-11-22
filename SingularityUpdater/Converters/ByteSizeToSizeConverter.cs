using System;
using System.Globalization;
using System.Windows.Data;

namespace SingularityUpdater.Converters {
    /// <summary>
    /// 字节大小转为对应的适当大小表示字符串,(受品牌影响)
    /// </summary>
    public class ByteSizeToSizeConverter : IValueConverter {
        private static ByteSizeToSizeConverter staticInstance;
        public static ByteSizeToSizeConverter StaticInstance {
            get {
                return staticInstance ??
                    (staticInstance = new ByteSizeToSizeConverter());
            }
        }

        public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
            ulong sizeInByte = System.Convert.ToUInt64(value);
            if (parameter == null) {
                if (sizeInByte >= 1075741824) {
                    return string.Format("{0:F2} GB", (double)sizeInByte / 1075741824);
                }
                else if (sizeInByte >= 1048576) {
                    return string.Format("{0:F2} MB", (double)sizeInByte / 1048576);
                }
                else if (sizeInByte >= 1024) {
                    return string.Format("{0:F2} KB", (double)sizeInByte / 1024);
                }
                else {
                    return "0 KB";
                }
            }
            else {
                if (sizeInByte >= 1075741824) {
                    return string.Format("{0} GB", sizeInByte / 1075741824);
                }
                else if (sizeInByte >= 1048576) {
                    return string.Format("{0} MB", sizeInByte / 1048576);
                }
                else if (sizeInByte >= 1024) {
                    return string.Format("{0} KB", sizeInByte / 1024);
                }
                else {
                    return "0 KB";
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
