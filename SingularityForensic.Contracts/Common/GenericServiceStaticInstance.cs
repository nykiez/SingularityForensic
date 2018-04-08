using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// 服务提供者静态实例提供器;
    /// </summary>
    public class GenericServiceStaticInstance<TService> where TService : class {
        //public GenericServiceStaticInstance(string key = null) {
        //    this._key = key;
        //}
        //private string _key;
        private static TService _service;
        public static TService Current => _service ?? (_service = ServiceProvider.Current?.GetInstance<TService>());
    }
}
