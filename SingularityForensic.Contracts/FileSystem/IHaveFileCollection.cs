using SingularityForensic.Contracts.App;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    //具有子级的可迭代文件;
    public interface IHaveFileCollection  {
        FileBaseCollection Children { get; }
    }

    //子文件集;
    public class FileBaseCollection : IEnumerable<FileBase> {
        /// <summary>
        /// 文件子集的构造方法;
        /// </summary>
        /// <param name="parent">父文件</param>
        public FileBaseCollection(FileBase parent) {
            if(parent == null) {
                throw new ArgumentNullException(nameof(parent));
            }

            this._parent = parent;
        }

        private FileBase _parent;
        //核心文件集合;
        private List<FileBase> _children = new List<FileBase>();

        public IEnumerator<FileBase> GetEnumerator() => _children.GetEnumerator();

        public bool Contains(FileBase file) => _children.Contains(file);

        public void Add(FileBase file) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (file == _parent) {
                throw new InvalidOperationException($"The file can't own itself as a child.");
            }

            if (_children.Contains(file)) {
                throw new InvalidOperationException($"The file has already been added to the collection.");
            }

            file.InternalParent = _parent;
            _children.Add(file);
        }

        public void Remove(FileBase file) {
            if(file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (!_children.Contains(file)) {
                throw new InvalidOperationException($"The {nameof(file)} is not contained in the collection.");
            }
            
            _children.Remove(file);
            
            file.InternalParent = null;
        }
        IEnumerator IEnumerable.GetEnumerator() => _children.GetEnumerator();

        public int Count => _children.Count;
    }

    
}
