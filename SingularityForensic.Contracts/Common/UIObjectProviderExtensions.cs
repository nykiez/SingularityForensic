using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// UI提供器拓展;
    /// </summary>
    public static class UIObjectProviderExtensions {
        /// <summary>
        /// 添加热键绑定;
        /// </summary>
        /// <param name="uiObjectProvider"></param>
        /// <param name="command"></param>
        /// <param name="key"></param>
        /// <param name="modifier"></param>
        public static void AddKeyBinding(this IUIObjectProvider uiObjectProvider, ICommand command, Key key, ModifierKeys modifier = ModifierKeys.None) {
            if(uiObjectProvider.UIObject is UIElement uiElement) {
                uiElement.InputBindings.Add(new KeyBinding(command,key,modifier));
            }
        }
    }
}
