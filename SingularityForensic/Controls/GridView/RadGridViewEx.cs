using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace SingularityForensic.Controls.GridView {
    public partial class RadGridViewEx:RadGridView
    {
        public RadGridViewEx() {
            this.CurrentCellChanged += GridViewEx_CurrentCellChanged;
            this.Filtered += GridViewEx_Filtered;
        }

        private void GridViewEx_Filtered(object sender, GridViewFilteredEventArgs e) {
            this.FilterSettings = GridViewExtensions.SaveColumnFilters(this);
        }

        /// <summary>
        /// 变更SelectedTExt拓展;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewEx_CurrentCellChanged(object sender, GridViewCurrentCellChangedEventArgs e) {
            if(e.NewCell == null) {
                return;
            }

            if (e.NewCell.Content is TextBox txb) {
                SelectedText = txb?.Text;
            }
            else if(e.NewCell.Content is TextBlock txbl) {
                SelectedText = txbl.Text;
            }
        }
        
        public string SelectedText {
            get { return (string)GetValue(SelectedTexProperty); }
            set { SetValue(SelectedTexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedTex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTexProperty =
            DependencyProperty.Register(nameof(SelectedText), typeof(string), typeof(RadGridViewEx),
                new FrameworkPropertyMetadata(null, 
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
 
        public IEnumerable<FilterSetting> FilterSettings {
            get { return (IEnumerable<FilterSetting>)GetValue(FilterSettingsProperty); }
            set { SetValue(FilterSettingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterSettings.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterSettingsProperty =
            DependencyProperty.Register(nameof(FilterSettings), typeof(IEnumerable<FilterSetting>), typeof(RadGridViewEx), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    null,FilterSettings_CoerceValueCallBack));

        private static object FilterSettings_CoerceValueCallBack(DependencyObject d, object baseValue) {
            if (!(d is GridViewDataControl grid)) {
                return baseValue;
            }
            if(baseValue is IEnumerable<FilterSetting> settings) {
                GridViewExtensions.LoadColumnFilters(grid,settings);
            }
            
            return baseValue;
        }

        private static void FilterSettings_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (!(e.NewValue is IEnumerable<FilterSetting> settings)) {
                return;
            }
            if (!(d is GridViewDataControl grid)) {
                return;
            }

            GridViewExtensions.LoadColumnFilters(grid, settings);
        }


        /// <summary>
        /// 可滚动区域的上下文菜单;
        /// </summary>
        public ContextMenu ScrollContentContextMenu {
            get { return (ContextMenu)GetValue(ScrollContentContextMenuProperty); }
            set { SetValue(ScrollContentContextMenuProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScrollContentContextMenu.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollContentContextMenuProperty =
            DependencyProperty.Register(nameof(ScrollContentContextMenu),
                typeof(ContextMenu), typeof(RadGridViewEx), new PropertyMetadata(null,
                ScrollContentContextMenu_PropertyChanged));

        private static void ScrollContentContextMenu_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if(!(d is RadGridViewEx gv)) {
                return;
            }

            gv.ApplyScrollContextMenu(e.NewValue as ContextMenu);
        }

        private void ApplyScrollContextMenu(ContextMenu menu) {
            if(menu == null) {
                return;
            }

            if(this.Template == null) {
                return;
            }

            if (!(this.Template.FindName(ScrollPart, this) is FrameworkElement scrollpart)) {
                return;
            }

            var name = menu.Name;

            scrollpart.ContextMenu = menu;
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            ApplyScrollContextMenu(this.ScrollContentContextMenu);
            
        }

        private const string ScrollPart = "PART_GridViewVirtualizingPanel";

        
    }

    public partial class RadGridViewEx  {



        public IEnumerable<CustomColumn> CustomColumns {
            get { return (IEnumerable<CustomColumn>)GetValue(CustomColumnsProperty); }
            set { SetValue(CustomColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CustomColumns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomColumnsProperty =
            DependencyProperty.Register(nameof(CustomColumns), typeof(IEnumerable<CustomColumn>), typeof(RadGridViewEx), new PropertyMetadata(null, CustomColumnsPropertyChanged));



        //public static IEnumerable<CustomColumn> GetCustomColumns(DependencyObject obj) {
        //    return (IEnumerable<CustomColumn>)obj.GetValue(CustomColumnsProperty);
        //}

        //public static void SetCustomColumns(DependencyObject obj, IEnumerable<CustomColumn> value) {
        //    obj.SetValue(CustomColumnsProperty, value);
        //}

        //// Using a DependencyProperty as the backing store for CustomColumns.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CustomColumnsProperty =
        //    DependencyProperty.RegisterAttached("CustomColumns", typeof(IEnumerable<CustomColumn>), typeof(RadGridView), new PropertyMetadata(null, CustomColumnsPropertyChanged));
        
        private static void CustomColumnsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if(!(d is RadGridView gv)) {
                return;
            }

            if(!(e.NewValue is IEnumerable<CustomColumn> columns)) {
                return;
            }

            foreach (var col in columns) {
                Telerik.Windows.Controls.GridViewColumn newColumn = null;
                if(col is CustomDataColumn dataCol) {
                    newColumn = new GridViewDataColumn {
                        DataMemberBinding = dataCol.Binding,
                        DataType = dataCol.ColumnDataType
                    };
                }
                else {
                    newColumn = new Telerik.Windows.Controls.GridViewColumn();
                }
                newColumn.CellTemplate = col.CellTemplate;
                //newColumn.ShowFieldFilters = true;
                newColumn.Header = col.Header;
                //newColumn.FilterMemberType = typeof(string);
                newColumn.ShowDistinctFilters = col.ShowDistinctFilters;
                gv.Columns.Add(newColumn);
            }
            
        }
    }
}
