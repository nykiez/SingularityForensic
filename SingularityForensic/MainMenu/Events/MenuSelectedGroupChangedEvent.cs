using Prism.Events;
using SingularityForensic.Contracts.MainMenu;

namespace SingularityForensic.MainMenu.Events {
    /// <summary>
    /// 菜单选择项变化事件(通知);
    /// </summary>
    public class MenuSelectedGroupChangedEvent : PubSubEvent<MenuItemGroup> {

    }
}
