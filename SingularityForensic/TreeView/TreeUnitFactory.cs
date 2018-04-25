using SingularityForensic.Contracts.TreeView;
using System.ComponentModel.Composition;

namespace SingularityForensic.TreeView {
    [Export(typeof(ITreeUnitFactory))]
    class TreeUnitFactory : ITreeUnitFactory {
        public ITreeUnit CreateNew(string typeGUID) => new TreeUnit(typeGUID);
    }
}
