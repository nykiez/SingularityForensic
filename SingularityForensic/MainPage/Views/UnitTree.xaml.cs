using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;
using System.ComponentModel.Composition;
using CDFCUIContracts.Helpers;
using CDFC.Util;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.MainPage.ViewModels;

namespace SingularityForensic.MainPage.Views {
    /// <summary>
    /// Interaction logic for CaseManager.xaml
    /// </summary>
    [Export(Contracts.MainPage.Constants.UnitTreeView)]
    public partial class UnitTree : UserControl {
        public UnitTree() {
            InitializeComponent();
        }

        
        private void CaseTreeList_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 1 && DataContext is UnitTreeViewModel vm) {
                if (e.OriginalSource is DependencyObject dpo) {
                    object dt = null;
                    if (dpo is FrameworkElement element) {
                        dt = element.DataContext;
                    }
                    else {
                        dt = VisualHelper.GetVisualParent<FrameworkElement>(dpo)?.DataContext;
                    }

                    if (dt is ITreeUnit unit) {
                        if (unit is ITreeUnit tUnit) {
                            //通知右键点击;
                            if (e.RightButton == MouseButtonState.Pressed) {
                                vm.NotifyRightClick(tUnit);
                            }
                        }

                    }
                }
            }
        }
    }

    ///// <summary>
    ///// 根据节点类型动态选择上下文菜单;
    ///// </summary>
    //public class LevelToMarginConverter : GenericStaticInstance<LevelToMarginConverter>,IValueConverter {
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
    //        if (value != null && int.TryParse(value.ToString(), out int level)) {
    //            return new Thickness(level * 16, 0, 0, 0);
    //        }
    //        return new Thickness(0, 0, 0, 0);
    //    }


    //    public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture) {
    //        throw new NotImplementedException();
    //    }

    //    //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
    //    //    throw new NotImplementedException();
    //    //}
    //}
    
}
