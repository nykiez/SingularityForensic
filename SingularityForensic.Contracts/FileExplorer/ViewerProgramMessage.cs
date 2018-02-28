using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    /// <summary>
    /// 查看程序讯息;
    /// </summary>
    public class ViewerProgramMessage {
        public ViewerProgramMessage(string vPath, string fPath) {
            this.ViewerPath = vPath;
            this.FilePath = fPath;
        }
        public ViewerProgramMessage(string vPath, string fileName, Stream stream) {
            this.ViewerPath = vPath;
            this.FileName = fileName;
            this.FStream = stream;
        }

        public string ViewerPath { get; }
        public string FilePath { get; }
        public string FileName { get; }
        public Stream FStream { get; }
    }
}
