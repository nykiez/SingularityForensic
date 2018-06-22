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

namespace SingularityForensic.Contracts.Common {
    public class CustomTypeDescriptorWrapper : CustomTypeDescriptor {
        private PropertyDescriptorCollection _properties = null;
        public IEnumerable<ICustomMemberDescriptor> CustomMemberDescriptors => _customMemberDescriptors.Select(p => p.CustomMemberDescriptor);
        private List<NestedDescriptor> _customMemberDescriptors = new List<NestedDescriptor>();

        public void CompositeCustomMemberDecriptor(ICustomMemberDescriptor descriptor) {
            _customMemberDescriptors.Add(new NestedDescriptor {
                CustomMemberDescriptor = descriptor
            });
        }

        public override PropertyDescriptorCollection GetProperties() {
            if (_properties == null) {
                _properties = new PropertyDescriptorCollection(
                    _customMemberDescriptors.SelectMany(
                        p => p.CustomMemberDescriptor.GetMemberInfos().
                            Select(
                                        q => new PropertyDescriptorWrapper(q) { GroupName = p.CustomMemberDescriptor.DisplayName }
                            )
                        ).ToArray()
                    );
            }
            return _properties;
        }

        class NestedDescriptor {
            public ICustomMemberDescriptor CustomMemberDescriptor { get; set; }
        }
    }



    public class PropertyDescriptorWrapper : PropertyDescriptor {
        public PropertyDescriptorWrapper(IMemberInfo memberInfo):base(memberInfo.MemberName, null) {
            this.MemberInfo = memberInfo ?? throw new ArgumentNullException(nameof(memberInfo));
        }

