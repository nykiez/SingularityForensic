using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// 具有GUID的对象,这将用于状态保存;
    /// </summary>
    public interface IHaveGUIDObject {
        /// <summary>
        /// 获取GUID;(关于对象唯一);
        /// </summary>
        string GUID { get; }
    }
}
