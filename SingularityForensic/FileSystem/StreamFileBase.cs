using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;

namespace SingularityForensic.FileSystem {


    /// <summary>
    /// 流文件类型;可用作描述分区,磁盘等的基类;
    /// </summary>
    /// <typeparam name="TStoken"></typeparam>
    public abstract class StreamFileBase<TStoken> : FileBase<TStoken>,
        IStreamFile<TStoken> where TStoken : StreamFileStoken,new() {
        public StreamFileBase(string key):base(key){

        }
        
        public Stream BaseStream => _stoken?.BaseStream;

        public int BlockSize => _stoken?.BlockSize ?? 0;
        
        private bool _disposed = false;
        public virtual void Dispose() {
            if (_disposed) {
                return;
            }


            if (!_disposed) {
                try {
                    BaseStream?.Close();
                    _stoken.BaseStream = null;
                    _disposed = true;
                }
                catch(Exception ex) {
                    LoggerService.WriteException(ex);
                }

                try {
                    Disposing?.Invoke(this, EventArgs.Empty);
                }
                catch(Exception ex) {
                    LoggerService.WriteException(ex);
                }
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


