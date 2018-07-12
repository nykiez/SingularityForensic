using System.Security.Cryptography;

namespace SingularityForensic.Hash {
    /// <summary>
    /// 哈希算法提供器(此单位旨在封装系统提供的哈希算法);
    /// </summary>
    public interface IHashAlgorithmProvider {
        HashAlgorithm GetOrCreateNewOne();
    }
}
