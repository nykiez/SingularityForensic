using SingularityForensic.Contracts.ToolBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.ToolBar {
    public class ToolBarObjectItem : IToolBarObjectItem {
        public ToolBarObjectItem(object uiObject,string guid) {
            this.GUID = guid;
            this.UIObject = uiObject;
        }

        public string GUID { get; }
        public object UIObject { get ; }
        public int Sort { get; set; }
    }


}
