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
        public TInstance GetIntance<TInstance>(string extName) => GetIntanceCore<TInstance>(extName);
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

        protected virtual TInstance GetIntanceCore<TInstance>(string extName) {
            var ins = GetInstance(typeof(TInstance), extName);
            if (ins != null) {
                return (TInstance)ins;
            }
            return default(TInstance);
        }
    }

    public abstract class ExtensibleBase:ReadOnlyExtensibleBase {
        public virtual TInstance GetIntance<TInstance>(string extName) => GetIntanceCore<TInstance>(extName);

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
