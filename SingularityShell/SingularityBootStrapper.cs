using Microsoft.Practices.ServiceLocation;
using Prism.Mef;
using Prism.Modularity;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Contracts.Splash;
using System;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;

namespace SingularityShell {
    public class SingularityBootStrapper : MefBootstrapper {
        protected override void ConfigureAggregateCatalog() {
            base.ConfigureAggregateCatalog();

            //契约模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(SingularityForensic.Contracts.Dummy).Assembly));

            //框架模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(SingularityForensic.Dummy).Assembly));


            ////取证信息模块;
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(InfoModule).Assembly));

            //安卓模块;
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(SingularityForensic.Android.Dummy).Assembly));
            //////this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(RelevanceModule).Assembly));


            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType => {
                var viewSpace = viewType.Namespace;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly;
                var viewName = viewType.Name;
                try {
                    var lowerSpace = viewSpace.Substring(0, viewSpace.LastIndexOf("Views"));
                    var viewModelName = $"{lowerSpace}ViewModels.{viewName}ViewModel,{viewAssemblyName}";
                    return Type.GetType(viewModelName);
                }
                catch {
                    return null;
                }
            });
        }


        protected override IModuleCatalog CreateModuleCatalog() {
            return new ConfigurationModuleCatalog();
        }

        protected override DependencyObject CreateShell() {
            ServiceProvider.SetServiceProvider(new PracticeServiceProvider(ServiceLocator.Current));
            return this.Container.GetExportedValue<IShell>() as DependencyObject;
        }

        protected override void InitializeModules() {
            var splashService = ServiceProvider.Current.GetInstance<ISplashService>();
            splashService.ShowSplash();
            //初始化语言;
            ServiceProvider.Current.GetInstance<ILanguageService>()?.Initialize();
            base.InitializeModules();
            splashService.CloseSplash();
        }

        protected override void InitializeShell() {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }


    }
}
