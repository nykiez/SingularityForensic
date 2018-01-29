using Microsoft.Practices.ServiceLocation;
using Prism.Mef;
using Prism.Modularity;
using Prism.Mvvm;
using Singularity.Contracts;
using Singularity.Contracts.Common;
using Singularity.Previewers;
using Singularity.UI.AdbViewer;
using Singularity.UI.Case;
using Singularity.UI.FileExplorer;
using Singularity.UI.FileSystem;
using Singularity.UI.FileSystem.Android;
using Singularity.UI.Hex;
using Singularity.UI.Info;
using Singularity.UI.Info.Android;
using Singularity.UI.ITunes;
using SingularityForensic;
using SingularityForensic.Modules.Shell.Models;
using System;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;

namespace SingularityShell {
    public class SingularityBootStrapper:MefBootstrapper {
        protected override void ConfigureAggregateCatalog() {
            base.ConfigureAggregateCatalog();
            
            //主模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MainModule).Assembly));
            //框架模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Dummy).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CaseModule).Assembly));
            ////取证信息模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(InfoModule).Assembly));

            //////功能模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AdbViewerModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AndroidInfoModule).Assembly));
            //////this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(RelevanceModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ITunesModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(HexModule).Assembly));

            //////默认预览器模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DefaultPreviewerModule).Assembly));

            //文件系统模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(FileSystemModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ExplorerModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AndroidFSModule).Assembly));


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

        protected override void InitializeShell() {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }
        
        
    }
}
