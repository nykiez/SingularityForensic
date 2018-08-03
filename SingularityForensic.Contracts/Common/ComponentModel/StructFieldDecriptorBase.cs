using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common.ComponentModel {

    /// <summary>
    /// 针对非托管结构体的描述单位,本类别使用了反射获取各个字段的信息;
    /// </summary>
    /// <typeparam name="TStruct"></typeparam>
    public abstract class StructFieldDecriptorBase<TStruct> : ICustomMemberDescriptor where TStruct : struct {
        public StructFieldDecriptorBase(TStruct structInstance) {
            this.StructInstance = structInstance;
            this.ObjectType = typeof(TStruct);
        }

        public Type ObjectType { get; }
        public TStruct StructInstance { get; }

        private List<IMemberInfo> _descriprors;
        public IEnumerable<IMemberInfo> GetMemberInfos() {
            if (_descriprors != null) {
                return _descriprors;
            }

            _descriprors = new List<IMemberInfo>();

            foreach (var fieldInfo in ObjectType.GetFields()) {
                var descriptor = new FieldMemberInfo(fieldInfo);
                var arg = new CancelEventArgs();
                OnEditMemberDescriptorOverride(fieldInfo, arg);
                if (arg.Cancel) {
                    continue;
                }
                EditFieldDecriptor(descriptor);
                _descriprors.Add(descriptor);
            }

            return _descriprors;
        }

        protected virtual void OnEditMemberDescriptorOverride(MemberInfo memberInfo, CancelEventArgs arg) {

        }

        private void EditFieldDecriptor(FieldMemberInfo descriptor) {
            var stringEventArgs = new EditingValueEventArgs<string>();

            OnEditFieldDescriptorMemberName(descriptor.FieldInfo, stringEventArgs);
            descriptor.MemberName = stringEventArgs.Value;

            stringEventArgs.Value = null;
            OnEditFieldDescriptorDisplayName(descriptor.FieldInfo, stringEventArgs);
            descriptor.DisplayName = stringEventArgs.Value;

            stringEventArgs.Value = null;
            OnEditFieldDescriptorStringValue(descriptor.FieldInfo, stringEventArgs);
            descriptor.Value = stringEventArgs.Value;

            var szEventArgs = new EditingValueEventArgs<int>();
            OnEditFieldDescriptorSize(descriptor.FieldInfo, szEventArgs);
            descriptor.MemberSize = szEventArgs.Value;
        }

        protected virtual void OnEditFieldDescriptorMemberName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = fieldInfo.Name;
        }

        protected abstract void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args);

        protected virtual void OnEditFieldDescriptorStringValue(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            //若为字节数组,则访问ByteExtensions,获取值;
            if (fieldInfo.FieldType == typeof(byte[])) {
                if (!(fieldInfo.GetValue(StructInstance) is byte[] bts)) {
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
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }
        }

        protected virtual void OnEditFieldDescriptorSize(FieldInfo fieldInfo, EditingValueEventArgs<int> args) {
            //若为字节数组,则访问MarshalAsAttribute,获取大小;
            if (fieldInfo.FieldType == typeof(byte[])) {
                if (!(Attribute.GetCustomAttribute(fieldInfo, typeof(MarshalAsAttribute)) is MarshalAsAttribute attr)) {
                    LoggerService.WriteCallerLine($"{nameof(attr)} can't be null.");
                    return;
                }
                args.Value = attr.SizeConst;
            }
            else if (fieldInfo.FieldType == typeof(int[]) || fieldInfo.FieldType == typeof(uint[])) {
                if (!(Attribute.GetCustomAttribute(fieldInfo, typeof(MarshalAsAttribute)) is MarshalAsAttribute attr)) {
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
}
