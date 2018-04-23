using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Test.App;
using SingularityForensic.Test.Casing;
using SingularityForensic.Test.Common;
using SingularityForensic.Test.Shell;
using System.ComponentModel.Composition.Hosting;

namespace SingularityForensic.Test {
    /// <summary>
    /// //业务逻辑测试时必须调用这个类的InitializeTest方法;
    /// </summary>
    public static class TestCommon {
        /// <summary>
        /// 初始化ServiceProvider等;
        /// </summary>
        public static void InitializeTest() {
            ServiceProvider.SetServiceProvider(ExportProviderServiceProviderMocker.StaticInstance);
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Dummy).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Contracts.Dummy).Assembly));
            
            var container = new CompositionContainer(catalog);

            ExportProviderServiceProviderMocker.StaticInstance.ExportProvider = container;
           

            //设定案件MessageBoxMocker;
            ExportProviderServiceProviderMocker.StaticInstance.SetInstance(CaseMockers.CaseDialogServiceMocker);

            //设定事件聚合器;
            ExportProviderServiceProviderMocker.StaticInstance.SetInstance(CommonMockers.AggregatorMocker);

            //设定视图查找器;
            ViewProvider.SetViewProvider(CommonMockers.ViewProviderMocker);

            //App相关Mocker;
            ExportProviderServiceProviderMocker.StaticInstance.SetInstance(AppMockers.MsgBoxMocker);
            ExportProviderServiceProviderMocker.StaticInstance.SetInstance(AppMockers.ThreadInvokerMocker);
            ExportProviderServiceProviderMocker.StaticInstance.SetInstance(AppMockers.DialogMocker);
            ExportProviderServiceProviderMocker.StaticInstance.SetInstance(AppMockers.LanguageDictObjectMocker);
            ExportProviderServiceProviderMocker.StaticInstance.SetInstance(AppMockers.LocalExplorerServiceMocker);

            DocumentService.MainDocumentService.Initialize();

        }
    }
}
