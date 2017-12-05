using Singularity.Contracts.TabControl;
using Singularity.UI.Info.ViewModels;
using Singularity.UI.Info.Views;

namespace Singularity.UI.Info.TabModels {

    public class BasicTextTabModel:PinTabModel {
        public BasicTextTabModel(string pinKind,string text):base(pinKind) {
            var vm = new InfoBasicViewModel { BasicText = text };
            Content = new InfoBasicView() { DataContext = vm };
        }
    }
}
