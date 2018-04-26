using SingularityForensic.Contracts.FileSystem;
using System;

namespace SingularityForensic.FileSystem {

    [Serializable]
    public class Directory : BlockGroupedFileBase<DirectoryStoken>, IDirectory {
        public Directory(string key) : base(key) {
            Children = new FileBaseCollection(this);
        }

        //public IEnumerable<FileBase> Children => _stoken?.Children?.Select(p => p);


        public IFileCollection Children { get; }

        public bool IsBack => _stoken.IsBack;

        public bool IsLocalBackUp => _stoken.IsLocalBackUp;
    }
}
