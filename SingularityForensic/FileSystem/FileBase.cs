using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {
    /// <summary>
    /// 文件基类;
    /// </summary>
    [Serializable]
    public abstract class FileBase : IFile {
        public abstract IEnumerable<string> TypeGuids { get; }

        public IFile Parent => InternalParent;

        public abstract string Name { get; }

        public abstract long Size { get; }

        internal IFile InternalParent { get; set; }

        internal void ChangeParent(IFile parent) {
            InternalParent = parent;
        }
    }

    /// <summary>
    /// 文件内部信息基类;
    /// </summary>
    /// <typeparam name="TStoken"></typeparam>
    [Serializable]
    public abstract class FileBase<TStoken> :
        FileBase, IFile<TStoken>
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
        IHaveFileTime, IBlockGroupedFile, IDeletable where TStoken : FileStokenBase2, new() {
        public BlockGroupedFileBase(string key, TStoken stoken = null) : base(key, stoken) {

        }

        public DateTime? ModifiedTime => _stoken?.ModifiedTime;

        public DateTime? AccessedTime => _stoken?.AccessedTime;

        public DateTime? CreateTime => _stoken?.CreateTime;

        public IEnumerable<BlockGroup> BlockGroups => _stoken?.BlockGroups?.Select(p => p);

        public bool? Deleted => _stoken?.Deleted;

    }
}
