using SingularityForensic.Contracts.StatusBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.StatusBar {
    class StatusBarObjectItem : IStatusBarObjectItem {
        public StatusBarObjectItem(string guid,object content) {
            this.GUID = guid;
            this.UIObject = content;
        }

        public string GUID { get;  }

        public object UIObject { get; }
        
    }
}
