﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    ////块设备流(可描述分区/硬盘等文件);
    //public abstract class BlockedStreamFile<TStoken> : FileBase<TStoken>,IEnumerableFile ,IDisposable where TStoken:BlockedStreamFileStoken,new(){
    //    public BlockedStreamFile(string key, TStoken stoken = null) : base(key, stoken) {

    //    }
    //    Stream BaseStream { get; }
    //    int BlockSize { get; }

    //    public virtual void Dispose() {
    //        BaseStream.Close();
    //    }

        
    //}

    public class BlockedStreamFileStoken : FileStokenBase  {
        public Stream BaseStream { get; set; }

        public int BlockSize { get; set; }    
    }

    public interface IBlockedStream {
        Stream BaseStream { get; }
        int BlockSize { get; }
    }

    //块-流文件类型;可用作描述分区,磁盘等的基类;
    public abstract class BlockedStreamFileBase<TStoken> :
        FileBase<TStoken>,IDisposable,
        IHaveFileCollection where TStoken:BlockedStreamFileStoken , new(){
        public BlockedStreamFileBase(string key,TStoken stoken = null):base(key,stoken){

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

        //正在析构事件;
        public event EventHandler Disposing; 

        private FileBaseCollection _children;
        public FileBaseCollection Children => _children ?? (_children = new FileBaseCollection(this));
    }
    
}

