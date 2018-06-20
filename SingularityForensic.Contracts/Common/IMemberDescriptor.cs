using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public interface IMemberInfo {
        /// <summary>
        /// 键名
        /// </summary>
        string KeyName { get; }
        /// <summary>
        /// 成员大小;
        /// </summary>
        int MemberSize { get; }
        /// <summary>
        /// 字符串值;
        /// </summary>
        string StringValue { get; }
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
        /// 键名
        /// </summary>
        public string KeyName { get; internal set; }
        /// <summary>
        /// 字段大小;
        /// </summary>
        public int MemberSize { get; internal set; }
        /// <summary>
        /// 值;
        /// </summary>
        public string StringValue { get; internal set; }

        ///删除原因,在顺序的字段描述中,既然具有了字段大小,就无需设定字段偏移了;
        ///public int FieldOffset { get; set; }
    }
    
    public interface ICustomMemberDecriptor {
        IEnumerable<IMemberInfo> GetMemberInfos();
        /// <summary>
        /// 所描述实体的类型;
        /// </summary>
        Type ObjectType { get; }
    }

    /// <summary>
    /// 针对非托管结构体的描述单位,本类别使用了反射获取各个字段的信息;
    /// </summary>
    /// <typeparam name="TStruct"></typeparam>
    public abstract class StructFieldDecriptorBase<TStruct> : ICustomMemberDecriptor where TStruct :struct {
        public StructFieldDecriptorBase(TStruct structInstance) {
            this.ObjectType = typeof(TStruct);
            this.StructInstance = structInstance;
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

            OnEditFieldDescriptorKeyName(descriptor.FieldInfo,stringEventArgs);
            descriptor.KeyName = stringEventArgs.EditingValue;

            stringEventArgs.EditingValue = null;
            OnEditFieldDescriptorStringValue(descriptor.FieldInfo,stringEventArgs);
            descriptor.StringValue = stringEventArgs.EditingValue;

            var szEventArgs = new EditingValueEventArgs<int>();
            OnEditFieldDescriptorSize(descriptor.FieldInfo,szEventArgs);
            descriptor.MemberSize = szEventArgs.EditingValue;
        }

        protected virtual void OnEditFieldDescriptorKeyName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.EditingValue = fieldInfo.Name;
        }

        protected virtual void OnEditFieldDescriptorStringValue(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            //若为字节数组,则访问ByteExtensions,获取值;
            if (fieldInfo.FieldType == typeof(byte[])) {
                var bts = fieldInfo.GetValue(StructInstance) as byte[];
                if (bts.Length < 16) {
                    args.EditingValue = bts.BytesToHexString();
                }
                else {
                    args.EditingValue = "blobs";
                }
            }
            //否则直接调用ToString();
            else {
                try {
                    args.EditingValue = fieldInfo.GetValue(StructInstance).ToString();
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
                args.EditingValue = attr.SizeConst;
            }
            else if(fieldInfo.FieldType == typeof(int[]) || fieldInfo.FieldType == typeof(uint[])) {
                var attr = Attribute.GetCustomAttribute(fieldInfo, typeof(MarshalAsAttribute)) as MarshalAsAttribute;
                if (attr == null) {
                    LoggerService.WriteCallerLine($"{nameof(attr)} can't be null.");
                    return;
                }
                args.EditingValue = attr.SizeConst * 4;
            }
            //否则直接使用Marshal.SizeOf获取大小;
            else {
                try {
                    args.EditingValue = Marshal.SizeOf(fieldInfo.FieldType);
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            
        }


    }

    public class EditingValueEventArgs<T> : EventArgs {
        public T EditingValue { get; set; }
    }
    
}
