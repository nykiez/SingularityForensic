using System;
using System.Collections.ObjectModel;

namespace Singularity.Contracts.Contracts.MainMenu {
    public class MenuItemGroup {
        public MenuItemGroup(string guid, int sortOrder = 32) {
            this.SortOrder = sortOrder;
            this.GUID = guid;
        }
        public ObservableCollection<MenuButtonItemModel> Children {
            get; set; } = new ObservableCollection<MenuButtonItemModel>();
        public string Text { get; set; }
        public Uri IconSource { get; set; }
        public int SortOrder { get; }
        public string GUID { get; }
    }
}
