using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// 可拓展基类;
    /// </summary>
    public class ExtensibleObject : IInstanceExtensible {
        private class InstanceCell {
            public Type InstanceType { get; set; }
            public string ExtName { get; set; }
            public object Instance { get; set; }
        }

        /// <summary>
        /// 实例保存栈;
        /// </summary>
        private List<InstanceCell> _stateStack =
            new List<InstanceCell>();

        public TInstance GetIntance<TInstance>(string extName) {
            var ins = GetInstance(typeof(TInstance), extName);
            if (ins != null) {
                return (TInstance)ins;
            }
            return default(TInstance);
        }

        private object GetInstance(Type instanceType, string extName) {
            var cell = _stateStack.FirstOrDefault(p => p.InstanceType == instanceType
            && p.ExtName == extName);

            if (cell == null) {
                return null;
            }

            if(cell.InstanceType == instanceType) {
                return cell.Instance;
            }

            throw new InvalidCastException($"{nameof(cell.Instance)} can't be cast to {instanceType}.");
        }

        public void SetInstance<TInstance>(TInstance instance, string extName) {
            var tuple = _stateStack.FirstOrDefault(p => p.InstanceType == typeof(TInstance)
            && p.ExtName == extName);

            if(tuple != null) {
                tuple.Instance = instance;
            }
            else {
                _stateStack.Add(new InstanceCell {
                    Instance = instance,
                    ExtName = extName,
                    InstanceType = typeof(TInstance)
                });
            }
        }
    }

    /// <summary>
    /// 可拓展绑定基类;
    /// </summary>
    public abstract class ExtensibleBindableBase: BindableBase,IInstanceExtensible {
        private ExtensibleObject _extensibleBase = new ExtensibleObject();

        public TInstance GetIntance<TInstance>(string extName) => _extensibleBase.GetIntance<TInstance>(extName);

        public void SetInstance<TInstance>(TInstance instance, string extName) => _extensibleBase.SetInstance<TInstance>(instance, extName);
    }
}
