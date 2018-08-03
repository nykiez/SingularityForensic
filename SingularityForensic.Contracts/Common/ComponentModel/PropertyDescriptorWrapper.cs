using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common.ComponentModel {



    /// <summary>
    /// IMemberInfo-><see cref="System.ComponentModel.PropertyDescriptor"/>的封装;
    /// </summary>
    public class PropertyDescriptorWrapper : PropertyDescriptor {
        public PropertyDescriptorWrapper(IMemberInfo memberInfo):base(memberInfo.MemberName, null) {
            this.MemberInfo = memberInfo ?? throw new ArgumentNullException(nameof(memberInfo));
        }

        public IMemberInfo MemberInfo { get; }
        public override string Name => MemberInfo.MemberName;
        public override string DisplayName => MemberInfo.DisplayName;
        public string GroupName { get; set; }
        public int Order { get; set; }
        private AttributeCollection _attributes;
        public override AttributeCollection Attributes {
            get {
                if (_attributes == null) {
                    _attributes = new AttributeCollection(new Attribute[] { new DisplayAttribute {
                        GroupName = GroupName,
                        Order = Order
                    } });
                }
                return _attributes;
            }
        }

        public override Type ComponentType { get; }

        public override bool IsReadOnly => true;

        public override Type PropertyType => MemberInfo.MemberType;

        public override bool CanResetValue(object component) {
            throw new NotImplementedException();
        }

        public override object GetValue(object component) => MemberInfo.Value;

        public override void ResetValue(object component) {
            throw new NotImplementedException();
        }

        public override void SetValue(object component, object value) {
            throw new NotImplementedException();
        }

        public override bool ShouldSerializeValue(object component) {
            throw new NotImplementedException();
        }
    }
    
    
    
}
