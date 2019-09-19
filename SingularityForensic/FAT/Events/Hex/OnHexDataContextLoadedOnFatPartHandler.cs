using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Common.ComponentModel;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.FAT.Events.Hex {
    /// <summary>
    /// 高亮FAT分区信息;
    /// </summary>
    [Export(typeof(IHexDataContextLoadedEventHandler))]
    public class OnHexDataContextLoadedOnFatPartHandler : IHexDataContextLoadedEventHandler {
        public int Sort => 640;

        public bool IsEnabled => true;

        public void Handle(IHexDataContext hexDataContext) {
            if(hexDataContext == null) {
                return;
            }

            var part = hexDataContext.GetInstance<IFile>(Contracts.FileExplorer.Constants.HexDataContextTag_File) as IPartition;
            if(part == null) {
                return;
            }

            if(part.TypeGuid != Constants.PartitionType_FAT32) {
                return;
            }

            var fatPartInfo = part.GetInstance<FATPartInfo>(Constants.PartitionStokenTag_FATPartInfo);
            if(fatPartInfo == null) {
                LoggerService.WriteCallerLine($"{nameof(fatPartInfo)} can't be null.");
                return;
            }

            IEnumerable<(ICustomMemberDescriptor descriptor,long offset,string languagePrefix)> GetDescriptorTuples() {
                yield return (fatPartInfo.FatDBR, fatPartInfo.FatDBR?.Offset??0,Constants.FATFieldPrefix_DBR);
                yield return (fatPartInfo.FatDBR_BackUp, fatPartInfo.FatDBR_BackUp?.Offset ?? 0, Constants.FATFieldPrefix_DBR);
                yield return (fatPartInfo.FatInfo, fatPartInfo.FatInfo?.Offset ?? 0,Constants.FATFieldPrefix_Info);
                yield return (fatPartInfo.FatInfo_BackUp, fatPartInfo.FatInfo_BackUp?.Offset ?? 0,Constants.FATFieldPrefix_Info);
            };

            var infoIndex = 0;
            foreach (var tuple in GetDescriptorTuples().OrderBy(p => p.offset)) {
                if(tuple.descriptor == null) {
                    continue;
                }
                hexDataContext.LoadCustomTypeDescriptor(
                    tuple.descriptor, 
                    tuple.offset,
                    infoIndex % 2 == 0?BrushBlockFactory.FirstBrush:BrushBlockFactory.SecondBrush,
                    BrushBlockFactory.HighLightBrush
                );
                infoIndex++;
            }
        }
    }
}
