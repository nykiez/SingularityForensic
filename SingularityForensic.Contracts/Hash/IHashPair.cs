using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    public interface IHashPair {
        /// <summary>
        /// 哈希值(十六进制表示);
        /// </summary>
        string Value { get; }

        /// <summary>
        /// 名;
        /// </summary>
        string Name { get; }
    }

    public interface IHashPairFactory {
        IHashPair CreateHashPair(string name, string value);
    }

    public class HashPairFactory : GenericServiceStaticInstance<IHashPairFactory> {
        public static IHashPair CreateHashPair(string name, string value) => Current?.CreateHashPair(name, value);

    }
}
