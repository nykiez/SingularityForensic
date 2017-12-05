using CDFCUIContracts.Abstracts;
using System.Collections.ObjectModel;

namespace Singularity.Contracts.FileExplorer {
    public interface IHaveTabModels {
        ObservableCollection<ITabModel> TabViewModels { get; set; }
        ITabModel SelectedTabModel { get; set; }
    }
}
