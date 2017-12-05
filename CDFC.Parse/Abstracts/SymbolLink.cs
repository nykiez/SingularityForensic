using CDFC.Parse.Contracts;

namespace CDFC.Parse.Abstracts {
    public abstract class SymbolLink : IFile {
        public SymbolLink(IFile parent) {
            this.Parent = parent;
        }
        public FileType Type => FileType.SymbolicLink;

        public abstract string Name { get; }

        public IFile Parent { get; }

        public abstract long Size { get; }
    }
}
