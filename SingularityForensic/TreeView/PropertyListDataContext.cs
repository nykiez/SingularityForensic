using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.PropertyGrid.ViewModels;
using SingularityForensic.TreeView.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.TreeView {
    class PropertyListDataContext : IPropertyListDataContext {
        public PropertyListDataContext() {
            _vm = new PropertyListViewViewModel ();
            UIObject = ViewProvider.CreateView(Contracts.TreeView.Constants.PorpertyListView, _vm);
            _vm.SelectedPropertyChanged += delegate {
                SelectedMemberInfoChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        private PropertyListViewViewModel  _vm;
        
        public IMemberInfo SelectedMemberInfo {
            get {
                var memberInfo = _vm.SelectedProperty?.GetInstance<IMemberInfo>(Constants.PropertyItemTag_MemberInfo);
                return memberInfo;
            }
        }

        public object UIObject { get; }

        public IEnumerable<ICustomMemberDescriptor> CustomMemberDescriptors => throw new NotImplementedException();

        public event EventHandler SelectedMemberInfoChanged;

        public void AddCustomMemberDescriptor(ICustomMemberDescriptor descriptor) {
            if(descriptor == null) {
                throw new ArgumentNullException(nameof(descriptor));
            }
            var propItem = new PropertyItem { PropertyName = descriptor.DisplayName };
            
            var memberInfos = descriptor.GetMemberInfos();
            if(memberInfos == null) {
                LoggerService.WriteCallerLine($"{nameof(memberInfos)} can't be null.");
                return;
            }

            _vm.Items.Add(propItem);
            ThreadInvokerExtensions.AddBufferItemsToCollection(
                propItem.Items,
                memberInfos,
                memberInfo => {
                    var infoPropItem = new PropertyItem { PropertyName = memberInfo.DisplayName, Value = memberInfo.Value?.ToString() };
                    infoPropItem.SetInstance<IMemberInfo>(memberInfo, Constants.PropertyItemTag_MemberInfo);
                    return infoPropItem;
                },
                sleepInterval:100
            );
            
            
            //_vm.Item.CompositeCustomMemberDecriptor(descriptor);
        }
    }


}
