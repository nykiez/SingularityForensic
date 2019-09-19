using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace SingularityForensic.Test.Common {
    /// <summary>
    /// 输出提供容器(mef)依赖项提供器;
    /// 本类提供了在运行时动态指定实现实例的功能，以便于部分情况下单元测试的场景;
    /// </summary>
    public class ExportProviderServiceProviderMocker : Contracts.Common.IServiceProvider {
        private static ExportProviderServiceProviderMocker _staticInstance;
        public static ExportProviderServiceProviderMocker StaticInstance => _staticInstance ?? (_staticInstance = new ExportProviderServiceProviderMocker());

        public ExportProvider ExportProvider { get; set; }

        public IEnumerable<TService> GetAllInstances<TService>() => ExportProvider.GetExportedValues<TService>();

        public IEnumerable<object> GetAllInstances(Type serviceType) {
            List<object> instances = new List<object>();



            IEnumerable<Lazy<object, object>> exports = this.ExportProvider.GetExports(serviceType, null, null);

            if (exports != null) {

                instances.AddRange(exports.Select(export => export.Value));

            }



            return instances;
        }

        public TService GetInstance<TService>() => (TService)GetInstance(typeof(TService));

        public object GetInstance(Type serviceType, string key) {
            IEnumerable<Lazy<object, object>> exports = this.ExportProvider.GetExports(serviceType, null, key);

            if ((exports != null) && (exports.Count() > 0)) {

                // If there is more than one value, this will throw an InvalidOperationException, 

                // which will be wrapped by the base class as an ActivationException.

                return exports.Single().Value;
            }

            throw new InvalidOperationException("Export not found");
        }

        private Dictionary<Type, object> dics = new Dictionary<Type, object>();
        public void SetInstance<TService>(TService service) {
            foreach (var item in dics) {
                if (item.Key == typeof(TService)) {
                    throw new InvalidOperationException($"The Type {typeof(TService)} has already been set.");
                }
            }

            dics.Add(typeof(TService), service);
        }

        public object GetInstance(Type serviceType) {
            if (dics.ContainsKey(serviceType) && dics[serviceType] != null) {
                return dics[serviceType];
            }
            return GetInstance(serviceType, null);
        }

        public TService GetInstance<TService>(string key) => (TService)GetInstance(typeof(TService), key);
    }
}
