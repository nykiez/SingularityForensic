using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public interface IStreamFile : IFile, IDisposable {
        Stream BaseStream { get; }
        int BlockSize { get; }
    }
    public interface IStreamFile<TStoken> :IFile<TStoken>,IStreamFile, IDisposable,
         IHaveFileCollection where TStoken : StreamFileStoken, new() {
        event EventHandler Disposing;
    }
}
