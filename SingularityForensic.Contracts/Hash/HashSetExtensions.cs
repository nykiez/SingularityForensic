using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    public static class HashSetExtensions {
        /// <summary>
        /// 检查某个哈希集中是否包含某个哈希值;在调用本方法前,须先调用<see cref="IHashSet.BeginEdit"/>,并在完成并且必要时调用<see cref="IHashSet.EndEdit"/>
        /// </summary>
        /// <param name="hashSet"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ContainsValue(this IHashSet hashSet,string value) {
            if(hashSet == null) {
                throw new ArgumentNullException(nameof(hashSet));
            }

            var pairs = hashSet.FindHashPairs(value);
            return pairs.FirstOrDefault() != null;
        }
    }
}
