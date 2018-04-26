using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SingularityForensic.FileSystem {
    /// <summary>
    /// 子文件集;
    /// </summary>
    public class FileBaseCollection : IFileCollection {
        /// <summary>
        /// 文件子集的构造方法;
        /// </summary>
        /// <param name="parent">父文件</param>
        public FileBaseCollection(IFile parent) {
            if (parent == null) {
                throw new ArgumentNullException(nameof(parent));
            }

            this._parent = parent;
        }

        private IFile _parent;
        //核心文件集合;
        private List<IFile> _children = new List<IFile>();

        public IEnumerator<IFile> GetEnumerator() => _children.GetEnumerator();

        public bool Contains(IFile file) => _children.Contains(file);

        public void Add(IFile file) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (file == _parent) {
                throw new InvalidOperationException($"The file can't own itself as a child.");
            }

            if (_children.Contains(file)) {
                throw new InvalidOperationException($"The file has already been added to the collection.");
            }

            if (file is FileBase fileBase) {
                fileBase.InternalParent = _parent;
            }

            _children.Add(file);
        }

        public bool Remove(IFile file) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (!_children.Contains(file)) {
                throw new InvalidOperationException($"The {nameof(file)} is not contained in the collection.");
            }

            _children.Remove(file);

            if (file is FileBase fileBase) {
                fileBase.InternalParent = null;
            }

            return true;
        }

        

        public void Clear() {
            throw new NotImplementedException();
        }

        public void CopyTo(IFile[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => _children.GetEnumerator();

        public int Count => _children.Count;

        public bool IsReadOnly => false;
    }
}
