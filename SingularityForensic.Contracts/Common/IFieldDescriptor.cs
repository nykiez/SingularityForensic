using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// 字段自行描述的单位;
    /// </summary>
    public class FieldDescriptor {
        public FieldDescriptor(FieldInfo fieldInfo) {
            this.FieldInfo = fieldInfo;
        }

        public FieldInfo FieldInfo { get; }
        /// <summary>
        /// 键名(语言相关)
        /// </summary>
        public string KeyName { get; set; }
        /// <summary>
        /// 字段大小;
        /// </summary>
        public int FieldSize { get; set; }
        /// <summary>
        /// 值;
        /// </summary>
        public string Value { get; set; }

        ///删除原因,在顺序的字段描述中,既然具有了字段大小,就无需设定字段偏移了;
        ///public int FieldOffset { get; set; }
    }
    
    public interface ICustomFieldDecriptor {
        IEnumerable<FieldDescriptor> GetDescriptors();
        /// <summary>
        /// 所描述实体的类型;
        /// </summary>
        Type ObjectType { get; }
    }

    /// <summary>
    /// 针对非托管结构体的描述单位;
    /// </summary>
    /// <typeparam name="TStruct"></typeparam>
    public abstract class StructFieldDecriptorBase<TStruct> : ICustomFieldDecriptor where TStruct :struct {
        public StructFieldDecriptorBase(TStruct structInstance) {
            this.ObjectType = typeof(TStruct);
            this.StructInstance = structInstance;
        }

        //键的前缀名;
        public string PrefixName { get; protected set; }

        public Type ObjectType { get; }
        public TStruct StructInstance { get; }

        private List<FieldDescriptor> _descriprors;
        public IEnumerable<FieldDescriptor> GetDescriptors() {
            if(_descriprors != null) {
                return _descriprors;
            }

            _descriprors = new List<FieldDescriptor>();

            foreach (var fieldInfo in ObjectType.GetFields()) {
                var descriptor = new FieldDescriptor(fieldInfo);
                EditFieldDecriptor(descriptor);
                _descriprors.Add(descriptor);
            }

            return _descriprors;
        }

        protected virtual void EditFieldDecriptor(FieldDescriptor descriptor) {
            EditFieldDescriptorKeyName(descriptor);
            EditFieldDescriptorValue(descriptor);
            EditFieldDescriptorSize(descriptor);
        }

        protected virtual void EditFieldDescriptorKeyName(FieldDescriptor descriptor) {
            descriptor.KeyName = $"{PrefixName}{descriptor.FieldInfo.Name}";
        }

        protected virtual void EditFieldDescriptorValue(FieldDescriptor descriptor) {
            var fieldInfo = descriptor.FieldInfo;

            //若为字节数组,则访问ByteExtensions,获取值;
            if (fieldInfo.FieldType == typeof(byte[])) {
                var bts = fieldInfo.GetValue(StructInstance) as byte[];
                if (bts.Length < 16) {
                    descriptor.Value = bts.BytesToHexString();
                }
                else {
                    descriptor.Value = "blob";
                }
            }
            //否则直接调用ToString();
            else {
                try {
                    descriptor.Value = fieldInfo.GetValue(StructInstance).ToString();
                }
                catch(Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }
        }

        protected virtual void EditFieldDescriptorSize(FieldDescriptor descriptor) {
            var fieldInfo = descriptor.FieldInfo;
            //若为字节数组,则访问MarshalAsAttribute,获取大小;
            if (fieldInfo.FieldType == typeof(byte[])) {
                var attr = Attribute.GetCustomAttribute(fieldInfo, typeof(MarshalAsAttribute)) as MarshalAsAttribute;
                if (attr == null) {
                    LoggerService.WriteCallerLine($"{nameof(attr)} can't be null.");
                    return;
                }
                descriptor.FieldSize = attr.SizeConst;
            }
            //否则直接使用Marshal.SizeOf获取大小;
            else {
                try {
                    descriptor.FieldSize = Marshal.SizeOf(fieldInfo.FieldType);
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            
        }
    }
}
