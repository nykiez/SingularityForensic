using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Common.ComponentModel;
using SingularityForensic.Contracts.TreeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SingularityForensic.Contracts.Hex {
    public static class HexExtensions {
        /// <summary>
        /// 高亮结构体背景与ToolTip,PropertyGrid;
        /// </summary>
        /// <param name="hexDataContext"></param>
        /// <param name="offset">绝对偏移量</param>
        /// <param name="originBrush">默认颜色</param>
        /// <param name="fieldPrefix">字段高亮前缀值(语言相关)</param>
        /// <param name="highlightBrush">高亮颜色</param>
        /// <param name="languageKeyPrefix">语言前缀类型</param>
        public static void LoadCustomTypeDescriptor(
            this IHexDataContext hexDataContext,
            ICustomMemberDescriptor customMemberDescriptor,
            long offset,
            Brush originBrush,
            Brush highlightBrush
        ) {
            var memberInfos = customMemberDescriptor.GetMemberInfos();

            var fieldOffset = 0;
            var toolTipAndBrushBlockTuples = new List<(IToolTipDataItem dataItem, IBrushBlock brushBlock,IMemberInfo memberInfo)?>();

            foreach (var memberInfo in memberInfos) {
                var dataItem = ToolTipItemFactory.CreateIToolTipDataItem();
                var brushBlock = BrushBlockFactory.CreateNewBackgroundBlock();

                dataItem.KeyName = memberInfo.DisplayName + $"({memberInfo.MemberSize})";

                dataItem.Value = memberInfo.Value?.ToString();

                brushBlock.Background = originBrush;
                brushBlock.StartOffset = offset + fieldOffset;
                brushBlock.Length = memberInfo.MemberSize;

                toolTipAndBrushBlockTuples.Add((dataItem, brushBlock,memberInfo));
                fieldOffset += memberInfo.MemberSize;
            }

            toolTipAndBrushBlockTuples.ForEach(p => {
                hexDataContext.CustomBackgroundBlocks.Add(p.Value.brushBlock);
                hexDataContext.CustomDataToolTipItems.Add((offset, fieldOffset, p.Value.dataItem));
            });

            //ToolTip悬停高亮;
            hexDataContext.SelectedToolTipItemChanged += delegate {
                if(hexDataContext.SelectedToolTipItem == null) {
                    return;
                }
                toolTipAndBrushBlockTuples.ForEach(p => {
                    p.Value.brushBlock.Background = originBrush;
                });
                var slTuple = toolTipAndBrushBlockTuples.FirstOrDefault(p => p.Value.dataItem == hexDataContext.SelectedToolTipItem);
                if (slTuple == null) {
                    return;
                }
                slTuple.Value.brushBlock.Background = highlightBrush;
                hexDataContext.UpdateCustomBackgroundBlocks();
            };

            //PropertyGrid部分;
            var (instance, isNew) = hexDataContext.GetOrCreateInstanceInform(Constants.HexDataContextTag_PropertyListDataContext,PropertyGridDataContextFactory.CreateNew);
            var propertyListDataContext = instance;
            if (propertyListDataContext == null) {
                return;
            }
            //若为首次创建,则加入视图;
            if (isNew) {
                hexDataContext.StackGrid.AddChild(propertyListDataContext, new Contracts.Controls.GridChildLength(new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)),1);
            }
            propertyListDataContext.AddCustomMemberDescriptor(customMemberDescriptor);
            

            //PropertyGrid选中属性高亮;
            propertyListDataContext.SelectedMemberInfoChanged += delegate {
                if (propertyListDataContext.SelectedMemberInfo == null) {
                    return;
                }
                toolTipAndBrushBlockTuples.ForEach(p => {
                    p.Value.brushBlock.Background = originBrush;
                });
                var slTuple = toolTipAndBrushBlockTuples.FirstOrDefault(p => p.Value.memberInfo == propertyListDataContext.SelectedMemberInfo);
                if(slTuple == null) {
                    return;
                }

                slTuple.Value.brushBlock.Background = highlightBrush;
                if(hexDataContext.BytePerLine != 0) {
                    hexDataContext.Position = slTuple.Value.brushBlock.StartOffset / hexDataContext.BytePerLine * hexDataContext.BytePerLine;
                }
                hexDataContext.UpdateCustomBackgroundBlocks();
            };
        }

        
    }
}
