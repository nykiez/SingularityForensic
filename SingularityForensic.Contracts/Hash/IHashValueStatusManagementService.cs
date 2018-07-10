using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    /// <summary>
    /// 哈希值状态管理服务;
    /// 本服务的管理内容与案件(当前)相关;
    /// </summary>
    public interface IHashValueStatusManagementService {
        /// <summary>
        /// 开始编辑;
        /// </summary>
        void BeginEdit();
        /// <summary>
        /// 设定某个单位的哈希值,并进行存储;
        /// 调用本方法前，须先调用<see cref="BeginEdit"/>
        /// </summary>
        /// <param name="unitName">单位名称,比如对于文件的哈希值标识,此处则存储文件相对文件系统的路径</param>
        /// <param name="hasherGUID">哈希器类型标识</param>
        /// <param name="hashPairType">哈希值类型标识(比如属于文件的哈希值)</param>
        void SetUnitHashValueStatus(string unitName, string hashValue,string hasherGUID, string hashPairType);
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
        IEnumerable<IUnitHashValueStatus> GetAllHashValueStatus();
        /// <summary>
        /// 终止查看;
        /// </summary>
        void EndOpen();
        /// <summary>
        /// 清除;
        /// </summary>
        void DeleteAll();
    }
    
    public class HashStatusManagementService : GenericServiceStaticInstance<IHashValueStatusManagementService> {
        public static void BeginEdit() => Current.BeginEdit();
        public static void SetUnitHashValue(string unitName, string hashValue, string hasherGUID, string hashPairType) => Current.SetUnitHashValueStatus(unitName, hashValue, hasherGUID, hashPairType);
        public static void EndEdit() => Current.EndEdit();
        public static void BeginOpen() => Current.BeginOpen();
        public static IEnumerable<IUnitHashValueStatus> GetAllHashValueStatus() => Current.GetAllHashValueStatus();
        public static void EndOpen() => Current.EndOpen();
        public static void DeleteAll() => Current.DeleteAll();
    }
}
