﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SingularityForensic.Contracts.Common {
    public interface IGridStack {
        IEnumerable<object> Children { get; }
        void AddChild(object child, GridLength gridLength, int index = -1,bool isHorizontal = false);
        void RemoveChild(object child);
    }
    
    public static class GridStackHelper {
        public static void AddChild(this Grid grid, object child, 
            GridLength gridLength, int index = -1, bool isHorizontal = false) {
            if(!(child is UIElement elem)) {
                return;
            }

            grid.Children.Add(elem);
            if (isHorizontal) {
                ApplyChildIndexToDefinitions(grid.ColumnDefinitions, elem, gridLength, index);
            }
            else {
                ApplyChildIndexToDefinitions(grid.RowDefinitions, elem, gridLength, index);
            }
        }


        private static void ApplyChildIndexToDefinitions<Definition>
            (ICollection<Definition> definitions,
            UIElement elem,GridLength gridLength,int index = -1) where Definition:DefinitionBase {
            
        }



        public static void RemoveChild(this Grid grid,object child) {
            
        }
        
        public static IEnumerable<object> GetChildren(this Grid grid) {
            foreach (var elem in grid.Children) {
                yield return elem;
            }
        }

    }
}
