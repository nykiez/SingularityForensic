﻿using CDFC.Util.IO;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// 用于某块内部使用的文件信息(Tag可自定义,外部需要根据Key拿到所需数据);
    /// </summary>
    [Serializable]
    public abstract class FileStokenBase : ExtensibleObject, IHaveExtendTime {
        public IEnumerable<string> TypeGuids { get; set; }          //文件类型;
        public string Name { get; set; }                //文件名;
        public long Size { get; set; }                  //文件大小;

        public Dictionary<string, DateTime> ExtendedTimes { get; } = new Dictionary<string, DateTime>();

        /// <summary>
        /// 拓展时间获取;
        /// </summary>
        /// <param name="timeLabel"></param>
        /// <returns></returns>
        public DateTime? GetExtensionTime(string timeLabel) {
            if (!ExtendedTimes.ContainsKey(timeLabel)) {
                return null;
            }

            return ExtendedTimes[timeLabel];
        }
    }

    /// <summary>
    /// 用于描述文件,文件夹等具有时间,块组特性的文件的信息;
    /// </summary>
    [Serializable]
    public abstract class FileStokenBase2 : FileStokenBase, IHaveFileTime, IDeletable {
        public DateTime? ModifiedTime { get; set; }

        public DateTime? AccessedTime { get; set; }

        public DateTime? CreateTime { get; set; }

        public ICollection<IBlockGroup> BlockGroups { get; } = new List<IBlockGroup>();

        //是否被删除;
        public bool? Deleted { get; set; }                      //是否被删除;
    }

    /// <summary>
    /// 表示具有删除状态的文件;
    /// </summary>
    public interface IDeletable {
        bool? Deleted { get; }
    }
    
    public interface IFile  {
        IEnumerable<string> TypeGuids { get; }

        IFile Parent { get; }

        string Name { get; }

        long Size { get; }
    }
    
    public interface IFile<TStoken> : IHaveStoken<TStoken>,IFile where TStoken : FileStokenBase, new() {
        DateTime? GetExtensionTime(string timeLabel);
    }
    
    /// <summary>
    /// 块组流文件拓展;
    /// </summary>
    public static class BlockGroupedFileBaseExtension{
        /// <summary>
        /// 获取起始LBA;
        /// </summary>
        /// <param name="blockGrouped"></param>
        /// <returns></returns>
        public static long? GetStartLBA(this IBlockGroupedFile blockGrouped) {
            var firstBlock = blockGrouped.BlockGroups.FirstOrDefault();
            if (firstBlock != null) {
                return firstBlock.Offset;
            }
            return null;
        }

        /// <summary>
        /// 获取输入流;
        /// </summary>
        /// <param name="blockGrouped"></param>
        /// <returns></returns>
        public static Stream GetInputStream(this IBlockGroupedFile blockGrouped) {
            if(!(blockGrouped is IFile file)) {
                return null;
            }

            
            var blockedFile = file.GetParent<IStreamFile>();
            if (blockedFile == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(blockedFile)} can't be null!");
                return null;
            }

            //检查块组是否为空;
            if(blockGrouped.BlockGroups == null) {
                LoggerService.WriteCallerLine($"{nameof(blockGrouped.BlockGroups)} can't be null.");
                return null;
            }

            //检查块组集合是否为空;
            if(blockGrouped.BlockGroups.FirstOrDefault() == null) {
                LoggerService.WriteCallerLine($"{nameof(blockGrouped.BlockGroups)} can't be empty.");
                return null;
            }

            var blockSize = blockedFile.BlockSize;
            var partStream = blockedFile.BaseStream;
            if(partStream == null) {
                LoggerService.WriteCallerLine($"{nameof(blockedFile.BaseStream)} can't be null.");
                return null;
            }

            //若块组不为空,则遍历块组组成虚拟流;
            var blockSum = blockGrouped.BlockGroups.Sum(p => p.BlockSize * p.Count);
            //核对和Size之间的差,最终将要切去最后一个簇的多余内容;
            var blockSub = blockSum - file.Size;
            //取所有块组的位置;
            var ranges = blockGrouped.BlockGroups.Select(p =>
                        ValueTuple.Create(p.Offset , p.Count * p.BlockSize)).
                        ToArray();

            //假如file.Size大于块组合,则忽视大小,直接取块组合;
            if (blockSub < 0) {
                return MulPeriodsStream.CreateFromStream(partStream, ranges);
            }
            //假如file.Size小于块组合;
            else {
                long blockSumSize = 0;
                int rangeIndex = 0;
                long lastRangeSize = 0;
                foreach (var range in ranges) {
                    if (blockSumSize + range.Item2 >= file.Size) {
                        lastRangeSize = file.Size - blockSumSize;
                        break;
                    }
                    blockSumSize += range.Item2;
                    rangeIndex++;
                }

                ranges[rangeIndex].Item2 = lastRangeSize;

                return MulPeriodsStream.CreateFromStream(partStream,ranges.Take(rangeIndex + 1).ToArray());
            }


            
        }
    }

    /// <summary>
    /// 常规文件内部信息;
    /// </summary>
    [Serializable]
    public class RegularFileStoken : FileStokenBase2 {

    }

    /// <summary>
    /// 文件夹内部信息;
    /// </summary>
    [Serializable]
    public class DirectoryStoken : FileStokenBase2 {
        /// <summary>
        /// 是否为上级目录;
        /// </summary>
        public bool IsBack { get; set; }

        /// <summary>
        /// 是否为本目录;
        /// </summary>
        public bool IsLocalBackUp { get; set; }
    }
    
    public interface IRegularFile : IFile<RegularFileStoken>, IHaveFileTime, IBlockGroupedFile, IDeletable {

    }

    public interface IDirectory : IFile<DirectoryStoken>, IHaveFileCollection {
        bool IsBack { get; }

        bool IsLocalBackUp { get; }
    }
}
