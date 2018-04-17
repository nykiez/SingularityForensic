using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// 服务提供者静态实例提供器;
    /// </summary>
    public abstract class GenericServiceStaticInstance<TService> where TService : class {
        private static TService _current;
        public static TService Current => _current ?? (_current = ServiceProvider.GetInstance<TService>());
    }

    //public class GenericServiceStaticInstance2<TService> where TService : class {
    //    public GenericServiceStaticInstance2(string keyName) {
    //        KeyName = keyName;
    //    }

    //    public string KeyName { get; private set; }
    //    private TService _current;
    //    public TService Current => _current ?? (_current = ServiceProvider.GetInstance<TService>());
    //}
}
