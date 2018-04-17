using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// 可拓展契约;
    /// </summary>
    public interface IExtensible {
        /// <summary>
        /// 获得拓展实例;
        /// </summary>
        /// <typeparam name="TInstance">拓展实例类型</typeparam>
        /// <returns></returns>
        TInstance GetIntance<TInstance>(string extName);
        /// <summary>
        /// 设定拓展实例;
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="instance"></param>
        void SetInstance<TInstance>(TInstance instance,string extName);
    }
}
