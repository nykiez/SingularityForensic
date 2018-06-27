using SingularityForensic.Contracts.App;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// 具有子级的可迭代文件;
    /// </summary>
    public interface IHaveFileCollection : IFile {
        IFileCollection Children { get; }
    }

    /// <summary>
    /// 文件集;
    /// </summary>
    public interface IFileCollection : ICollection<IFile> {
    }

    
    
}
