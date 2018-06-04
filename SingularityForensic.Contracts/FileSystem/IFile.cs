using CDFC.Util.IO;
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
    
    public interface IFile : IInstanceReadOnlyExtensible {
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
            var firstBlock = blockGrouped.BlockGroups?.FirstOrDefault();
            if (firstBlock != null) {
                return firstBlock.Offset;
            }
            return null;
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
