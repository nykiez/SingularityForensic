using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events.Hash {
    /// <summary>
    /// 案件加载完成后尝试加载文件的哈希值状态;
    /// </summary>
    [Export(typeof(ICaseLoadedEventHandler))]
    public class OnCaseLoadedOnLoadFileHashHandler : ICaseLoadedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle() {
            try {
                HashStatusManagementService.BeginOpen();
                var statuss = HashStatusManagementService.GetAllHashValueStatus();
                foreach (var status in statuss) {
                    var metaGUID = $"{Constants.FileHashMetaDataProvider_GUIDPrefix}{status.HasherGUID}";
                    if (status.StatusType != Constants.HashValueStatusType_File) {
                        continue;
                    }

                    var file = FileSystemService.Current.GetFile(status.Name);
                    if(file == null) {
                        continue;
                    }

                    file.ExtensibleTag.SetInstance(status.Value, metaGUID);
                }
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
            }
            finally {
                HashStatusManagementService.EndOpen();
            }
        }
    }
}
