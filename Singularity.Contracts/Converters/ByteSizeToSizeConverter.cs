using CDFC.Util;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Singularity.Contracts.Converters {
    public class ByteSizeToSizeConverter : GenericStaticInstance<ByteSizeToSizeConverter>,IValueConverter {
        public string Convert(long size) {
            return Convert(size, null, null, null).ToString();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            ulong.TryParse(value != null?value.ToString():"0", out var sizeInByte);
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
                    return $"{sizeInByte} B";
                    
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
