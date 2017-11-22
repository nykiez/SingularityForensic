using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Singularity.UI.Converters {
    public class LevelToMarginConverter : IValueConverter {
        private static LevelToMarginConverter _staticInstance;
        public static LevelToMarginConverter StaticInstance => _staticInstance ??
            (_staticInstance = new LevelToMarginConverter());

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int direction = 0;
            int level = 0;
            if(int.TryParse(parameter?.ToString()??"0",out direction) && int.TryParse(value?.ToString()??"0",out level)) {
                switch (direction) {
                    case 0:
                        return new Thickness(level * 12, 0, 0, 0);
                    case 1:
                        return new Thickness(0, level * 12, 0, 0);
                    case 2:                     
                        return new Thickness(0, 0, level * 12, 0);
                    case 3:                     
                        return new Thickness(0, 0, 0, level * 12);
                    default:                    
                        return new Thickness(level * 12, 0, 0, 0);
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
