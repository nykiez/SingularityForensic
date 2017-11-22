using System;
using System.Collections.Generic;

namespace CDFC.Parse.Signature.Contracts {
    public interface IFileSearcher:IDisposable {
        bool SearchStart(long startLBA,long byteCount);
        List<IFileNode> GetFileList(string extensionName);
        int CurFileCount { get; }
        bool Stop();
        long CurOffset { get; }

    }
}
