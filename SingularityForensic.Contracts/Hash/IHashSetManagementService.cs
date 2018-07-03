using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    /// <summary>
    /// 哈希集管理服务;
    /// </summary>
    public interface IHashSetManagementService {
        /// <summary>
        /// 所有哈希集;
        /// </summary>
        IEnumerable<IHashSet> HashSets { get; }

        /// <summary>
        /// 添加哈希集;
        /// </summary>
        /// <param name="hashSet"></param>
        void AddHashSet(IHashSet hashSet);

        /// <summary>
        /// 移除哈希集;
        /// </summary>
        /// <param name="hashSet"></param>
        void RemoveHashSet(IHashSet hashSet);

        /// <summary>
        /// 清除菜单;
        /// </summary>
        void ClearHashSets();

        /// <summary>
        /// 初始化;
        /// </summary>
        void Initialize();
    }

    public class HashSetManagementService : GenericServiceStaticInstance<IHashSetManagementService> {
        public static void AddHashSet(IHashSet hashSet) => Current?.AddHashSet(hashSet);
        public static void Initialize() => Current?.Initialize();
        public static void RemoveHashSet(IHashSet hashSet) => Current?.RemoveHashSet(hashSet);
        public static IEnumerable<IHashSet> HashSets => Current?.HashSets;
    }
}
