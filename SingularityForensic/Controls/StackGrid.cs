using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SingularityForensic.Controls
{
    class StackGrid<TStackItem> : IStackGrid<TStackItem> where TStackItem : IUIObjectProvider {
        private readonly List<(TStackItem stackItem, GridChildLength gridChildLength)> _children = new List<(TStackItem stackItem, GridChildLength gridChildLength)>();
        public IEnumerable<(TStackItem stackItem, GridChildLength gridChildLength)> Children => _children.Select(p => p);

        private Orientation _orientation;
        public Orientation Orientation {
            get => _orientation;
            set {
                if(_orientation != value) {
                    _orientation = value;

                }
            }
        }

        public object UIObject => _grid;
        private Grid _grid;

        public bool NeedSplitter { get; set; }
        
        public void AddChild<TDefinition>(TStackItem child, GridChildLength gridChildLength, int index = -1) {
            if ((uint)index <= (uint)_children.Count)
                _children.Insert(index,(child,gridChildLength));
            else
                _children.Add((child, gridChildLength));
            UpdateGrid();
        }
        
        public void Remove(TStackItem child) {
            throw new NotImplementedException();
        }

        public double SplitterLength {
            get => splitterLength;
            set {
                if (splitterLength != value) {
                    splitterLength = value;
                    UpdateGrid();
                }
            }
        }
        double splitterLength;

        private void UpdateGrid() {
            _grid.Children.Clear();
            _grid.ColumnDefinitions.Clear();
            _grid.RowDefinitions.Clear();

            var needSplitter = false;
            double d = 0.05;
            int rowCol = 0;

            

            // Make sure the horizontal grid splitters can resize the content
            if (Orientation == Orientation.Vertical) {
                _grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });    
                foreach (var info in _children) {
                    if (needSplitter && !info.gridChildLength.GridLength.IsAuto) {
                        var gridSplitter = GetSplitter();
                        Panel.SetZIndex(gridSplitter, 1);
                        _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(splitterLength, GridUnitType.Pixel) });
                        gridSplitter.SetValue(Grid.RowProperty, rowCol);
                        gridSplitter.Margin = new Thickness(0, -5, 0, -5);
                        gridSplitter.BorderThickness = new Thickness(0, 5, 0, 5);
                        gridSplitter.HorizontalAlignment = HorizontalAlignment.Stretch;
                        gridSplitter.VerticalAlignment = VerticalAlignment.Center;
                        rowCol++;
                    }

                    var rowDef = new RowDefinition() { Height = GetGridLength(info.gridChildLength.GridLength, -d) };
                    rowDef.MaxHeight = info.gridChildLength.MaxLength;
                    rowDef.MinHeight = info.gridChildLength.MinLength;
                        
                    _grid.RowDefinitions.Add(rowDef);

                    var uiel = GetUIElement(info.stackItem);
                    uiel.SetValue(Grid.RowProperty, rowCol);
                    uiel.ClearValue(Grid.ColumnProperty);
                    
                    rowCol++;
                    d = -d;
                    needSplitter = !info.gridChildLength.GridLength.IsAuto;
                }
            }
            else {
                _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                
                foreach (var info in _children) {
                    if (needSplitter && !info.gridChildLength.GridLength.IsAuto) {
                        var gridSplitter = GetSplitter();
                        Panel.SetZIndex(gridSplitter, 1);
                        _grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(splitterLength, GridUnitType.Pixel) });
                        gridSplitter.SetValue(Grid.ColumnProperty, rowCol);
                        gridSplitter.Margin = new Thickness(-5, 0, -5, 0);
                        gridSplitter.BorderThickness = new Thickness(5, 0, 5, 0);
                        gridSplitter.HorizontalAlignment = HorizontalAlignment.Center;
                        gridSplitter.VerticalAlignment = VerticalAlignment.Stretch;
                        
                        rowCol++;
                    }

                    var colDef = new ColumnDefinition() { Width = GetGridLength(info.gridChildLength.GridLength, -d) };
                    colDef.MaxWidth = info.gridChildLength.MaxLength;
                    colDef.MinWidth = info.gridChildLength.MinLength;
                    _grid.ColumnDefinitions.Add(colDef);

                    var uiel = GetUIElement(info.stackItem);
                    uiel.ClearValue(Grid.RowProperty);
                    uiel.SetValue(Grid.ColumnProperty, rowCol);

                    rowCol++;
                    d = -d;
                    needSplitter = !info.gridChildLength.GridLength.IsAuto;
                }
            }
        }

        private GridLength GetGridLength(GridLength len, double d) {
            if (len.IsStar && len.Value == 1)
                return new GridLength(1 + d, GridUnitType.Star);
            return len;
        }

        private GridSplitter GetSplitter() {
            var gridSplitter = new GridSplitter();
            gridSplitter.BorderBrush = Brushes.Transparent;
            gridSplitter.Focusable = false;
            _grid.Children.Add(gridSplitter);
            return gridSplitter;
        }

        private UIElement GetUIElement(TStackItem child) {
            var obj = child.UIObject;
            var uiel = obj as UIElement;
            if (uiel == null)
                uiel = new ContentPresenter { Content = obj };

            _grid.Children.Add(uiel);
            return uiel;
        }
    }

    [Export(typeof(IStackGridFactory))]
    class StackGridFactoryImpl : IStackGridFactory {
        public IStackGrid<TStackItem> CreateNew<TStackItem>() where TStackItem : IUIObjectProvider {
            return new StackGrid<TStackItem>() {
                SplitterLength = Constants.SpliterLength_Default
            };
        }
    }
}
