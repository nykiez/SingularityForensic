using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.App {
    /// <summary>
    /// 输出提供容器(mef)依赖项提供器;
    /// </summary>
    public class ExportProviderServiceProviderMocker : EmptyServiceProvider<ExportProviderServiceProviderMocker> {
        //private static ExportProviderServiceProvider _staticInstance;
        //public static ExportProviderServiceProvider StaticInstance => _staticInstance ?? (_staticInstance = new ExportProviderServiceProvider());

        public ExportProvider ExportProvider { get; set; }

        public override IEnumerable<TService> GetAllInstances<TService>() => ExportProvider.GetExportedValues<TService>();

        public override IEnumerable<object> GetAllInstances(Type serviceType) {
            List<object> instances = new List<object>();



            IEnumerable<Lazy<object, object>> exports = this.ExportProvider.GetExports(serviceType, null, null);

            if (exports != null) {

                instances.AddRange(exports.Select(export => export.Value));

            }



            return instances;
        }

        public override TService GetInstance<TService>() => (TService)GetInstance(typeof(TService));

        public override object GetInstance(Type serviceType, string key) {
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

        public override object GetInstance(Type serviceType) {
            if (dics.ContainsKey(serviceType)) {
                return dics[serviceType];
            }
            return GetInstance(serviceType, null);
        }

    }
}
