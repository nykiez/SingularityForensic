using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SingularityForensic.Contracts.MainMenu {
    /// <summary>
    /// 菜单项;
    /// </summary>
    public interface IMenu  {
        /// <summary>See <see cref="ExportMenuAttribute.Guid"/></summary>
        string GUID { get; }
        /// <summary>See <see cref="ExportMenuAttribute.Order"/></summary>
        double Order { get; }
        /// <summary>See <see cref="ExportMenuAttribute.Header"/></summary>
        string Header { get; }
    }
    
    
    
    public class MenuObjectItem : IUIObjectProvider {
        public object UIObject { get; set; }
        public string GUID { get; set; }
    }

    /// <summary>
    /// 上下文菜单模型;
    /// </summary>
    public class MenuButtonItem:MenuObjectItem {
        public MenuButtonItem(string groupId , string text, int sortOrder = 32) {
            this.Text = text;
            this.GroupID = groupId;
            this.SortOrder = sortOrder;
        }

        public string GroupID { get; }
        public string Text { get;  }
        public int SortOrder { get; }
        public object ToolTip { get; set; }
        public Uri IconSource { get; set; }
        public string InputGestureText { get; set; }
        public ICommand Command { get; set; }
        public bool IsChecked { get; set; }
        public bool IsVisible { get; set; }
        public Key Key { get; set; }
        public ModifierKeys Modifier { get; set; }
    }
}
