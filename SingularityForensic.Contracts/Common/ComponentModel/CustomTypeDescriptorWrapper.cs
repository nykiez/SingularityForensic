using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common.ComponentModel {
    /// <summary>
    /// ICustomMemberDescriptor-><see cref="System.ComponentModel.CustomTypeDescriptor"/>的封装;
    /// </summary>
    public class CustomTypeDescriptorWrapper : CustomTypeDescriptor {
        private PropertyDescriptorCollection _properties = null;
        public IEnumerable<ICustomMemberDescriptor> CustomMemberDescriptors => _customMemberDescriptors.Select(p => p);
        private List<ICustomMemberDescriptor> _customMemberDescriptors = new List<ICustomMemberDescriptor>();

        public void CompositeCustomMemberDecriptor(ICustomMemberDescriptor descriptor) {
            _customMemberDescriptors.Add(descriptor);
        }

        public override PropertyDescriptorCollection GetProperties() {
            if (_properties == null) {
                _properties = new PropertyDescriptorCollection(
                    _customMemberDescriptors.SelectMany(
                        p => {
                            var index = 0;
                            return p.GetMemberInfos().
                           Select(
                                   q => new PropertyDescriptorWrapper(q) { GroupName = p.DisplayName, Order = index++ }
                           );
                        }
                        ).ToArray()
                    );
            }
            return _properties;
        }

        
    }
}
