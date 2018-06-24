using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Ext.Events.Hex {
    /// <summary>
    /// 高亮Ext分区信息;
    /// </summary>
    [Export(typeof(IHexDataContextLoadedEventHandler))]
    class OnHexDataContextLoadedOnExtPartHandler : IHexDataContextLoadedEventHandler {
        public int Sort => 629;

        public bool IsEnabled => true;

        public void Handle(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                return;
            }

            var part = hexDataContext.GetInstance<IFile>(Contracts.FileExplorer.Constants.HexDataContextTag_File) as IPartition;
            if (part == null) {
                return;
            }

            if (!part.TypeGuids?.Contains(Constants.PartitionType_Ext) ?? false) {
                return;
            }

            var extPartInfo = part.GetInstance<ExtPartInfo>(Constants.PartitionStokenTag_ExtPartInfo);
            if(extPartInfo == null) {
                LoggerService.WriteCallerLine($"{nameof(extPartInfo)} can't be null.");
                return;
            }

            if(extPartInfo.SuperBlock != null) {
                var stSuperBlock = extPartInfo.SuperBlock.StructInstance;
                hexDataContext.LoadCustomTypeDescriptor(extPartInfo.SuperBlock, Constants.ExtSuperBlockStartIndex, BrushBlockFactory.FirstBrush, BrushBlockFactory.HighLightBrush);
                
                var descStart = (stSuperBlock.s_first_data_block + 1) * stSuperBlock.BlockSize;

                var descIndex = 0;
                if(extPartInfo.Ext4GroupDescs == null) {
                    LoggerService.WriteCallerLine($"{nameof(extPartInfo.Ext4GroupDescs)} can't be null.");
                    return;
                }

                foreach (var desc in extPartInfo.Ext4GroupDescs) {
                    hexDataContext.LoadCustomTypeDescriptor(
                        desc,
                        descStart + descIndex * stSuperBlock.s_desc_size,
                        descIndex % 2 == 0?BrushBlockFactory.FirstBrush:BrushBlockFactory.SecondBrush,
                        BrushBlockFactory.HighLightBrush
                    );
                    descIndex++;
                }
                //extPartInfo.StExt4GroupDescs
            }

            Handle(hexDataContext, extPartInfo);
        }

        
        public void Handle(IHexDataContext hexDataContext,ExtPartInfo extPartInfo) {
            //var propertyGridDataContext = hexDataContext.GetInstance<IPropertyGridDataContext>();
            //if(propertyGridDataContext == null) {
            //    propertyGridDataContext = PropertyGridDataContextFactory.CreateNew();
            //    hexDataContext.StackGrid.AddChild(propertyGridDataContext, new Contracts.Controls.GridChildLength(new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)));
            //    hexDataContext.SetInstance(propertyGridDataContext, HexDataContextTag_PropertyGridDataContext);
            //}

            //if(propertyGridDataContext == null) {
            //    return;
            //}

            //propertyGridDataContext.AddCustomMemberDescriptor(extPartInfo.SuperBlock,LanguageService.FindResourceString(Constants.ExtSuperBlockGroupName));
        }
    }
}
