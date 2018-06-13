using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// 可拓展绑定基类;
    /// </summary>
    public abstract class ExtensibleBindableBase: BindableBase,IExtensible {
        private ExtensibleObject _extensibleBase = new ExtensibleObject();

        public TInstance GetIntance<TInstance>(string extName) => _extensibleBase.GetIntance<TInstance>(extName);

        public void SetInstance<TInstance>(TInstance instance, string extName) => _extensibleBase.SetInstance<TInstance>(instance, extName);
    }
}
