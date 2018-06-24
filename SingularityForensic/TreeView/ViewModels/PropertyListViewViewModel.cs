using Prism.Mvvm;
using SingularityForensic.Contracts.Common;
using SingularityForensic.TreeView.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace SingularityForensic.PropertyGrid.ViewModels {
    class PropertyListViewViewModel :BindableBase {
        public ObservableCollection<PropertyItem> Items { get; } = new ObservableCollection<PropertyItem>();

        private PropertyItem _selectedProperty;
        public PropertyItem SelectedProperty {
            get => _selectedProperty;
            set {
                var equal = value == _selectedProperty;
                _selectedProperty = value;
                if (!equal) {
                    SelectedPropertyChanged?.Invoke(this, EventArgs.Empty); 
                }
            }
        }
        public event EventHandler SelectedPropertyChanged;
        
    }
}
