using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace SingularityForensic.Controls.GridView {
    /// <summary>
    /// 网格视图拓展;
    /// </summary>
    public static class GridViewExtensions {
        //得到过滤设定;
        public static IEnumerable<FilterSetting> SaveColumnFilters(GridViewDataControl grid) {
            IList<FilterSetting> settings = new List<FilterSetting>();

            foreach (IFilterDescriptor filter in grid.FilterDescriptors) {
                IColumnFilterDescriptor columnFilter = filter as IColumnFilterDescriptor;
                if (columnFilter != null) {
                    FilterSetting setting = new FilterSetting();

                    setting.ColumnUniqueName = columnFilter.Column.UniqueName;

                    setting.SelectedDistinctValues.AddRange(columnFilter.DistinctFilter.DistinctValues);

                    if (columnFilter.FieldFilter.Filter1.IsActive) {
                        setting.Filter1 = new FilterDescriptorProxy();
                        setting.Filter1.Operator = columnFilter.FieldFilter.Filter1.Operator;
                        setting.Filter1.Value = columnFilter.FieldFilter.Filter1.Value;
                        setting.Filter1.IsCaseSensitive = columnFilter.FieldFilter.Filter1.IsCaseSensitive;
                    }

                    setting.FieldFilterLogicalOperator = columnFilter.FieldFilter.LogicalOperator;

                    if (columnFilter.FieldFilter.Filter2.IsActive) {
                        setting.Filter2 = new FilterDescriptorProxy();
                        setting.Filter2.Operator = columnFilter.FieldFilter.Filter2.Operator;
                        setting.Filter2.Value = columnFilter.FieldFilter.Filter2.Value;
                        setting.Filter2.IsCaseSensitive = columnFilter.FieldFilter.Filter2.IsCaseSensitive;
                    }

                    settings.Add(setting);
                }
            }

            return settings;
        }

        //加载过滤设定;
        public static void LoadColumnFilters(GridViewDataControl grid
            , IEnumerable<FilterSetting> savedSettings) {
            if(savedSettings.Count() == 0) {
                grid.FilterDescriptors.Clear();
                return;
            }

            grid.FilterDescriptors.SuspendNotifications();
            
            foreach (FilterSetting setting in savedSettings) {
                Telerik.Windows.Controls.GridViewColumn column = grid.Columns[setting.ColumnUniqueName];

                IColumnFilterDescriptor columnFilter = column.ColumnFilterDescriptor;

                foreach (object distinctValue in setting.SelectedDistinctValues) {
                    columnFilter.DistinctFilter.AddDistinctValue(distinctValue);
                }

                if (setting.Filter1 != null) {
                    columnFilter.FieldFilter.Filter1.Operator = setting.Filter1.Operator;
                    columnFilter.FieldFilter.Filter1.Value = setting.Filter1.Value;
                    columnFilter.FieldFilter.Filter1.IsCaseSensitive = setting.Filter1.IsCaseSensitive;
                }

                columnFilter.FieldFilter.LogicalOperator = setting.FieldFilterLogicalOperator;

                if (setting.Filter2 != null) {
                    columnFilter.FieldFilter.Filter2.Operator = setting.Filter2.Operator;
                    columnFilter.FieldFilter.Filter2.Value = setting.Filter2.Value;
                    columnFilter.FieldFilter.Filter2.IsCaseSensitive = setting.Filter2.IsCaseSensitive;
                }
            }

            grid.FilterDescriptors.ResumeNotifications();
        }
    }
}
