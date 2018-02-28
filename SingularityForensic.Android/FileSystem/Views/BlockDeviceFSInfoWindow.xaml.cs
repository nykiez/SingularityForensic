using CDFCControls.Controls;
using SingularityForensic.Android.FileSystem.Models;
using SingularityForensic.Android.FileSystem.MessageBoxes.ViewModels;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CDFC.Util;

namespace SingularityForensic.Android.FileSystem.Views {
    /// <summary>
    /// Interaction logic for BlockDeviceFSInfoWindow.xaml
    /// </summary>
    public partial class BlockDeviceFSInfoWindow : CorneredWindow {
        public BlockDeviceFSInfoWindow(BlockDeviceFSInfoViewModel vm) {
            InitializeComponent();
            if (vm == null)
               throw new ArgumentNullException(nameof(vm));


            this.DataContext = vm;
            this.VM = vm;
        }

        public BlockDeviceFSInfoViewModel VM { get; }
        private void TreeList_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 1 && VM != null) {
                var element = e.OriginalSource as FrameworkElement;
                if (element != null) {
                    var unit = element.DataContext as TreeUnit;
                    if (unit != null) {
                        if (e.LeftButton == MouseButtonState.Pressed) {
                            VM.NotifySelectionUnitChanged(unit);
                        }
                        else {
                            VM.SelectedUnit = unit;
                        }
                        
                    }
                }
            }
        }
        
    }

    public class UnitLevelToMarginConverter : GenericStaticInstance<UnitLevelToMarginConverter>,IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value != null && byte.TryParse(value.ToString(), out byte unitLevel)) {
                return new Thickness(unitLevel * 12, 0, 0, 0);
            }
            return new Thickness(0, 0, 0, 0);
        }

        
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
