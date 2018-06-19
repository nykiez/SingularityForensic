using SingularityForensic.Contracts.StatusBar;

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
