using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {
    /// <summary>
    /// 快捷方式;
    /// </summary>
    public class SymbolLink : ISymbolLink {
        public SymbolLink(IFile parent) {
            this.Parent = parent;
        }

        public string Name { get; }

        public IFile Parent { get; }

        public long Size { get; }

        public IEnumerable<string> TypeGuids => throw new NotImplementedException();
    }
}
