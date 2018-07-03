using System.Security.Cryptography;

namespace SingularityForensic.Hash {
    /// <summary>
    /// 哈希算法提供器(此单位是针对系统提供的哈希算法而设立);
    /// </summary>
    public interface IHashAlgorithmProvider {
        HashAlgorithm GetOrCreateNewOne();
    }
}
