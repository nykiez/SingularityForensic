using CDFCUIContracts.Abstracts;
using System.Collections.ObjectModel;

namespace Singularity.UI.FileSystem.Models {
    public interface IHaveTabModels {
        ObservableCollection<ITabModel> TabViewModels { get; set; }
        ITabModel SelectedTabModel { get; set; }
    }
}
