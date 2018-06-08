using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// 是否已经被勾选,本对象将用于确认勾选;
    /// </summary>
    public interface ICheckable {
        bool IsChecked { get; set; }
        /// <summary>
        /// 获取GUID;(关于对象唯一);
        /// </summary>
        string GUID { get; }
    }
}
