using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Controls {
    public class CustomTypedListSource<T> : ObservableCollection<T>, ITypedList {
        public IList<PropertyDescriptor> PropertyDescriptorList { get; } = new List<PropertyDescriptor>();

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors) {
            return new PropertyDescriptorCollection(PropertyDescriptorList.ToArray(), true);
        }

        public string GetListName(PropertyDescriptor[] listAccessors) {
            throw new NotImplementedException();
        }
    }
}
