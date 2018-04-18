using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.ToolBar {
    public interface IToolBarObject : IUIObjectProvider {

    }

    public class ToolBarObjectItem : IUIObjectProvider {
        public object UIObject { get; set; }
    }
}
