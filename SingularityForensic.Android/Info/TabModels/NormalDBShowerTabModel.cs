using SingularityForensic.Contracts.TabControl;
using SingularityForensic.Android.Info.ViewModels;
using SingularityForensic.Android.Info.Views;

namespace SingularityForensic.Android.Info.TabModels {
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
