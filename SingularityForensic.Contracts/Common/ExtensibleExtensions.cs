using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public static class ExtensibleExtensions {
        /// <summary>
        /// 从可拓展实例中获取或创建实例;
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="ext"></param>
        /// <param name="extName"></param>
        /// <param name="factory">生产方法</param>
        /// <returns></returns>
        public static TInstance GetOrCreateInstance<TInstance>(this IExtensible ext, string extName, Func<TInstance> factory) 
            => GetOrCreateInstanceInform<TInstance>(ext, extName, factory).instance;

        /// <summary>
        /// 从可拓展实例中获取或创建实例;
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="ext"></param>
        /// <param name="extName"></param>
        /// <param name="factory">生产方法</param>
        /// <returns>isNew指示是否为新建的实例</returns>
        public static (TInstance instance,bool isNew) GetOrCreateInstanceInform<TInstance>(this IExtensible ext, string extName, Func<TInstance> factory) {
            if (ext == null) {
                throw new ArgumentNullException(nameof(ext));
            }
            var created = false;
            var ins = ext.GetInstance<TInstance>(extName);
            if (ins == null) {
                ins = factory();
                ext.SetInstance(ins, extName);
                created = true;
            }
            return (ins,created);
        }
    }
}
