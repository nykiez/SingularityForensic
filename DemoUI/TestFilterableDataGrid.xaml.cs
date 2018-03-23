using CDFCControls.Controls;
using SingularityForensic.Controls.FilterableDataGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace DemoUI {

    public class CustomLocalizationManager : LocalizationManager {
        
        public override string GetStringOverride(string key) {
            var bs = base.GetStringOverride(key);
            
            switch (key) {
                case "GridViewGroupPanelText":
                    
                    return "Zum gruppieren ziehen Sie den Spaltenkopf in diesen Bereich.";
                //---------------------- RadGridView Filter Dropdown items texts:
                case "GridViewClearFilter":
                    return "Filter löschen";
                case "GridViewFilterShowRowsWithValueThat":
                    return "Anzeigen der Werte mit Bedingung:";
                case "GridViewFilterSelectAll":
                    return "Alles anzeigen";
                case "GridViewFilterContains":
                    return "Enthält";
                case "GridViewFilterEndsWith":
                    return "Endet mit";
                case "GridViewFilterIsContainedIn":
                    return "Enthalten in";
                case "GridViewFilterIsEqualTo":
                    return "Gleich";
                case "GridViewFilterIsGreaterThan":
                    return "Grösser als ";
                case "GridViewFilterIsGreaterThanOrEqualTo":
                    return "Grösser oder gleich";
                case "GridViewFilterIsLessThan":
                    return "Kleiner als";
                case "GridViewFilterIsLessThanOrEqualTo":
                    return "Kleiner oder gleich";
                case "GridViewFilterIsNotEqualTo":
                    return "Ungleich";
                case "GridViewFilterStartsWith":
                    return "Beginnt mit";
                case "GridViewFilterAnd":
                    return "Und";
                case "GridViewFilter":
                    return "Filter";
            }
            return base.GetStringOverride(key);
        }
    }
        /// <summary>
        /// Interaction logic for TestFilterableDataGrid.xaml
        /// </summary>
        public partial class TestFilterableDataGrid : UserControl {
        public TestFilterableDataGrid() {
            //Telerik.Windows.Controls.RadGridView
            InitiliazeItems();
            
            InitializeComponent();
            
            //InitilalizeWithObjects();
            InitiliazeWithDT();
            LocalizationManager.Manager = new CustomLocalizationManager();
        }

        

        private ObservableCollection<DGModel> items = new ObservableCollection<DGModel>();
        private void InitiliazeItems() {
            for (int i = 0; i < 20000; i++) {
                var model = new DGModel {
                    Sex = i % 2 == 0,
                    Name = Path.GetRandomFileName()
                };
                Extender.SetAttachedText(model, "Das");

                Extender.SetAttachedText(model, (i % 2).ToString());
                items.Add(model);
            }
        }

        private void InitilalizeWithObjects() {
            var dgColumns = new DataGridCloumnsCollection();

            var provider = new ObjectDataProvider {
                MethodName = "GetVal"
            };

            provider.ObjectType = typeof(DGModel);
            
            provider.MethodParameters.Add("Da");
            dgColumns.Add(new DataGridTextColumn {
                Header = "你好啊",
                Binding = new Binding {
                    //Path = new PropertyPath("Extender.Name")
                    Source = provider,
                    //RelativeSource=new RelativeSource { Mode = RelativeSourceMode.Self}
                },
                //GetObjectFunc = ob => {
                //    if(ob is DGModel dg) {
                //        return dg.Name;
                //    }
                //    return null;
                //}
            });

            dgColumns.Add(new DataGridTextColumn {
                Header = "F/M",
                Binding = new Binding {
                    Path = new PropertyPath("Sex")
                },
                //GetObjectFunc = ob => {
                //    if (ob is DGModel dg) {
                //        return dg.Sex;
                //    }
                //    return null;
                //}
            });

            var exCol = new DataGridTextColumn {
                Header = "Extend",
                Binding = new Binding {
                    Path = new PropertyPath(Extender.AttachedTextProperty)
                },
                
                //GetObjectFunc = ob => {
                //    if(ob is DGModel dg) {
                //        return dg.Name;
                //    }
                //    return null;
                //}
            };
            

            dgColumns.Add(exCol);

            
            //dgg.Columns.AddRange(dgColumns);

            this.DataContext = new { Items = items, Type = typeof(DGModel), Columns = dgColumns };
        }

        
        private void InitiliazeWithDT() {
            var dt = new DataTable();
            string col1 = nameof(col1);
            string col2 = nameof(col2);
            string col3 = nameof(col3);
            string col4 = nameof(col4);
            string col5 = nameof(col5);
            dt.Columns.AddRange(
                new DataColumn[] {
                    new DataColumn(col1, typeof(string)),
                    new DataColumn(col2, typeof(bool)),
                    new DataColumn(col3, typeof(Depart)),
                    new DataColumn(col4, typeof(DateTime)),
                    new DataColumn(col5, typeof(long))
                }
            );

            var rand = new Random();
            foreach (var item in items) {
                var row = dt.NewRow();
                row[col1] = item.Name;
                row[col2] = item.Sex;
                row[col3] = Depart.dasd;
                row[col4] = DateTime.Now;
                row[col5] = rand.Next(1000);
                dt.Rows.Add(row);
            }

            
            this.DataContext = new { Items = dt.AsDataView() };
        }

        private void dgg_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e) {
            if(e.ItemPropertyInfo.Name == "col1") {
                //e.Cancel = true;
            }
            
        }

        private void dgg_FieldFilterEditorCreated(object sender, Telerik.Windows.Controls.GridView.EditorCreatedEventArgs e) {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var setting = SaveColumnFilters(dgg);
            LoadColumnFilters(dgg2,setting);
            //dgg2.FilterDescriptors.AddRange(dgg.FilterDescriptors);
        }

        public static IEnumerable<FilterSetting> SaveColumnFilters(Telerik.Windows.Controls.GridView.GridViewDataControl grid) {
            IList<FilterSetting> settings = new List<FilterSetting>();

            foreach (IFilterDescriptor filter in grid.FilterDescriptors) {
                Telerik.Windows.Controls.GridView.IColumnFilterDescriptor columnFilter = filter as Telerik.Windows.Controls.GridView.IColumnFilterDescriptor;
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

        public static void LoadColumnFilters(Telerik.Windows.Controls.GridView.GridViewDataControl grid
            , IEnumerable<FilterSetting> savedSettings) {
            grid.FilterDescriptors.SuspendNotifications();

            foreach (FilterSetting setting in savedSettings) {
                Telerik.Windows.Controls.GridViewColumn column = grid.Columns[setting.ColumnUniqueName];

                Telerik.Windows.Controls.GridView.IColumnFilterDescriptor columnFilter = column.ColumnFilterDescriptor;

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

    public class FilterDescriptorProxy {
        public FilterOperator Operator { get; set; }
        public object Value { get; set; }
        public bool IsCaseSensitive { get; set; }
        
    }

    public class FilterSetting {
        public string ColumnUniqueName { get; set; }
        public List<object> SelectedDistinctValues { get; } = new List<object>();
        public FilterDescriptorProxy Filter1 { get; set; }
        public FilterCompositionLogicalOperator FieldFilterLogicalOperator { get; set; }
        public FilterDescriptorProxy Filter2 { get; set; }
    }
   

    public class DGModel : DependencyObject {
        public string Name { get; set; }
        public bool Sex { get; set; } = false;
        public Depart Depart { get; set; }
        public string GetVal(string propName) {
            return Name?.ToString();
        }


    }

    public class VM {

    }
    
    public enum Depart {
        dasd,
        dasd1,
        dasd2,
        dasd3
    }
    

    public static class Extender {
        public static readonly DependencyProperty AttachedTextProperty =
      DependencyProperty.RegisterAttached(
        "AttachedText",
        typeof(string),
        typeof(Extender),
        new PropertyMetadata(string.Empty));

        public static void SetAttachedText(DependencyObject obj, string value) {
            obj.SetValue(AttachedTextProperty, value);
        }

        public static string GetAttachedText(DependencyObject obj) {
            //if(obj is DGModel dgModel) {
            //    return dgModel.Name;
            //}
            //return "dasd2";
            return (string)obj.GetValue(AttachedTextProperty);
            //return (string)obj.GetValue(AttachedTextProperty);
        }


    }
}
