using Prism.Mef;
using Prism.Modularity;
using Singularity.Net;
using Singularity.Previewers;
using Singularity.UI.AdbViewer;
using Singularity.UI.Info;
using Singularity.UI.Info.Android;
using Singularity.UI.ITunes;
using SingularityForensic;
using SingularityForensic.Modules.Shell.Models;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace SingularityNet {
    public class SingularityBootStrapper : MefBootstrapper {
        protected override void ConfigureAggregateCatalog() {
            base.ConfigureAggregateCatalog();

            //主模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MainModule).Assembly));

            //取证信息模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(InfoModule).Assembly));

            ////功能模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AdbViewerModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AndroidInfoModule).Assembly));
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(RelevanceModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ITunesModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(HexModule).Assembly));

            ////默认预览器模块;
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DefaultPreviewerModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(NetModule).Assembly));
        }

        protected override IModuleCatalog CreateModuleCatalog() {
            return new ConfigurationModuleCatalog();
        }

        protected override DependencyObject CreateShell() {
            return this.Container.GetExportedValue<IShell>() as DependencyObject;
        }

        protected override void InitializeShell() {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }


    }
}
