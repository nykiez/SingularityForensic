using SingularityForensic.Contracts.ToolBar;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace SingularityForensic.ToolBar.ViewModels {
    [Export]
    public class ToolBarViewModel
    {
        public ObservableCollection<IToolBarObjectItem> ToolBarItems { get; set; } = new ObservableCollection<IToolBarObjectItem>();  
    }
}
