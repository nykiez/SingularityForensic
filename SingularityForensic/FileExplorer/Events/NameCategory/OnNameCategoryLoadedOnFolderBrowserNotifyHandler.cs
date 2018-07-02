using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events.NameCategory {
    /// <summary>
    /// 名称类别加载时通知文件资源管理器属性变化;
    /// </summary>
    [Export(typeof(INameCategoryDescriptorsLoadedEventHandler))]
    class OnNameCategoryLoadedOnFolderBrowserNotifyHandler : INameCategoryDescriptorsLoadedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle() {
            var dts = FileExplorerDataContextFactory.Current?.GetAllFolderBrowserDataContext();
            if (dts == null) {
                return;
            }

            ThreadInvoker.BackInvoke(() => {
                foreach (var dt in dts) {
                    var files = dt.FolderBrowserViewModel?.Files;
                    if (files == null) {
                        continue;
                    }
                    foreach (var file in files) {
                        file.NotifyProperty(Constants.FileMetaDataGUID_NameCategory);
                    }
                }
            });
        }
    }
}
