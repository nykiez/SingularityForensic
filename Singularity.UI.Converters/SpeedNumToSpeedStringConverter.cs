using System;
using System.Globalization;
using System.Windows.Data;

namespace Singularity.UI.Converters {
    public class SpeedNumToSpeedStringConverter : IValueConverter {
        private static SpeedNumToSpeedStringConverter staticInstance;
        public static SpeedNumToSpeedStringConverter StaticInstance {
            get {
                return staticInstance ??
                    (staticInstance = new SpeedNumToSpeedStringConverter());
            }
        }

        private static string speedSuffixInKB = "KB/S";
        private static string speedSuffixInMB = "MB/S";
        private static string speedSuffixInGB = "GB/S";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            //Get Speed Num in with the unit byte;
            ulong speedNum = 0;
            float speedFloatNum;

            ulong.TryParse(value.ToString(), out speedNum);

            if (speedNum > 1073741824) {
                speedFloatNum = System.Convert.ToSingle(speedNum) /
                    (float)1024 / (float)1024 / (float)1024;
                return string.Format("{0:F1}{1}", speedFloatNum, speedSuffixInGB);
            }
            else if (speedNum > 1048576) {
                speedFloatNum = System.Convert.ToSingle(speedNum) /
                    System.Convert.ToSingle(1048576);
                return string.Format("{0:F1}{1}", speedFloatNum, speedSuffixInMB);
            }
            else if (speedNum > 1024) {
                speedFloatNum = System.Convert.ToSingle(speedNum) /
                    System.Convert.ToSingle(1024);
                return string.Format("{0:F1}{1}", speedFloatNum, speedSuffixInKB);
            }
            else {
                return "0.0KB/S";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
