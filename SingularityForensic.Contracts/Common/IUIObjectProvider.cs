using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    //UI元素提供者;
    public interface IUIObjectProvider {
        object UIObject { get; }
    }

    public interface IUIObjectProvider2:IUIObjectProvider {
        object Tag { get; set; }
    }
}
