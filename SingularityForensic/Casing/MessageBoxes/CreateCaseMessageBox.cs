using SingularityForensic.Contracts.Casing;
using SingularityForensic.Casing.ViewModels;
using SingularityForensic.Casing.Views;

namespace SingularityForensic.Casing.MessageBoxes {
    public class CreateCaseMessageBox {
        public static ICase Show() {
            var vm = new CreateCaseWindowViewModel();
            var msgBox = new CreateCaseWindow(vm);
            var res = msgBox.ShowDialog();
            if (res == true) {
                var sCase = vm.Case;
                return sCase;
            }
            return null;
        }
    }
}
