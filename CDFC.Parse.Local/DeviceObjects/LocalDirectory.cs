using System;
using System.Collections.Generic;
using CDFC.Parse.Contracts;
using System.IO;

namespace CDFC.Parse.Local.DeviceObjects {
    /// <summary>
    /// 本地目录实体;
    /// </summary>
    public class LocalDirectory : Abstracts.Directory {
        public LocalDirectory(DirectoryInfo di,IFile parent):base(parent) {
            if(di == null) {
                throw new ArgumentNullException(nameof(di));
            }

            this.DirectoryInfo = di;
            
        }

        public DirectoryInfo DirectoryInfo { get; }

        public override DateTime? AccessedTime => null;
        
        private List<IFile> _children;
        public override List<IFile> Children 
            {
            get {
                if(_children == null) {
                    _children = new List<IFile>();
                    foreach (var item in DirectoryInfo.GetDirectories()) {
                        _children.Add(new LocalDirectory(item, this));
                    }
                    foreach (var file in DirectoryInfo.GetFiles()) {
                        _children.Add(new LocalRegFile(file, this));
                    }
                }
                return _children;
            }
        }

        public override DateTime? CreateTime => null;

        public override bool? Deleted => false;

        public override DateTime? ModifiedTime => null;

        public override string Name => DirectoryInfo.Name;

        public override long Size => 0;

        public override long StartLBA => 0;
    }
}
