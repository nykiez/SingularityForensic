using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Test {
    /// <summary>
    /// 输出提供容器(mef)依赖项提供器;
    /// </summary>
    public class ExportProviderServiceProvider : EmptyServiceProvider<ExportProviderServiceProvider> {
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

        public override TService GetInstance<TService>() {
            if (dics.ContainsKey(typeof(TService))) {
                return (TService)dics[typeof(TService)];
            }
            return (TService)GetInstance(typeof(TService));
        }
        
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
            return GetInstance(serviceType, null);
        }
    }

    class TestCommon {
        //初始化ServiceProvider等;
        public static void InitializeTest() {
            ServiceProvider.SetServiceProvider(ExportProviderServiceProvider.StaticInstance);
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(SingularityForensic.Dummy).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(SingularityForensic.Contracts.Dummy).Assembly));

            var container = new CompositionContainer(catalog);

            ExportProviderServiceProvider.StaticInstance.ExportProvider = container;
        }
    }
}
