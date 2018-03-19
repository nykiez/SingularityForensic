using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public class SymbolLink : FileBase {
        public SymbolLink(FileBase parent) {
            this.Parent = parent;
        }

        public override string Name { get; }

        public FileBase Parent { get; }

        public override long Size { get; }

        public override IEnumerable<string> TypeGuids => throw new NotImplementedException();
    }
}
