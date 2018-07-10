using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash {
    class UnitHashValueStatus : IUnitHashValueStatus {
        public UnitHashValueStatus(string name, string value) {
            if (value == null) {
                throw new ArgumentNullException($"{nameof(value)}");
            }

            if (value == string.Empty) {
                throw new ArgumentException($"{nameof(value)} can't be empty");
            }

            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value;
        }

        public string Value { get; }

        public string Name { get; }

        public string HasherGUID { get; internal set; }

        public string StatusType { get; internal set; }

    }
}
