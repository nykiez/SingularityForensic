using CDFCControls.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EventLogger;
using System.Threading;
using SingularityForensic.Controls.Models.Filtering;

namespace SingularityForensic.Controls.FilterableDataGrid {
    public class FilterableDataGrid : DataGridEx {
        public FilterableDataGrid() {

        }

        protected override void OnSorting(DataGridSortingEventArgs eventArgs) {
            //eventArgs.Column is DataGridTextColumn
            base.OnSorting(eventArgs);
            //return;
        }

        ~FilterableDataGrid() {

        }

        //protected override void OnSorting(DataGridSortingEventArgs eventArgs) {
        //    base.OnSorting(eventArgs);
        //}

        public static readonly DependencyProperty IsLoadingVisibleProperty =
            DependencyProperty.Register(nameof(IsLoadingVisible), typeof(Visibility), typeof(FilterableDataGrid), new PropertyMetadata(Visibility.Hidden));

        public Visibility IsLoadingVisible {
            get {
                return (Visibility)GetValue(IsLoadingVisibleProperty);
            }
            set {
                SetValue(IsLoadingVisibleProperty, value);
            }
        }

        protected override void ApplyDynamicColumns(DependencyPropertyChangedEventArgs e) {
            if (!(e.NewValue is DataGridCloumnsCollection newColumns)) {
                return;
            }

            Columns.Clear();
            colTypes.Clear();
            fstringModels.Clear();
            fzModels.Clear();
            fdModels.Clear();

            foreach (var col in newColumns) {
                if (RowType == null) {
                    Columns.Add(col);
                    continue;
                }
                if (!(col is DataGridTextColumn textColumn)) {
                    Columns.Add(col);
                    continue;
                }

                if (!(textColumn.Binding is Binding colBinding)) {
                    Columns.Add(col);
                    continue;
                }

                if (colBinding.Path == null) {
                    Columns.Add(col);
                    continue;
                }

                var prop = RowType.GetProperty(colBinding.Path.Path);

                if (prop == null) {
                    Columns.Add(col);
                    continue;
                }

                //当前仅支持三种类型的过滤;
                if (prop.PropertyType == typeof(DateTime?) ||
                    prop.PropertyType == typeof(string) ||
                    prop.PropertyType == typeof(long)
                    ) {
                    if (!(col.Header is string headerText)) {
                        Columns.Add(col);
                        continue;
                    }

                    var headerControl = new FilterHeader {
                        PropertyType = prop.PropertyType,
                        PropertyName = prop.Name
                    };
                    headerControl.FilterRequired += OnFilterRequired;
                    if (prop != null) {
                        colTypes.Add(prop.Name, prop.PropertyType);
                    }
                    if (prop.PropertyType == typeof(string)) {
                        fstringModels.Add(prop.Name, null);
                    }
                    col.Header = headerControl;
                }

                Columns.Add(col);
            }

        }

        //当列头请求过滤时;
        private void OnFilterRequired(object sender, EventArgs e) {
            if (!(sender is FilterHeader filHeader)) {
                return;
            }

            //var col = filHeader.Column;
            //if (col == null) {
            //    return;
            //}

            //if (!(col is DataGridTextColumn textColumn)) {
            //    return;
            //}

            //if (!(textColumn.Binding is Binding colBinding)) {
            //    return;
            //}

            //var path = colBinding.Path.Path;
            //var colTp = colTypes.FirstOrDefault(p => p.Key == path);
            //var prop = RowType.GetProperty(colBinding.Path.Path);

            //if (prop.PropertyType == typeof(string)) {
            //    var fSModel = fstringModels[path];
            //    var res = FilterStringMessageBox.Show(ref fSModel);
            //    if (res == null) {
            //        return;
            //    }
            //    filHeader.Filtering = res.Value;
            //    fstringModels[path] = fSModel;

            //    FilterAll();
            //}
            //else if (prop.PropertyType == typeof(DateTime?)) {
            //    var fdModel = fdModels[path];
            //    var res = FilterDTMessageBox.Show(ref fdModel);
            //    if (res == null) {
            //        return;
            //    }
            //    filHeader.Filtering = res.Value;
            //    fdModels[path] = fdModel;

            //    FilterAll();
            //}
            //else if (prop.PropertyType == typeof(long)) {
            //    var fzModel = fzModels[path];
            //    var res = FilterSizeMessageBox.Show(ref fzModel);
            //    if (res == null) {
            //        return;
            //    }
            //    filHeader.Filtering = res.Value;
            //    fzModels[path] = fzModel;

            //    FilterAll();
            //}


        }

