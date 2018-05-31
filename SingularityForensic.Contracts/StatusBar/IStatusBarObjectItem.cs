using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Contracts.StatusBar {
    /// <summary>
    /// 状态栏;
    /// </summary>
    public interface IStatusBarObjectItem:IUIObjectProvider {
        string GUID { get; }
    }
}
