using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    /// <summary>
    /// 哈希名称-值匹配，该类别为哈希集的线性单元;
    /// </summary>
    public interface IHashPair {
        /// <summary>
        /// 哈希值(十六进制表示);
        /// </summary>
        string Value { get; }

        /// <summary>
        /// 名称;
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 哈希类型;
        /// </summary>
        string HasherGUID { get; }

        /// <summary>
        /// 单元类型;用于区分文件的哈希值,哈希集的哈希值等;
        /// </summary>
        string PairType { get; }
    }

    //public interface IHashPairFactory {
    //    IHashPair CreateHashPair(string name, string value);
    //}

    //public class HashPairFactory : GenericServiceStaticInstance<IHashPairFactory> {
    //    public static IHashPair CreateHashPair(string name, string value) => Current?.CreateHashPair(name, value);

    //}
}
