using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace SingularityForensic.FAT.Events {
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

            if(!(part.TypeGuids?.Contains(Constants.PartitionType_FAT32)??false)) {
                return;
            }

            var fatPartInfo = part.GetInstance<FATPartInfo>(Constants.PartitionStokenTag_FATPartInfo);
            if(fatPartInfo == null) {
                LoggerService.WriteCallerLine($"{nameof(fatPartInfo)} can't be null.");
                return;
            }

            IEnumerable<(ICustomMemerDecriptor descriptor,long offset,string languagePrefix)> GetDescriptorTuples() {
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
                hexDataContext.UpdateDescriptorBackgroundAndToolTips(
                    tuple.descriptor, 
                    tuple.offset,
                    infoIndex % 2 == 0?BrushBlockFactory.FirstBrush:BrushBlockFactory.SecondBrush,
                    BrushBlockFactory.HighLightBrush,
                    tuple.languagePrefix
                );
                infoIndex++;
            }
            
            //UpdateBackgrounds(hexDataContext, fatPartInfo);
            //UpdateToolTips(hexDataContext, fatPartInfo);
        }

        ///// <summary>
        ///// 高亮背景;
        ///// </summary>
        ///// <param name="hexDataContext"></param>
        ///// <param name="fatPartInfo"></param>
        //private void UpdateBackgrounds(IHexDataContext hexDataContext,FATPartInfo fatPartInfo) {
        //    UpdateDBRBackgrounds(hexDataContext, fatPartInfo);
        //    UpdateInfoBackgrounds(hexDataContext, fatPartInfo);
        //    hexDataContext.UpdateCustomBackgroundBlocks();
        //}
        
        ///// <summary>
        ///// 高亮DBR背景;
        ///// </summary>
        ///// <param name="hexDataContext"></param>
        ///// <param name="fatPartInfo"></param>
        //private void UpdateDBRBackgrounds(IHexDataContext hexDataContext,FATPartInfo fatPartInfo) {
        //    var dbr = fatPartInfo.FatDBR;
        //    var dbrBackUp = fatPartInfo.FatDBR_BackUp;
        //    if (dbr != null) {
        //        UpdateStructBackgrounds<StFatDBR>(hexDataContext, dbr.Offset);
        //    }
        //    if (dbrBackUp != null) {
        //        UpdateStructBackgrounds<StFatDBR>(hexDataContext, dbrBackUp.Offset);
        //    }
        //}
        
        ///// <summary>
        ///// 高亮FATInfo背景;
        ///// </summary>
        ///// <param name="hexDataContext"></param>
        ///// <param name="fatPartInfo"></param>
        //private void UpdateInfoBackgrounds(IHexDataContext hexDataContext,FATPartInfo fatPartInfo) {
        //    var info = fatPartInfo.FatInfo;
        //    var infoBackup = fatPartInfo.FatInfo_BackUp;
        //    if(info != null) {
        //        UpdateStructBackgrounds<StFatINFO>(hexDataContext, info.Offset);
        //    }

        //    if (infoBackup != null) {
        //        UpdateStructBackgrounds<StFatINFO>(hexDataContext,infoBackup.Offset);
        //    }
        //}
        
        ///// <summary>
        ///// 高亮ToolTip;
        ///// </summary>
        ///// <param name="hexDataContext"></param>
        ///// <param name="fatPartInfo"></param>
        //private void UpdateToolTips(IHexDataContext hexDataContext,FATPartInfo fatPartInfo) {
        //    UpdateDBRToolTips(hexDataContext, fatPartInfo);
        //    UpdateInfoToolTips(hexDataContext, fatPartInfo);
        //}

        ///// <summary>
        ///// 高亮DBR ToolTip;
        ///// </summary>
        ///// <param name="hexDataContext"></param>
        ///// <param name="fatPartInfo"></param>
        //private void UpdateDBRToolTips(IHexDataContext hexDataContext,FATPartInfo fatPartInfo) {
        //    var dbr = fatPartInfo.FatDBR;
        //    var dbrBackup = fatPartInfo.FatDBR_BackUp;

        //    if(dbr != null) {
        //        UpdateStructOwnerToolTips<StFatDBR>(hexDataContext, dbr.Offset,
        //            LanguageService.FindResourceString(Constants.FAT_Owner),
        //            LanguageService.FindResourceString(Constants.FAT_Owner_DBR)
        //        );
        //        UpdateToolTipsForCustomFieldDecriptor(hexDataContext, dbr, dbr.Offset,Constants.FATFieldPrefix_DBR);
        //    }

        //    if(dbrBackup != null) {
        //        UpdateStructOwnerToolTips<StFatDBR>(hexDataContext, dbrBackup.Offset,
        //            LanguageService.FindResourceString(Constants.FAT_Owner),
        //            LanguageService.FindResourceString(Constants.FAT_Owner_DBR_Backup)
        //        );
        //        UpdateToolTipsForCustomFieldDecriptor(hexDataContext, dbrBackup, dbrBackup.Offset,Constants.FATFieldPrefix_DBR);
        //    }
        //}

        ///// <summary>
        ///// 高亮Info ToolTip;
        ///// </summary>
        ///// <param name="hexDataContext"></param>
        ///// <param name="fatPartInfo"></param>
        //private void UpdateInfoToolTips(IHexDataContext hexDataContext,FATPartInfo fatPartInfo) {
        //    var info = fatPartInfo.FatInfo;
        //    var infoBackup = fatPartInfo.FatInfo_BackUp;
        //    if (info != null) {
        //        UpdateStructOwnerToolTips<StFatINFO>(hexDataContext, info.Offset,
        //            LanguageService.FindResourceString(Constants.FAT_Owner),
        //            LanguageService.FindResourceString(Constants.FAT_Owner_Info));
        //    }

        //    if (infoBackup != null) {
        //        UpdateStructOwnerToolTips<StFatINFO>(hexDataContext, infoBackup.Offset,
        //            LanguageService.FindResourceString(Constants.FAT_Owner),
        //            LanguageService.FindResourceString(Constants.FAT_Owner_Info_Backup));
        //    }


        //}

        //private void UpdateToolTipsForCustomFieldDecriptor(IHexDataContext hexDataContext,ICustomMemerDecriptor customFieldDescriptor,long offset,string languageKeyPrefix)  {
        //    var fieldDescriptors = customFieldDescriptor.GetDescriptors();
        //    var currentOffset = 0;

        //    foreach (var fieldDescriptor in fieldDescriptors) {
        //        var dataItem = ToolTipItemFactory.CreateIToolTipDataItem();
        //        dataItem.KeyName = LanguageService.FindResourceString($"{languageKeyPrefix}{fieldDescriptor.KeyName}");
        //        dataItem.Value = fieldDescriptor.StringValue;
        //        hexDataContext.CustomDataToolTipItems.Add((offset + currentOffset, fieldDescriptor.MemberSize, dataItem));
        //        currentOffset += fieldDescriptor.MemberSize;
        //    }

        //}
        
        //private void UpdateStructOwnerToolTips<TStruct>(
        //   IHexDataContext hexDataContext,
        //   long offset,
        //   string ownerKeyName,
        //   string ownerValueName)
        //   where TStruct : struct {
        //    var stSize = Marshal.SizeOf(typeof(TStruct));
            
        //    hexDataContext.CustomDataToolTipItems.Add(
        //            (offset, stSize,ToolTipItemFactory.CreateIToolTipDataItem(ownerKeyName, ownerValueName))
        //        );
        //}

        ///// <summary>
        ///// 高亮结构体背景;
        ///// </summary>
        ///// <param name="hexDataContext"></param>
        ///// <param name="fatPartInfo"></param>
        ///// <param name="offset">绝对偏移量</param>
        //private void UpdateStructBackgrounds<TStruct>(IHexDataContext hexDataContext, long offset) where TStruct : struct {
        //    var tp = typeof(TStruct);
        //    var fieldIndex = 0;
        //    var fieldOffset = 0;
        //    Brush GetBrush() => fieldIndex % 2 == 0 ? BrushBlockFactory.FirstBrush : BrushBlockFactory.SecondBrush;
        //    foreach (var field in tp.GetFields()) {
        //        //若为字节数组,则访问MarshalAsAttribute,获取大小;
        //        if (field.FieldType == typeof(byte[])) {
        //            var attr = Attribute.GetCustomAttribute(field, typeof(MarshalAsAttribute)) as MarshalAsAttribute;
        //            if (attr == null) {
        //                LoggerService.WriteCallerLine($"{nameof(attr)} can't be null.");
        //                continue;
        //            }

        //            var block = BrushBlockFactory.CreateNewBackgroundBlock(offset + fieldOffset, attr.SizeConst, GetBrush());
        //            hexDataContext.CustomBackgroundBlocks.Add(block);
        //            fieldOffset += attr.SizeConst;
        //        }
        //        //若为结构体(值类型)，则尝试使用Marshal.SizeOf获取大小;
        //        else {
        //            try {
        //                var fieldSize = Marshal.SizeOf(field.FieldType);
        //                var block = BrushBlockFactory.CreateNewBackgroundBlock(offset + fieldOffset, fieldSize, GetBrush());
        //                hexDataContext.CustomBackgroundBlocks.Add(block);
        //                fieldOffset += fieldSize;
        //            }
        //            catch (Exception ex) {
        //                LoggerService.WriteCallerLine(ex.Message);
        //            }
        //        }

        //        fieldIndex++;
        //    }
        //}

    }
}
