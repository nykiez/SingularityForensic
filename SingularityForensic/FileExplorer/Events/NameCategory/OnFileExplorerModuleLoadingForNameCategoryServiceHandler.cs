using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 模块加载时,初始化名称类别服务;
    /// </summary>
    [Export(typeof(IFileExplorerModuleLoadingEventHandler))]
    class OnFileExplorerModuleLoadingForNameCategoryServiceHandler : IFileExplorerModuleLoadingEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle() {
            try {

                NameCategoryService.Current.Initialize();
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
        }
    }
}
