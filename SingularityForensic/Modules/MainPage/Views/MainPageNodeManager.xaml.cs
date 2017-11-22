﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;
using System.ComponentModel.Composition;
using CDFCUIContracts.Helpers;
using CDFCUIContracts.Models;
using SingularityForensic.ViewModels.Modules.MainPage.ViewModels;
using CDFC.Util;

namespace SingularityForensic.Modules.MainPage.Views {
    /// <summary>
    /// Interaction logic for CaseManager.xaml
    /// </summary>
    [Export(nameof(MainPageNodeManager))]
    public partial class MainPageNodeManager : UserControl {
        public MainPageNodeManager() {
            InitializeComponent();
        }

        [Import]
        private MainPageNodeManagerViewModel vm {
            set {
                this.DataContext = value;
                value.NotifyUnitExpanded += (d, unit) => {
                    var ti = CaseTreeList.GetContainerFromItemEx(unit);
                    if (ti != null) {
                        ti.IsExpanded = true;
                        ti.UpdateLayout();
                    }
                };
            }
        }
        
        private void CaseTreeList_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 1 && DataContext is MainPageNodeManagerViewModel vm) {
                if (e.OriginalSource is DependencyObject dpo) {
                    object dt = null;
                    if (dpo is FrameworkElement element) {
                        dt = element.DataContext;
                    }
                    else {
                        dt = VisualHelper.GetVisualParent<FrameworkElement>(dpo)?.DataContext;
                    }

                    if (dt is ITreeUnit unit) {
                        if (e.LeftButton == MouseButtonState.Pressed) {
                            vm.NotifyUnitClick(unit);
                        }
                        else {
                            vm.SelectedUnit = unit;
                        }
                        if (unit is ITreeUnit tUnit) {
                            //右键展开显示;
                            if (e.RightButton == MouseButtonState.Pressed) {
                                vm.NotifyRightClick(tUnit);
                            }
                        }

                    }
                }
            }
        }
    }

    /// <summary>
    /// 根据节点类型动态选择上下文菜单;
    /// </summary>
    public class LevelToMarginConverter : GenericStaticInstance<LevelToMarginConverter>,IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value != null && int.TryParse(value.ToString(), out int level)) {
                return new Thickness(level * 16, 0, 0, 0);
            }
            return new Thickness(0, 0, 0, 0);
        }


        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        //    throw new NotImplementedException();
        //}
    }
    
}
