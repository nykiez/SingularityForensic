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
        IFileCollection Children { get; }
    }

    public interface IFileCollection : ICollection<IFile> {
        int Count { get; }
    }
    
}
