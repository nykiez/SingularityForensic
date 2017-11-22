using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.MessageBoxes.Models.Filtering {
    /// <summary>
    /// 过滤模型契约;
    /// </summary>
    public interface IFilterModel {
        //是否启用;
        bool IsEnabled { get; }
    }
}
