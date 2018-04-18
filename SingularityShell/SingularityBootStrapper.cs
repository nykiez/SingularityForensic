using Microsoft.Practices.ServiceLocation;
using Prism.Mef;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using SingularityForensic.Common;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Contracts.Splash;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Threading;
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
            ViewProvider.SetViewProvider(new ServiceProviderViewProvider(ServiceProvider.Current));
            //因为各个模块都可能用到语言服务,必须先初始化语言服务;
            LanguageService.Current.Initialize();
            return ViewProvider.GetView(SingularityForensic.Contracts.Shell.Constants.ShellView) as DependencyObject;
        }

        protected override void InitializeModules() {
            var splashService = ServiceProvider.Current.GetInstance<ISplashService>();
            splashService.ShowSplash();
            Thread.Sleep(3000);
            base.InitializeModules();
            splashService.CloseSplash();
        }

        protected override void InitializeShell() {
            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
