using System;
using System.Collections.ObjectModel;

namespace SingularityForensic.Modules.MainMenu.Models {
    public class MenuItemGroup {
        public MenuItemGroup(int sortOrder = 32) {
            this.SortOrder = sortOrder;
        }
        public ObservableCollection<MenuButtonItemModel> Children {
            get; set; } = new ObservableCollection<MenuButtonItemModel>();
        public string Text { get; set; }
        public Uri IconSource { get; set; }
        public int SortOrder { get; }
    }
}
