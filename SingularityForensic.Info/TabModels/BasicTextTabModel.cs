using SingularityForensic.Info.ViewModels;
using SingularityForensic.Info.Views;
using SingularityForensic.Contracts.TabControl;
using SingularityForensic.Controls.Info.ViewModels;
using SingularityForensic.Controls.Info.Views;

namespace SingularityForensic.Info.TabModels {

    public class BasicTextTabModel:PinTabModel {
        public BasicTextTabModel(string pinKind,string text):base(pinKind) {
            var vm = new InfoBasicViewModel { BasicText = text };
            Content = new InfoBasicView() { DataContext = vm };
        }
    }
}
