using CDFC.Util;
using Singularity.UI.Info.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace Singularity.UI.Info.Android.Views {
    /// <summary>
    /// Interaction logic for AndroidBasicDataGrid.xaml
    /// </summary>
    public partial class AndroidBasicDataGrid : UserControl {
        public AndroidBasicDataGrid() {
            InitializeComponent();
        }

        private void FilterableDataGrid_LoadingRow(object sender, DataGridRowEventArgs e) {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }

    


    public class FromNumbersToNumbersStringConverter : 
        GenericStaticInstance<FromNumbersToNumbersStringConverter>,IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var numbers = value as IEnumerable<ContactDbModel.PhoneNumber>;
            if(numbers != null) {
                var sb = new StringBuilder();
                foreach (var num in numbers) {
                    sb.Append($"{num.Number};");
                }
                return sb.ToString();
            }
            else {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class FromInt64ToDTStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (long.TryParse(value?.ToString(),out var dt)) {

            }
            return "N/A";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

}
