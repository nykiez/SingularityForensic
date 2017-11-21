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
using Singularity.UI.Controls.MessageBoxes.Filtering;
using Singularity.UI.Controls.Filtering;
using Singularity.UI.Controls.Models.Filtering;

namespace Singularity.UI.Controls.Controls.FilterableDataGrid {
    public class FilterableDataGrid : DataGridEx {
        public FilterableDataGrid() {
            
        }

        
        ~FilterableDataGrid() {

        }
        static FilterableDataGrid() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilterableDataGrid), new FrameworkPropertyMetadata(typeof(FilterableDataGrid)));
        }


        ////滚动内容上下文菜单;
        //public static readonly DependencyProperty ScrollContentContextMenuProprerty =
        //    DependencyProperty.Register(
        //    "ScrollContentContextMenu", typeof(ContextMenu), typeof(FilterableDataGrid),
        //    new PropertyMetadata(null));
        //public ContextMenu ScrollContentContextMenu {
        //    get {
        //        return (ContextMenu)this.GetValue(ScrollContentContextMenuProprerty);
        //    }
        //    set {
        //        this.SetValue(ScrollContentContextMenuProprerty, value);
        //    }
        //}

        public static readonly DependencyProperty IsLoadingVisibleProperty =
            DependencyProperty.Register(nameof(IsLoadingVisible),typeof(Visibility),typeof(FilterableDataGrid),new PropertyMetadata(Visibility.Hidden));

        public Visibility IsLoadingVisible {
            get {
                return (Visibility)GetValue(IsLoadingVisibleProperty);
            }
            set {
                SetValue(IsLoadingVisibleProperty, value);
            }
        }

        ////动态列属性;
        public new static readonly DependencyProperty DynamicColumnsProperty = DependencyProperty.Register(
            nameof(DynamicColumns), typeof(DataGridCloumnsCollection), typeof(FilterableDataGrid)
            , new PropertyMetadata(null, DynamicColumns_PropertyChanged));

        private static void DynamicColumns_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is FilterableDataGrid dg) {
                Func<string, FilterHeader> createHeaderPanel = header => {
                    var ctr = new FilterHeader { ColHeader = header };
                    return ctr;
                };

                var newColumns = e.NewValue as DataGridCloumnsCollection;
                if (newColumns != null) {
                    dg.Columns.Clear();
                    dg.colTypes.Clear();
                    dg.fstringModels.Clear();
                    dg.fzModels.Clear();
                    dg.fdModels.Clear();

                    foreach (var col in newColumns) {
                        if (dg.RowType != null && (col as DataGridTextColumn)?.Binding is Binding colBinding) {
                            var prop = dg.RowType.GetProperty(colBinding.Path.Path);

                            if (prop != null && (prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(string)
                                || prop.PropertyType == typeof(long))) {
                                string headerText = null;
                                if (col.Header is string) {
                                    headerText = col.Header as string;
                                }
                                else if (col.Header is FilterHeader filterHeader) {
                                    headerText = filterHeader.ColHeader;
                                }
                                if (headerText != null) {
                                    var header = createHeaderPanel(headerText);
                                    if (prop != null) {
                                        dg.colTypes.Add(prop.Name, prop.PropertyType);
                                    }
                                    if (prop.PropertyType == typeof(string)) {
                                        dg.fstringModels.Add(prop.Name, null);
                                    }
                                    EventHandler<EventArgs> filterAct = (sender, o) => {
                                        var filHeader = header;
                                        if (filHeader != null) {
                                            Action<Func<bool?>> FilterAndDealAct = func => {
                                                var res = func();
                                                if (res == null) {
                                                    return;
                                                }

                                                filHeader.Filtering = res.Value;
                                                dg.Items.Clear();
                                                dg.IsLoadingVisible = Visibility.Visible;
                                                ThreadPool.QueueUserWorkItem(cb => {
                                                    try {
                                                        IEnumerable<object> rows = null;
                                                        dg.Dispatcher.Invoke(() => {
                                                            rows = dg.PreItemsSource;
                                                        });
                                                        foreach (var item in dg.FilterRows(rows)) {
                                                            dg.Dispatcher.Invoke(() => {
                                                                dg.Items.Add(item);
                                                            });
                                                        }
                                                        //Thread.Sleep(5000);
                                                    }
                                                    catch (Exception ex) {
                                                        Logger.WriteLine($"{nameof(FilterableDataGrid)}->Filtering:{ex.Message}");
                                                    }
                                                    finally {
                                                        dg.Dispatcher.Invoke(() => {
                                                            dg.IsLoadingVisible = Visibility.Hidden;
                                                        });
                                                    }
                                                });
                                            };
                                            if ((col as DataGridTextColumn)?.Binding is Binding) {
                                                var path = ((col as DataGridTextColumn).Binding as Binding).Path.Path;
                                                var colTp = dg.colTypes.FirstOrDefault(p => p.Key == path);

                                                if (prop.PropertyType == typeof(string)) {
                                                    var fSModel = dg.fstringModels.FirstOrDefault(p => p.Key == path).Value;
                                                    FilterAndDealAct(() => {
                                                        var res = FilterStringMessageBox.Show(ref fSModel);
                                                        dg.fstringModels[path] = fSModel;
                                                        return res;
                                                    });
                                                }
                                                else if (prop.PropertyType == typeof(DateTime?)) {
                                                    var fdModel = dg.fdModels.FirstOrDefault(p => p.Key == path).Value;
                                                    FilterAndDealAct(() => {
                                                        var res = FilterDTMessageBox.Show(ref fdModel);
                                                        dg.fdModels[path] = fdModel;
                                                        return res;
                                                    });
                                                }
                                                else if (prop.PropertyType == typeof(long)) {
                                                    var fzModel = dg.fzModels.FirstOrDefault(p => p.Key == path).Value;
                                                    FilterAndDealAct(() => {
                                                        var res = FilterSizeMessageBox.Show(ref fzModel);
                                                        dg.fzModels[path] = fzModel;
                                                        return res;
                                                    });
                                                }
                                            }
                                        }
                                    };

                                    header.FilterRequired += filterAct;
                                    //WeakEventManager<FilterHeader, EventArgs>.AddHandler(header, nameof(header.FilterRequired), filterAct);

                                    col.Header = header;
                                }

                            }

                        }

                        dg.Columns.Add(col);
                    }
                }
            }
        }

        //过滤字符串模型集合;
        private Dictionary<string, FilterStringModel> fstringModels = new Dictionary<string,FilterStringModel>();
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
            if(rowType == null) {
                return obRows;
            }
            foreach (var item in fstringModels) {
                if(item.Value?.IsEnabled == true) {
                    rows = FilterStringRow(rows, item.Value, p => (string)rowType.GetProperty(item.Key).GetValue(p));
                }
            }
            foreach (var item in fzModels) {
                if (item.Value?.IsEnabled == true) {
                    rows = FilterSizeRow(rows, item.Value, p => (long)rowType.GetProperty(item.Key).GetValue(p));
                }
            }
            foreach (var item in fdModels) {
                if(item.Value?.IsEnabled == true) {
                    rows = FilterDTRow(rows, item.Value, p => (DateTime?)rowType.GetProperty(item.Key).GetValue(p));
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
                        keys.Any(key => !string.IsNullOrEmpty(key) && fieldFunc(r)?.IndexOf(key,fsModel.MatchCase?StringComparison.Ordinal:StringComparison.OrdinalIgnoreCase) != -1)
                    );
                }
            }
            else if (fsModel?.MatchWay == StringMatchWay.FullMatch) {
                if(fsModel.Keys?.Length > 0) {
                    rows = rows.Where(r => fsModel.Keys.Contains(fieldFunc(r)));
                }
            }

            return rows;
        }

        private IEnumerable<object> FilterSizeRow(IEnumerable<object> rows, FilterSizeModel fzModel,Func<object,long> szFunc) {
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

        private void FilterString(string propName,string[] values) {
            var removeItems = new List<object>();
            foreach (var item in Items) {
                if (values.FirstOrDefault(
                    p => ((string)RowType.GetProperty(propName).GetValue(item))?.Contains(p)??false) == null) {
                    removeItems.Add(item);
                }
            }
            removeItems.ForEach(p => Items.Remove(p));
        }
        
        public static readonly DependencyProperty RowTypeProperty = DependencyProperty.Register(
            nameof(RowType),typeof(Type),typeof(FilterableDataGrid));

        public Type RowType {
            get {
                return (Type) this.GetValue(RowTypeProperty);
            }
            set {
                SetValue(RowTypeProperty, value);
            }
        }
        
        public static readonly DependencyProperty PreItemsSourceProperty = DependencyProperty.Register(
            nameof(PreItemsSource), typeof(IEnumerable<object>), typeof(FilterableDataGrid),
            new PropertyMetadata(PreItemsSource_PropertyChanged));

        private static void PreItemsSource_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var dg = d as FilterableDataGrid;
            var newValue = e.NewValue as IEnumerable;
            var oldValue = e.OldValue as IEnumerable;
            if(dg != null) {
                if(oldValue != null && oldValue is INotifyCollectionChanged) {
                    (oldValue as INotifyCollectionChanged).CollectionChanged -= dg.FilterableDataGrid_CollectionChanged;
                }
                if(newValue != null && newValue is INotifyCollectionChanged ntyCollect) {
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
            if(RowType == null) {
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
                    foreach(var item in e.OldItems) {
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