        private void FilterAll() {
            Items.Clear();
            IsLoadingVisible = Visibility.Visible;
            ThreadPool.QueueUserWorkItem(cb => {
                try {
                    IEnumerable<object> rows = null;
                    Dispatcher.Invoke(() => {
                        rows = PreItemsSource;
                    });
                    foreach (var item in FilterRows(rows)) {
                        Dispatcher.Invoke(() => {
                            Items.Add(item);
                        });
                    }
                    //Thread.Sleep(5000);
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(FilterableDataGrid)}->Filtering:{ex.Message}");
                }
                finally {
                    Dispatcher.Invoke(() => {
                        IsLoadingVisible = Visibility.Hidden;
                    });
                }
            });
        }

        //过滤字符串模型集合;
        private Dictionary<string, FilterStringModel> fstringModels = new Dictionary<string, FilterStringModel>();
        //过滤大小模型集合;
        private Dictionary<string, FilterSizeModel> fzModels = new Dictionary<string, FilterSizeModel>();
        //过滤日期模型集合;
        private Dictionary<string, FilterDateModel> fdModels = new Dictionary<string, FilterDateModel>();

        //列的类型属性集合;string为属性名;
        private Dictionary<string, Type> colTypes = new Dictionary<string, Type>();


        public IEnumerable<object> FilterRows(IEnumerable<object> obRows) {
            var rows = obRows;
            Type rowType = null;
            this.Dispatcher.Invoke(() => {
                rowType = RowType;
            });
            if (rowType == null) {
                return obRows;
            }
            foreach (var item in fstringModels) {
                if (item.Value?.IsEnabled == true) {
                    var propInfo = rowType.GetProperty(item.Key);
                    rows = FilterStringRow(rows, item.Value, p => (string)propInfo.GetValue(p));
                }
            }
            foreach (var item in fzModels) {
                if (item.Value?.IsEnabled == true) {
                    var propInfo = rowType.GetProperty(item.Key);
                    rows = FilterSizeRow(rows, item.Value, p => (long)propInfo.GetValue(p));
                }
            }
            foreach (var item in fdModels) {
                if (item.Value?.IsEnabled == true) {
                    var propInfo = rowType.GetProperty(item.Key);
                    rows = FilterDTRow(rows, item.Value, p => (DateTime?)propInfo.GetValue(p));
                }
            }

            return rows;
        }

        private IEnumerable<object> FilterDTRow(IEnumerable<object> rows, FilterDateModel fdtModel, Func<object, DateTime?> dtFunc) {
            if (fdtModel.Condition == TwoConditionRule.MinOnly) {
                rows = rows.Where(p => dtFunc(p) >= fdtModel.MinTime);
            }
            else if (fdtModel.Condition == TwoConditionRule.MaxOnly) {
                rows = rows.Where(p => dtFunc(p) <= fdtModel.MaxTime);
            }
            else if (fdtModel.Condition == TwoConditionRule.MinOrMax) {

                rows = rows.Where(p => dtFunc(p) <= fdtModel.MaxTime || dtFunc(p) >= fdtModel.MinTime);
            }
            else if (fdtModel.Condition == TwoConditionRule.MinAndMax) {
                rows = rows.Where(p => dtFunc(p) <= fdtModel.MaxTime && dtFunc(p) >= fdtModel.MinTime);
            }
            return rows;
        }

        private IEnumerable<object> FilterStringRow(IEnumerable<object> rows, FilterStringModel fsModel, Func<object, string> fieldFunc) {
            if (fsModel?.MatchWay == StringMatchWay.AnyKey) {
                var keys = fsModel.Keys;
                if (keys.Length > 0) {
                    rows = rows.Where(r =>
                        keys.Any(key => !string.IsNullOrEmpty(key) && fieldFunc(r)?.IndexOf(key, fsModel.MatchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase) != -1)
                    );
                }
            }
            else if (fsModel?.MatchWay == StringMatchWay.FullMatch) {
                if (fsModel.Keys?.Length > 0) {
                    rows = rows.Where(r => fsModel.Keys.Contains(fieldFunc(r)));
                }
            }

            return rows;
        }

