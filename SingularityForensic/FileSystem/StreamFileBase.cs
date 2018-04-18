using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {
    

    /// <summary>
    /// 流文件类型;可用作描述分区,磁盘等的基类;
    /// </summary>
    /// <typeparam name="TStoken"></typeparam>
    public abstract class StreamFileBase<TStoken> : FileBase<TStoken>,
        IStreamFile<TStoken> where TStoken : StreamFileStoken,new() {
        public StreamFileBase(string key,TStoken stoken = null):base(key,stoken){

        }
        
        public Stream BaseStream => _stoken?.BaseStream;

        public int BlockSize => _stoken?.BlockSize ?? 0;
        
        public override IEnumerable<string> TypeGuids {
            get {
                if (base.TypeGuids != null) {
                    foreach (var guid in base.TypeGuids) {
                        yield return guid;
                    }
                }
            }
        }

        private bool _disposed = false;
        public virtual void Dispose() {
            if (!_disposed) {
                Disposing?.Invoke(this, EventArgs.Empty);
                BaseStream?.Close();
                _stoken.BaseStream = null;
                _disposed = true;
            }
        }

        /// <summary>
        /// //正在析构事件;
        /// </summary>
        public event EventHandler Disposing; 

        private FileBaseCollection _children;
        public IFileCollection Children => _children ?? (_children = new FileBaseCollection(this));
    }
    
}


