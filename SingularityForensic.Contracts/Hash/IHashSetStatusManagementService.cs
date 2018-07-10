using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    /// <summary>
    /// 哈希集(比如文件的哈希集归属)状态管理服务;
    /// 本服务的管理内容与案件(当前)相关;
    /// </summary>
    public interface IHashSetStatusManagementService {
        /// <summary>
        /// 开始编辑;
        /// </summary>
        void BeginEdit();
        /// <summary>
        /// 设定某个单位的哈希集集合,并进行存储;
        /// 调用本方法前，须先调用<see cref="BeginEdit"/>
        /// </summary>
        /// <param name="unitName">单位名称,比如,对于文件的哈希值标识,此处则为存储文件相对文件系统的路径;</param>
        /// <param name="hashSetGuids">哈希集集合的GUID</param>
        /// <param name="hashSetStatusType">哈希值类型标识(比如属于文件的哈希集)</param>
        void SetUnitHashSetStatus(string unitName,string[] hashSetGuids, string hashSetStatusType);
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
        IEnumerable<IUnitHashSetStatus> GetAllHashSetStatus();
        /// <summary>
        /// 终止查看;
        /// </summary>
        void EndOpen();
        /// <summary>
        /// 清除;
        /// </summary>
        void DeleteAll();
    }

    public class HashSetStatusManagementService:GenericServiceStaticInstance<IHashSetStatusManagementService> {
        public static void BeginEdit() => Current.BeginEdit();
        public static void SetUnitHashSetStatus(string unitName,  string[] hashSetGuids, string hashSetStatusType) => Current.SetUnitHashSetStatus(unitName, hashSetGuids, hashSetStatusType);
        public static void EndEdit() => Current.EndEdit();
        public static void BeginOpen() => Current.BeginOpen();
        public static IEnumerable<IUnitHashSetStatus> GetAllHashSetStatus() => Current.GetAllHashSetStatus();
        public static void EndOpen() => Current.EndOpen();
        public static void DeleteAll() => Current.DeleteAll();
    }
}
