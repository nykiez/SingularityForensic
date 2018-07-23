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
    /// 案件加载完成后尝试加载文件的哈希集状态;
    /// </summary>
    [Export(typeof(ICaseLoadedEventHandler))]
    class OnCaseLoadedOnLoadFileHashSetHandler : ICaseLoadedEventHandler {
        public int Sort => 4;

        public bool IsEnabled => true;

        public void Handle() {
            try {
                HashSetStatusManagementService.BeginOpen();
                var statuss = HashSetStatusManagementService.GetAllHashSetStatus();
                var allHashSets = HashSetManagementService.HashSets;

                foreach (var status in statuss) {
                    //判断类型是否为文件的哈希集;
                    if(status.StatusType != Constants.HashSetsStatusType_File) {
                        continue;
                    }

                    if(status.HashSetGuids == null) {
                        continue;
                    }
#if DEBUG
                    //if (status.UnitName.StartsWith("9")) {

                    //}
#endif
                    //从文件系统中找到对应的文件;
                    var file = FileSystemService.Current.GetFile(status.UnitName);
                    if(file == null) {
                        continue;
                    }

                    //找出属于包含了文件的哈希集集合;
                    var hashSets = allHashSets.
                        Where(p => status.HashSetGuids.Contains(p.GUID)).
                        ToArray();
                    if(hashSets.Length == 0) {
                        continue;
                    }

                    file.ExtensibleTag.SetInstance<IHashSet[]>(hashSets, Constants.FileTag_HashSets);
                }
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
            finally {
                HashSetStatusManagementService.EndOpen();
            }
        }
    }
}
