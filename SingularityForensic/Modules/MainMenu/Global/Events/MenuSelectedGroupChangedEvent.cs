using Prism.Events;
using Singularity.Contracts.Contracts.MainMenu;

namespace SingularityForensic.Modules.MainMenu.Global.Events {
    /// <summary>
    /// 菜单选择项变化事件(通知);
    /// </summary>
    public class MenuSelectedGroupChangedEvent : PubSubEvent<MenuItemGroup> {

    }
}
