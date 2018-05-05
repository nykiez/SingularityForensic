using SingularityForensic.Contracts.ToolBar;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace SingularityForensic.ToolBar {
    [Export(typeof(IToolBarItemFactory))]
    public class ToolBarFactoryImpl : IToolBarItemFactory {
        public IToolBarButtonItem CreateToolBarButtonItem(ICommand command, string guid) {
            return new ToolBarButtonItem(command, guid);
        }

        public IToolBarObjectItem CreateToolBarObjectItem(object uiObject, string guid) => new ToolBarObjectItem(uiObject, guid);
    }
}
