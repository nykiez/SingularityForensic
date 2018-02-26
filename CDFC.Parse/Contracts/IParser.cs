using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFC.Parse.Contracts {
    public interface IParser {
        string GUID { get; }
        bool CanParse(Stream stream);
        IFile Parse(Stream stream);
    }
}
