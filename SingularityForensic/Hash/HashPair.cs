using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash {
    class HashPair : IHashPair {
        public HashPair(string name,string value) {
            if (value == null) {
                throw new ArgumentNullException($"{nameof(value)}");
            }
            
            if(value == string.Empty) {
                throw new ArgumentException($"{nameof(value)} can't be empty");
            }

            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value;
        }
        public string Value { get; }

        public string Name { get; }
    }

    [Export(typeof(IHashPairFactory))]
    class HashPairFactoryImpl : IHashPairFactory {
        public IHashPair CreateHashPair(string name, string value) => new HashPair(name, value);

        public IHashPair CreateHashPair(string value) => new HashPair(string.Empty, value);
    }
}
