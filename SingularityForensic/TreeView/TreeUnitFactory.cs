using SingularityForensic.Contracts.TreeView;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.TreeView {
    [Export(typeof(ITreeUnitFactory))]
    class TreeUnitFactory : ITreeUnitFactory {
        public ITreeUnit CreateNew(string typeGUID) => new TreeUnit(typeGUID);
    }
}
