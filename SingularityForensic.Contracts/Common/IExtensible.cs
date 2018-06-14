using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
   

    /// <summary>
    /// 可拓展契约;
    /// </summary>
    public interface IExtensible : IReadOnlyExtensible {
        /// <summary>
        /// 设定拓展实例;
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="instance"></param>
        void SetInstance<TInstance>(TInstance instance,string extName);
    }

    /// <summary>
    /// 只读拓展契约;
    /// </summary>
    public interface IReadOnlyExtensible {
        /// <summary>
        /// 获得拓展实例;
        /// </summary>
        /// <typeparam name="TInstance">拓展实例类型</typeparam>
        /// <returns></returns>
        TInstance GetInstance<TInstance>(string extName);

        ///// <summary>
        ///// 类型判断拆箱获取类型;
        ///// </summary>
        ///// <typeparam name="TInstance"></typeparam>
        ///// <param name="extName"></param>
        ///// <returns></returns>
        //TInstance GetGeneralInstance<TInstance>(string extName);
    }

    public interface ITextExtensible:ITextReadOnlyExtensible {
        /// <summary>
        /// 拓展元素;
        /// </summary>
        string this[string extendElemName] { get; set; }

        /// <summary>
        /// 拓展元素属性;
        /// </summary>
        /// <param name="extendElemName"></param>
        /// <param name=""></param>
        /// <returns></returns>
        string this[string extendElemName, string extendAttriName] { get;  set; }
    }

    public interface ITextReadOnlyExtensible {
        /// <summary>
        /// 拓展元素属性;
        /// </summary>
        /// <param name="extendElemName"></param>
        /// <param name=""></param>
        /// <returns></returns>
        string this[string extendElemName, string extendAttriName] { get; }
        /// <summary>
        /// 拓展元素;
        /// </summary>
        string this[string extendElemName] { get;  }
    }

    public interface ITextInstanceExtensible : ITextExtensible, IExtensible {
        
    }
}
