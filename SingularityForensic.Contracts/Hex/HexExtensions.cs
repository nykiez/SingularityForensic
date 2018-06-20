using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SingularityForensic.Contracts.Hex {
    public static class HexExtensions {
        /// <summary>
        /// 高亮结构体背景与ToolTip;
        /// </summary>
        /// <param name="hexDataContext"></param>
        /// <param name="offset">绝对偏移量</param>
        /// <param name="originBrush">默认颜色</param>
        /// <param name="fieldPrefix">字段高亮前缀值(语言相关)</param>
        /// <param name="highlightBrush">高亮颜色</param>
        /// <param name="languageKeyPrefix">语言前缀类型</param>
        public static void UpdateDescriptorBackgroundAndToolTips(
            this IHexDataContext hexDataContext,
            ICustomMemberDecriptor customMemberDescriptor,
            long offset,
            Brush originBrush,
            Brush highlightBrush,
            string languageKeyPrefix
        ) {
            var descriptors = customMemberDescriptor.GetMemberInfos();

            var fieldOffset = 0;
            var toolTipAndBrushBlockTuples = new List<(IToolTipDataItem dataItem, IBrushBlock brushBlock)?>();

            foreach (var fieldDescriptor in descriptors) {
                var dataItem = ToolTipItemFactory.CreateIToolTipDataItem();
                var brushBlock = BrushBlockFactory.CreateNewBackgroundBlock();

                dataItem.KeyName =
                    LanguageService.FindResourceString($"{languageKeyPrefix}{fieldDescriptor.MemberName}") +
                    $"({fieldDescriptor.MemberSize})";

                dataItem.Value = fieldDescriptor.StringValue;

                brushBlock.Background = originBrush;
                brushBlock.StartOffset = offset + fieldOffset;
                brushBlock.Length = fieldDescriptor.MemberSize;

                toolTipAndBrushBlockTuples.Add((dataItem, brushBlock));
                fieldOffset += fieldDescriptor.MemberSize;
            }

            toolTipAndBrushBlockTuples.ForEach(p => {
                hexDataContext.CustomBackgroundBlocks.Add(p.Value.brushBlock);
                hexDataContext.CustomDataToolTipItems.Add((offset, fieldOffset, p.Value.dataItem));
            });

            //悬停高亮;
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
        }
    }
}
