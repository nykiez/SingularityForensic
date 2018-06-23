using Prism.Mvvm;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace SingularityForensic.PropertyGrid.ViewModels {
    class PropertyGridViewModel:BindableBase {

        //private object _It;
        //public object val {
        //    get => _val;
        //    set => SetProperty(ref _val, value);
        //}

        public CustomTypeDescriptorWrapper Item { get;
            set; } = new CustomTypeDescriptorWrapper();

        private PropertyDefinition _selectedProperty;
        public PropertyDefinition SelectedProperty {
            get => _selectedProperty;
            set {
                var equal = value != _selectedProperty;
                _selectedProperty = value;
                if (!equal) {
                    SelectedPropertyChanged?.Invoke(this, EventArgs.Empty); 
                }
            }
        }
        public event EventHandler SelectedPropertyChanged;
        
    }
}
