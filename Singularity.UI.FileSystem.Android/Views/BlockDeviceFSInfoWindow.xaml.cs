using CDFCControls.Controls;
using Singularity.UI.FileSystem.Android.ViewModels;
using Singularity.UI.FileSystem.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Singularity.UI.FileSystem.Android.Views {
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
                    var unit = element.DataContext as FSTreeUnit;
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

    public class UnitLevelToMarginConverter : IValueConverter {
        private static UnitLevelToMarginConverter staticInstance;
        public static UnitLevelToMarginConverter StaticInstance {
            get {
                return staticInstance ??
                    (staticInstance = new UnitLevelToMarginConverter());
            }
        }

        

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            byte unitLevel;
            if (value != null && byte.TryParse(value.ToString(),out unitLevel)) {
                return new Thickness(unitLevel * 12, 0, 0, 0);
            }
            return new Thickness(0, 0, 0, 0);
        }

        
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
