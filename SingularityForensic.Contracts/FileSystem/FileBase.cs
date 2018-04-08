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
    public abstract class FileStokenBase : SecurityStoken, IHaveExtendTime {
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

        public IList<BlockGroup> BlockGroups { get; } = new List<BlockGroup>();

        //是否被删除;
        public bool? Deleted { get; set; }                      //是否被删除;
    }

    /// <summary>
    /// 表示具有删除状态的文件;
    /// </summary>
    public interface IDeletable {
        bool? Deleted { get; }
    }

    /// <summary>
    /// 文件基类;
    /// </summary>
    [Serializable]
    public abstract class FileBase {

        public abstract IEnumerable<string> TypeGuids { get; }

        public FileBase Parent => InternalParent;

        public abstract string Name { get; }

        public abstract long Size { get; }

        internal FileBase InternalParent { get; set; }

        internal void ChangeParent(FileBase parent) {
            InternalParent = parent;
        }


    }


    /// <summary>
    /// 文件内部信息基类;
    /// </summary>
    /// <typeparam name="TStoken"></typeparam>
    [Serializable]
    public abstract class FileBase<TStoken> :
        FileBase, IHaveStoken<TStoken>
        where TStoken : FileStokenBase, new() {

        private string _key;
        protected TStoken _stoken;

        public TStoken GetStoken(string key) {
            if (key != _key) {
                throw new AuthenticationException($"{nameof(key)} is not matched.");
            }
            return _stoken;
        }

        public FileBase(string key, TStoken stoken = null) {
            this._key = key;
            this._stoken = stoken ?? new TStoken();
        }

        public override IEnumerable<string> TypeGuids => _stoken?.TypeGuids;

        //public FileBase Parent => _stoken?.Parent;

        public override string Name => _stoken?.Name;

        public override long Size => _stoken?.Size ?? throw new InvalidOperationException($"{nameof(_stoken)} can't be null");

        /// <summary>
        /// 拓展时间获取;
        /// </summary>
        /// <param name="timeLabel"></param>
        /// <returns></returns>
        public DateTime? GetExtensionTime(string timeLabel) => _stoken.GetExtensionTime(timeLabel);
    }

    /// <summary>
    /// 用于描述文件,文件夹等具有时间,块组特性的文件;
    /// </summary>
    /// <typeparam name="TStoken"></typeparam>
    [Serializable]
    public abstract class BlockGroupedFileBase<TStoken> : FileBase<TStoken>,
        IHaveFileTime, IBlockGroupedFile ,IDeletable where TStoken : FileStokenBase2, new() {
        public BlockGroupedFileBase(string key, TStoken stoken = null) : base(key, stoken) {

        }
        
        public DateTime? ModifiedTime => _stoken?.ModifiedTime;

        public DateTime? AccessedTime => _stoken?.AccessedTime;

        public DateTime? CreateTime => _stoken?.CreateTime;

        public IEnumerable<BlockGroup> BlockGroups => _stoken?.BlockGroups?.Select(p => p);

        public bool? Deleted => _stoken?.Deleted;

        public DateTime? GetExtensionTime(string timeLabel) => _stoken?.GetExtensionTime(timeLabel);
        
        
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
            if(!(blockGrouped is FileBase file)) {
                return null;
            }

            
            var blockedFile = file.GetParent<IBlockedStream>();
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
    public class RegularFileStoken : FileStokenBase2  {

    }

    /// <summary>
    /// 文件夹内部信息;
    /// </summary>
    [Serializable]
    public class DirectoryStoken : FileStokenBase2  {
        /// <summary>
        /// 是否为上级目录;
        /// </summary>
        public bool IsBack { get; set; }

        /// <summary>
        /// 是否为本目录;
        /// </summary>
        public bool IsLocalBackUp { get; set; }
    }
    
    /// <summary>
    /// 正常文件入口
    /// </summary>
    [Serializable]
    public class RegularFile : BlockGroupedFileBase<RegularFileStoken>  {
        /// <summary>
        /// 常规文件构造方法;
        /// </summary>
        /// <param name="parent"></param>
        public RegularFile(string key, RegularFileStoken stoken = null) :base(key,stoken) {
            
        }
    }

    [Serializable]
    public class Directory : BlockGroupedFileBase<DirectoryStoken>, IHaveFileCollection {
        public Directory(string key, DirectoryStoken stoken = null) : base(key, stoken) {
            Children = new FileBaseCollection(this);
        }

        //public IEnumerable<FileBase> Children => _stoken?.Children?.Select(p => p);

        
        public FileBaseCollection Children { get; }

        public bool IsBack => _stoken.IsBack;

        public bool IsLocalBackUp => _stoken.IsLocalBackUp;
    }
}
