using System;
using System.Windows.Input;

namespace SingularityForensic.Modules.MainMenu.Models {
    
    //上下文菜单模型;
    public class MenuButtonItemModel {
        public MenuButtonItemModel(MenuItemGroup group, string text, int sortOrder = 32) {
            this.Text = text;
            this.Group = group;
            this.SortOrder = sortOrder;
        }

        public string Text { get;  }
        public MenuItemGroup Group { get; }
        public int SortOrder { get; }
        public object ToolTip { get; set; }
        public Uri IconSource { get; set; }
        public string InputGestureText { get; set; }
        public ICommand Command { get; set; }
        public bool IsChecked { get; set; }
        public bool IsVisible { get; set; }
        public Key Key { get; set; }
        public ModifierKeys Modifier { get; set; }
        public object CommandParameter { get; set; }
    }
}
