using SingularityForensic.Contracts.ToolBar;
using Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Local {
    public static class ToolBarItemDefinitions {
        private static IToolBarButtonItem _addILocalDirToolBarItem;
        [Export]
        public static IToolBarButtonItem AddILocalDirToolBarItem {
            get {
                if (_addILocalDirToolBarItem == null) {
                    _addILocalDirToolBarItem = ToolBarItemFactory.CreateToolBarButtonItem(
                        new DelegateCommand(() => {
                            ServiceProvider.Current.GetInstance<LocalFileService>()?.AddLocalDir();
                        }), Constants.TBButtonGUID_AddLocalDir);
                    _addILocalDirToolBarItem.Icon = IconResources.AddLocalDirIcon;
                    _addILocalDirToolBarItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_AddLocalDir);
                    _addILocalDirToolBarItem.Sort = 16;
                }
                return _addILocalDirToolBarItem;
            }
        }
    }
}
