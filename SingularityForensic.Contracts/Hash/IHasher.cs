using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    /// <summary>
    /// 哈希器契约;
    /// </summary>
    public interface IHasher {
        /// <summary>
        /// 哈希类型名;
        /// </summary>
        string HashTypeName { get; }
        /// <summary>
        /// GUID
        /// </summary>
        string GUID { get; }
        /// <summary>
        /// 计算哈希;
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        byte[] ComputeHash(Stream inputStream,IProgressReporter reporter);
        /// <summary>
        /// 排序;
        /// </summary>
        int Sort { get; }
    }
}
