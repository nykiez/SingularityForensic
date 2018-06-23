using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.PropertyGrid;
using SingularityForensic.PropertyGrid.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.PropertyGrid {
    class PropertyGridDataContext : IPropertyGridDataContext {
        public PropertyGridDataContext() {
            _vm = new PropertyGridViewModel();
            UIObject = ViewProvider.CreateView(Contracts.PropertyGrid.Constants.PorpertyGridView, _vm);
            _vm.SelectedPropertyChanged += delegate {
                SelectedMemberInfoChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        private PropertyGridViewModel _vm;
        
        public IMemberInfo SelectedMemberInfo {
            get {
                if(_vm.SelectedProperty?.SourceProperty?.Descriptor is PropertyDescriptorWrapper descriptor) {
                    return descriptor.MemberInfo;
                }
                return null;
            }
        }

        public object UIObject { get; }

        public IEnumerable<ICustomMemberDescriptor> CustomMemberDescriptors => throw new NotImplementedException();

        public event EventHandler SelectedMemberInfoChanged;

        public void AddCustomMemberDescriptor(ICustomMemberDescriptor descriptor) {
            if(descriptor == null) {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            _vm.Item.CompositeCustomMemberDecriptor(descriptor);
        }
    }


}
