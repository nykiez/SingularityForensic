using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    /// <summary>
    /// 哈希状态管理服务;
    /// 本服务的管理内容与案件(当前)相关;
    /// </summary>
    public interface IHashStatusManagementService {
        /// <summary>
        /// 开始编辑;
        /// </summary>
        void BeginEdit();
        /// <summary>
        /// 设定文件的哈希值,并进行存储;
        /// 调用本方法前，须先调用<see cref="BeginEdit"/>
        /// </summary>
        /// <param name="hashKey">哈希相关键值</param>
        /// <param name="hasherTypeGUID"></param>
        void SetFileHashValue(string name, string hashValue,string hasherGUID, string hashPairType);
        /// <summary>
        /// 终止编辑;
        /// </summary>
        void EndEdit();
        /// <summary>
        /// 查看开始;
        /// </summary>
        void BeginOpen();
        /// <summary>
        /// 列出所有哈希状态;
        /// </summary>
        /// <returns></returns>
        IEnumerable<IHashPair> GetAllFileHashPairs();
        /// <summary>
        /// 终止查看;
        /// </summary>
        void EndOpen();
        /// <summary>
        /// 清除;
        /// </summary>
        void DeleteAll();
    }

    public class HashStatusManagementService : GenericServiceStaticInstance<IHashStatusManagementService> {
        public static void BeginEdit() => Current.BeginEdit();
        public static void SetFileHashValue(string name, string hashValue, string hasherGUID, string hashPairType) => Current.SetFileHashValue(name, hashValue, hasherGUID, hashPairType);
        public static void EndEdit() => Current.EndEdit();
        public static void BeginOpen() => Current.BeginOpen();
        public static IEnumerable<IHashPair> GetAllFileHashPairs() => Current.GetAllFileHashPairs();
        public static void EndOpen() => Current.EndOpen();
        public static void DeleteAll() => Current.DeleteAll();
    }
}