        public IMemberInfo MemberInfo { get; }
        public override string Name => MemberInfo.MemberName;
        public override string DisplayName => MemberInfo.DisplayName;
        public string GroupName { get; set; }
        private AttributeCollection _attributes;
        public override AttributeCollection Attributes {
            get {
                if (_attributes == null) {
                    _attributes = new AttributeCollection(new Attribute[] { new DisplayAttribute {
                        GroupName = GroupName
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
    

    public interface IMemberInfo {
        /// <summary>
        /// 成员名;
        /// </summary>
        string MemberName { get; }
        /// <summary>
        /// 显示名;
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// 成员大小;
        /// </summary>
        int MemberSize { get; }
        /// <summary>
        /// 值;
        /// </summary>
        object Value { get; }
        /// <summary>
        /// 成员类型;
        /// </summary>
        Type MemberType { get; }
    }

    /// <summary>
    /// 字段自行描述的单位;
    /// </summary>
    public class FieldMemberInfo:IMemberInfo {
        public FieldMemberInfo(FieldInfo fieldInfo) {
            this.FieldInfo = fieldInfo??throw new ArgumentNullException(nameof(fieldInfo));
        }

        public FieldInfo FieldInfo { get; }
        /// <summary>
        /// 成员名
        /// </summary>
        public string MemberName { get; internal set; }
        /// <summary>
        /// 字段大小;
        /// </summary>
        public int MemberSize { get; internal set; }
        /// <summary>
        /// 值;
        /// </summary>
        public object Value { get; internal set; }

        public Type MemberType => FieldInfo.FieldType;

        public string DisplayName { get;internal set; }

        ///删除原因,在顺序的字段描述中,既然具有了字段大小,就无需设定字段偏移了;
        ///public int FieldOffset { get; set; }
    }
    
    public interface ICustomMemberDescriptor {
        IEnumerable<IMemberInfo> GetMemberInfos();
        Type ObjectType { get; }
        string DisplayName { get; }
        string Name { get; }
    }

    /// <summary>
    /// 针对非托管结构体的描述单位,本类别使用了反射获取各个字段的信息;
    /// </summary>
    /// <typeparam name="TStruct"></typeparam>
    public abstract class StructFieldDecriptorBase<TStruct> :  ICustomMemberDescriptor where TStruct :struct {
        public StructFieldDecriptorBase(TStruct structInstance) {
            this.StructInstance = structInstance;
            this.ObjectType = typeof(TStruct);
        }
        
        public Type ObjectType { get; }
        public TStruct StructInstance { get; }

        private List<IMemberInfo> _descriprors;
        public IEnumerable<IMemberInfo> GetMemberInfos() {
            if(_descriprors != null) {
                return _descriprors;
            }

            _descriprors = new List<IMemberInfo>();

            foreach (var fieldInfo in ObjectType.GetFields()) {
                var descriptor = new FieldMemberInfo(fieldInfo);
                var arg = new CancelEventArgs();
                OnEditMemberDescriptorOverride(fieldInfo,arg);
                if (arg.Cancel) {
                    continue;
                }
                EditFieldDecriptor(descriptor);
                _descriprors.Add(descriptor);
            }

            return _descriprors;
        }

        protected virtual void OnEditMemberDescriptorOverride(MemberInfo memberInfo,CancelEventArgs arg) {

        } 

        private void EditFieldDecriptor(FieldMemberInfo descriptor) {
            var stringEventArgs = new EditingValueEventArgs<string>();

            OnEditFieldDescriptorMemberName(descriptor.FieldInfo,stringEventArgs);
            descriptor.MemberName = stringEventArgs.Value;

            stringEventArgs.Value = null;
            OnEditFieldDescriptorDisplayName(descriptor.FieldInfo, stringEventArgs);
            descriptor.DisplayName = stringEventArgs.Value;

            stringEventArgs.Value = null;
            OnEditFieldDescriptorStringValue(descriptor.FieldInfo,stringEventArgs);
            descriptor.Value = stringEventArgs.Value;

            var szEventArgs = new EditingValueEventArgs<int>();
            OnEditFieldDescriptorSize(descriptor.FieldInfo,szEventArgs);
            descriptor.MemberSize = szEventArgs.Value;

            

        }

        protected virtual void OnEditFieldDescriptorMemberName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = fieldInfo.Name;
        }
        protected abstract void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args);

        protected virtual void OnEditFieldDescriptorStringValue(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            //若为字节数组,则访问ByteExtensions,获取值;
            if (fieldInfo.FieldType == typeof(byte[])) {
                var bts = fieldInfo.GetValue(StructInstance) as byte[];

                if(bts == null) {
                    args.Value = null;
                }
                else if (bts.Length < 16) {
                    args.Value = bts.BytesToHexString();
                }
                else {
                    args.Value = "blobs";
                }
            }
            //否则直接调用ToString();
            else {
                try {
                    args.Value = fieldInfo.GetValue(StructInstance).ToString();
                }
                catch(Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }
        }

        protected virtual void OnEditFieldDescriptorSize(FieldInfo fieldInfo, EditingValueEventArgs<int> args) {
            //若为字节数组,则访问MarshalAsAttribute,获取大小;
            if (fieldInfo.FieldType == typeof(byte[])) {
                var attr = Attribute.GetCustomAttribute(fieldInfo, typeof(MarshalAsAttribute)) as MarshalAsAttribute;
                if (attr == null) {
                    LoggerService.WriteCallerLine($"{nameof(attr)} can't be null.");
                    return;
                }
                args.Value = attr.SizeConst;
            }
            else if(fieldInfo.FieldType == typeof(int[]) || fieldInfo.FieldType == typeof(uint[])) {
                var attr = Attribute.GetCustomAttribute(fieldInfo, typeof(MarshalAsAttribute)) as MarshalAsAttribute;
                if (attr == null) {
                    LoggerService.WriteCallerLine($"{nameof(attr)} can't be null.");
                    return;
                }
                args.Value = attr.SizeConst * 4;
            }
            //否则直接使用Marshal.SizeOf获取大小;
            else {
                try {
                    args.Value = Marshal.SizeOf(fieldInfo.FieldType);
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            
        }

        public abstract string DisplayName { get; }
        public virtual string Name { get; }
    }

    public interface ICompositeCustomMemberDecriptor:ICustomMemberDescriptor {
        //void Composite(ICustomMemberDescriptor );
    }

    public class CompositeCustomMemberDescriptor {

    }
}
