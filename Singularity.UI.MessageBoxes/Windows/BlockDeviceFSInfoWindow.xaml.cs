using MahApps.Metro.Controls;
using Singularity.UI.MessageBoxes.Models;
using Singularity.UI.MessageBoxes.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Singularity.UI.MessageBoxes.Windows {
    /// <summary>
    /// Interaction logic for DeviceFSInfoWindow.xaml
    /// </summary>
    public partial class BlockDeviceFSInfoWindow : MetroWindow {
        private BlockDeviceFSInfoViewModel vm;
        public BlockDeviceFSInfoWindow(BlockDeviceFSInfoViewModel vm) {
            InitializeComponent();
            if (vm == null)
                throw new ArgumentNullException(nameof(vm));

            this.vm = vm;
        }
        
        private void TreeList_PreviewMouseDown(object sender, MouseButtonEventArgs e) {

        }
    }

    public class UnitTypeToMarginConverter : IMultiValueConverter {
        private static UnitTypeToMarginConverter staticInstance;
        public static UnitTypeToMarginConverter StaticInstance {
            get {
                return staticInstance ??
                    (staticInstance = new UnitTypeToMarginConverter());
            }
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            TreeUnitType unitType;
            if (values != null && values.Length >= 2 && Enum.TryParse<TreeUnitType>(values[0].ToString(), out unitType)) {
                switch (unitType) {
                    case TreeUnitType.Case:
                    case TreeUnitType.Device:
                        return new Thickness(16, 0, 0, 0);
                    case TreeUnitType.Partition:
                        return new Thickness((int)unitType * 16 - 13, 0, 0, 0);
                    default:
                        int dirLevel = 0;
                        if (int.TryParse(values[1].ToString(), out dirLevel)) {
                            return new Thickness(((int)unitType + dirLevel) * 16 - 13, 0, 0, 0);
                        }
                        return new Thickness((int)unitType * 16 - 13, 0, 0, 0);
                }
            }
            return new Thickness(0, 0, 0, 0);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            TreeUnitType unitType;
            if (value != null && Enum.TryParse<TreeUnitType>(value.ToString(), out unitType)) {
                switch (unitType) {
                    case TreeUnitType.Case:
                    case TreeUnitType.Device:
                        return new Thickness(16, 0, 0, 0);
                    default:
                        return new Thickness((int)unitType * 16 - 13, 0, 0, 0);
                }
            }
            return new Thickness(0, 0, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        //    throw new NotImplementedException();
        //}
    }
}
