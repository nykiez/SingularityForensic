using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash {
    [Export(typeof(IHashSetManagementService))]
    class HashSetManagementServiceImpl : IHashSetManagementService {
        private List<IHashSet> _hashSets = new List<IHashSet>();
        public IEnumerable<IHashSet> HashSets => _hashSets.Select(p => p);

        public void AddHashSet(IHashSet hashSet) {
            throw new NotImplementedException();
        }

        public void Initialize() {
            throw new NotImplementedException();
        }

        public void RemoveHashSet(IHashSet hashSet) {
            throw new NotImplementedException();
        }
    }
}
