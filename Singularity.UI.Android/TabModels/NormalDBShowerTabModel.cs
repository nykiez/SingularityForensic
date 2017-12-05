using Singularity.Contracts.TabControl;
using Singularity.UI.Info.Android.ViewModels;
using Singularity.UI.Info.Android.Views;

namespace Singularity.UI.Info.Android.TabModels {
    public class NormalDBShowerTabModel : TabModel {
        public NormalDBShowerTabModel(NormalDBShowerViewModel vm, string tabName) {
            this.Title = tabName;
            this.Content = new NormalDbShower {
                DataContext = vm
            };
            this.NormalDBShowerViewModel = vm;
        }
        
        public NormalDBShowerViewModel NormalDBShowerViewModel { get; }
    }
}
