using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.TreeView.Models {
    class PropertyItem:ExtensibleBindableBase {
        public ObservableCollection<PropertyItem> Items { get; } = new ObservableCollection<PropertyItem>();
        private string _propertyName;
        public string PropertyName {
            get => _propertyName;
            set => SetProperty(ref _propertyName, value);
        }


        private string _value;
        public string Value {
            get => _value;
            set => SetProperty(ref _value, value);
        }

    }
}