        private IEnumerable<object> FilterSizeRow(IEnumerable<object> rows, FilterSizeModel fzModel, Func<object, long> szFunc) {
            if (fzModel.Condition == TwoConditionRule.MinOnly) {
                rows = rows.Where(p => szFunc(p) >= (fzModel.MinSize ?? 0));
            }
            else if (fzModel.Condition == TwoConditionRule.MaxOnly) {
                rows = rows.Where(p => szFunc(p) <= (fzModel.MaxSize ?? 0));
            }
            else if (fzModel.Condition == TwoConditionRule.MinOrMax) {
                rows = rows.Where(p => szFunc(p) <= (fzModel.MaxSize ?? 0) || szFunc(p) >= (fzModel.MinSize));
            }
            else if (fzModel.Condition == TwoConditionRule.MinAndMax) {
                rows = rows.Where(p => szFunc(p) <= (fzModel.MaxSize ?? 0) && szFunc(p) >= (fzModel.MinSize));
            }
            return rows;
        }

        private void FilterString(string propName, string[] values) {
            var removeItems = new List<object>();
            foreach (var item in Items) {
                if (values.FirstOrDefault(
                    p => ((string)RowType.GetProperty(propName).GetValue(item))?.Contains(p) ?? false) == null) {
                    removeItems.Add(item);
                }
            }
            removeItems.ForEach(p => Items.Remove(p));
        }

        public static readonly DependencyProperty RowTypeProperty = DependencyProperty.Register(
            nameof(RowType), typeof(Type), typeof(FilterableDataGrid));

        public Type RowType {
            get {
                return (Type)this.GetValue(RowTypeProperty);
            }
            set {
                SetValue(RowTypeProperty, value);
            }
        }

        public static readonly DependencyProperty PreItemsSourceProperty = DependencyProperty.Register(
            nameof(PreItemsSource), typeof(IEnumerable<object>), typeof(FilterableDataGrid),
            new PropertyMetadata(PreItemsSource_PropertyChanged));

        private static void PreItemsSource_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var newValue = e.NewValue as IEnumerable;
            var oldValue = e.OldValue as IEnumerable;
            if (d is FilterableDataGrid dg) {
                if (dg.ItemsSource != null) {
                    throw new InvalidOperationException($"{nameof(ItemsSource)} and {nameof(PreItemsSource)} can't be set at the same time.");
                }

                if (oldValue != null && oldValue is INotifyCollectionChanged) {
                    (oldValue as INotifyCollectionChanged).CollectionChanged -= dg.FilterableDataGrid_CollectionChanged;
                }
                if (newValue != null && newValue is INotifyCollectionChanged ntyCollect) {
                    ntyCollect.CollectionChanged += dg.FilterableDataGrid_CollectionChanged;
                    //WeakEventManager<INotifyCollectionChanged, NotifyCollectionChangedEventArgs>.AddHandler(
                    //    ntyCollect, nameof(INotifyCollectionChanged.CollectionChanged), dg.FilterableDataGrid_CollectionChanged);
                    //(newValue as INotifyCollectionChanged).CollectionChanged += dg.FilterableDataGrid_CollectionChanged;
                    foreach (var item in newValue) {
                        dg.Items.Add(item);
                    }
                }
            }
        }

        private void FilterableDataGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (RowType == null) {
                return;
            }
            var removeItems = new List<object>();
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    var items = e.NewItems as IEnumerable;
                    foreach (var item in items) {
                        Items.Add(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems) {
                        Items.Remove(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Items.Clear();
                    break;
            }
        }

        public IEnumerable<object> PreItemsSource {
            get {
                return (IEnumerable<object>)GetValue(PreItemsSourceProperty);
            }
            set {
                SetValue(PreItemsSourceProperty, value);
            }
        }



       



    }
}
