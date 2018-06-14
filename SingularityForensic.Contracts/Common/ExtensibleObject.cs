using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// 可拓展基类;
    /// </summary>
    public class ExtensibleObject : ExtensibleBase, IExtensible {
        public void SetInstance<TInstance>(TInstance instance, string extName) => SetInstanceCore(instance, extName);
    }

    class InstanceCell {
        public Type InstanceType { get; set; }
        public string ExtName { get; set; }
        public object Instance { get; set; }
    }

    public class ReadOnlyExtensibleObject : ReadOnlyExtensibleBase, IReadOnlyExtensible {
        public TInstance GetInstance<TInstance>(string extName) => GetInstanceCore<TInstance>(extName);

        public TInstance GetGeneralInstance<TInstance>(string extName) => GetGeneralInstanceCore<TInstance>(extName);
    }

    public abstract class ReadOnlyExtensibleBase {
        /// <summary>
        /// 实例保存栈;
        /// </summary>
        private List<InstanceCell> _stateStack;
        internal List<InstanceCell> StateStack => _stateStack ?? (_stateStack = new List<InstanceCell>());

        private object GetInstance(Type instanceType, string extName) {
            var cell = StateStack.FirstOrDefault(p => p.InstanceType == instanceType
            && p.ExtName == extName);

            if (cell == null) {
                return null;
            }

            if (cell.InstanceType == instanceType) {
                return cell.Instance;
            }

            throw new InvalidCastException($"{nameof(cell.Instance)} can't be cast to {instanceType}.");
        }

        protected virtual TInstance GetInstanceCore<TInstance>(string extName) {
            var ins = GetInstance(typeof(TInstance), extName);
            if (ins != null) {
                return (TInstance)ins;
            }
            return default(TInstance);
        }

        /// <summary>
        /// 类型判断拆箱获取类型;
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="extName"></param>
        /// <returns></returns>
        protected virtual TInstance GetGeneralInstanceCore<TInstance>(string extName) {
            foreach (var item in StateStack) {
                if(item.Instance is TInstance instance) {
                    return instance;
                }
            }

            return default(TInstance);
        }

        
    }

    public abstract class ExtensibleBase:ReadOnlyExtensibleBase {
        public virtual TInstance GetInstance<TInstance>(string extName) => GetInstanceCore<TInstance>(extName);
        public virtual TInstance GetGeneralInstance<TInstance>(string extName) => GetGeneralInstanceCore<TInstance>(extName);

        protected void SetInstanceCore<TInstance>(TInstance instance, string extName) {
            var tuple = StateStack.FirstOrDefault(p => p.InstanceType == typeof(TInstance)
            && p.ExtName == extName);

            if (tuple != null) {
                tuple.Instance = instance;
            }
            else {
                StateStack.Add(new InstanceCell {
                    Instance = instance,
                    ExtName = extName,
                    InstanceType = typeof(TInstance)
                });
            }
        }
    }
}
