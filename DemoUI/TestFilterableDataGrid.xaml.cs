using CDFCControls.Controls;
using Prism.Mvvm;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

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

    public class RowNumberColumn : Telerik.Windows.Controls.GridViewDataColumn {
        public override System.Windows.FrameworkElement CreateCellElement(Telerik.Windows.Controls.GridView.GridViewCell cell, object dataItem) {
            if (!(cell.Content is TextBlock textBlock)) {
                textBlock = new TextBlock();
            }

            textBlock.Text = (this.DataControl.Items.IndexOf(dataItem) + 1).ToString();
            return textBlock;
        }
    }

    /// <summary>
    /// Interaction logic for TestFilterableDataGrid.xaml
    /// </summary>
    public partial class TestFilterableDataGrid : UserControl {
        public TestFilterableDataGrid() {
            //Telerik.Windows.Controls.RadGridView

            InitializeComponent();

            //InitilalizeWithObjects();
            InitiliazeWithDT();
            this.DataContext = new VM();
            LocalizationManager.Manager = new CustomLocalizationManager();
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
            this.DataContext = new VM();
        }


        private void InitiliazeWithDT() {

        }

        private void Dgg_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e) {

        }

        private void Dgg_FieldFilterEditorCreated(object sender, Telerik.Windows.Controls.GridView.EditorCreatedEventArgs e) {

        }

        private void Button_Click(object sender, RoutedEventArgs e) {


            //dgg2.FilterDescriptors.AddRange(dgg.FilterDescriptors);
        }



        private void Dgg2_SelectedCellsChanged(object sender, GridViewSelectedCellsChangedEventArgs e) {

        }

        private void Dgg2_SelectionChanged(object sender, SelectionChangeEventArgs e) {

        }

        private void Dgg_Copying(object sender, GridViewClipboardEventArgs e) {

        }

        private void Dgg_CurrentCellChanged(object sender, GridViewCurrentCellChangedEventArgs e) {

        }

        private void Dgg_Filtered(object sender, GridViewFilteredEventArgs e) {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            dgg.Columns[0].Width = 100;
        }

        private void Dgg2_LoadingRowDetails(object sender, GridViewRowDetailsEventArgs e) {

        }

        private void Dgg_LoadingRowDetails(object sender, GridViewRowDetailsEventArgs e) {

        }

        private void Dgg_AutoGeneratingColumn_1(object sender, GridViewAutoGeneratingColumnEventArgs e) {

        }
    }



    public class DGModel : DependencyObject {
        public string Name { get; set; }
        public bool Sex { get; set; } = false;
        public Depart Depart { get; set; }
        public string GetVal(string propName) {
            return Name?.ToString();
        }


    }
    public class AbDGModel : DependencyObject {

    }

    public class VM : BindableBase {
        public VM() {
            InitiliazeItems();
            InitilaizeDt();
            var helloCm = CommandItemFactory.CreateNew(null);
            helloCm.Name = "你好";
            Commands.Add(helloCm);
        }

        public ObservableCollection<ICommandItem> Commands { get; set; } = new ObservableCollection<ICommandItem>();
        private List<DGModel> items = new List<DGModel>();
        public DataTable Items {
            get; private set;
        }


        private object _FilterSettings1;
        public object FilterSettings1 {
            get => _FilterSettings1;
            set => SetProperty(ref _FilterSettings1, value);
        }

        private object _FilterSettings2;
        public object FilterSettings2 {
            get => _FilterSettings2;
            set => SetProperty(ref _FilterSettings2, value);
        }

        public void InitilaizeDt() {
            Items = new DataTable();
            string col1 = nameof(col1);
            string col2 = nameof(col2);
            string col3 = nameof(col3);
            string col4 = nameof(col4);
            string col5 = nameof(col5);
            string col6 = nameof(col6);
            Items.Columns.AddRange(
                new DataColumn[] {
                    new DataColumn(col1, typeof(string)),
                    new DataColumn(col2, typeof(bool)),
                    new DataColumn(col3, typeof(Depart)),
                    new DataColumn(col4, typeof(DateTime)),
                    new DataColumn(col5, typeof(long)),
                    new DataColumn(col6, typeof(AbDGModel))
        }
            );

            var rand = new Random();
            foreach (var item in items) {
                var row = Items.NewRow();
                row[col1] = item.Name;
                row[col2] = item.Sex;
                row[col3] = Depart.dasd;
                row[col4] = DBNull.Value;
                row[col5] = rand.Next(1000);
                Items.Rows.Add(row);
            }



        }
        private void InitiliazeItems() {
            for (int i = 0; i < 200000; i++) {
                var model = new DGModel {
                    Sex = i % 2 == 0,
                    Name = Path.GetRandomFileName()
                };
                Extender.SetAttachedText(model, "Das");

                Extender.SetAttachedText(model, (i % 2).ToString());
                items.Add(model);
            }
        }

        private Prism.Commands.DelegateCommand _copyFilterComand;
        public Prism.Commands.DelegateCommand CopyFilterComand => _copyFilterComand ??
            (_copyFilterComand = new Prism.Commands.DelegateCommand(
                () => {
                    FilterSettings2 = FilterSettings1;
                }
            ));


        private string _selectedText;
        public string SelectedText {
            get => _selectedText;
            set => SetProperty(ref _selectedText, value);
        }

        private Prism.Commands.DelegateCommand _changeColumnIndexCommand;
        public Prism.Commands.DelegateCommand ChangeColumnIndexCommand => _changeColumnIndexCommand ??
            (_changeColumnIndexCommand = new Prism.Commands.DelegateCommand(
                () => {
                    Items.Columns.Remove(Items.Columns[0]);
                }
            ));
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
