using Microsoft.Practices.ServiceLocation;
using Prism.Mvvm;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Contracts.MainMenu;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.MainMenu.Global.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

namespace SingularityForensic.MainMenu.ViewModels {
    [Export]
    public class MainMenuViewModel:BindableBase {
        [ImportingConstructor]
        public MainMenuViewModel(
            [ImportMany]IEnumerable<MenuItemGroup> groups,
            [ImportMany]IEnumerable<MenuButtonItemModel> menuItems) {
            this.MenuGroups = new ObservableCollection<MenuItemGroup>(groups.OrderBy(g => g.SortOrder));
            this.menuItems = menuItems;
            
            BuildBasicMenus();
            AddGestures();
            //PubEventHelper.GetEvent<RequireMenuGroupChangeEvent>().Subscribe(group => {
            //    SelectedGroup = group;
            //});
            //PubEventHelper.GetEvent<MenuSelectedGroupChangedEvent>()?.Subscribe(group => {
            //    if (group != MenuGroupDefinitions.HelpMenuGroup && group != MenuGroupDefinitions.OptionsMenuGroup) {
            //        ServiceProvider.Current.GetInstance<IRegionManager>()?.RequestNavigate(RegionNames.MainRegion, "BlankPage");
            //    }
            //});
        }
        
        /// <summary>
        /// 建立基本菜单;
        /// </summary>
        private void BuildBasicMenus() {
            foreach (var group in MenuGroups) {
                foreach (var item in menuItems.Where(i => i.GroupID == group.GUID).OrderBy(i => i.SortOrder)) {
                    group.Children.Add(item);
                } 
            }
        }

        /// <summary>
        /// 添加快捷键绑定;
        /// </summary>
        private void AddGestures() {
            var shellService = ServiceProvider.Current.GetInstance<IShellService>();
            foreach (var item in menuItems) {
                if (item.Key != Key.None) {
                    shellService?.AddKeyGestrue(item.Command, item.Key, item.Modifier, item.CommandParameter);
                }
            }
        }

        private IEnumerable<MenuButtonItemModel> menuItems;
        public ObservableCollection<MenuItemGroup> MenuGroups { get; set; }

        private MenuItemGroup _selectedGroup;
        
        public MenuItemGroup SelectedGroup {
            get {
                return _selectedGroup;
            }
            set {
                SetProperty(ref _selectedGroup, value);
                //PubEventHelper.GetEvent<MenuSelectedGroupChangedEvent>().Publish(_selectedGroup);
            }
        }

        
    }
}
