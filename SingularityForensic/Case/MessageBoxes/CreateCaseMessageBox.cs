using SingularityForensic.Contracts.Case;
using SingularityForensic.Case.ViewModels;
using SingularityForensic.Case.Views;

namespace SingularityForensic.Case.MessageBoxes {
    public class CreateCaseMessageBox {
        public static SingularityCase Show() {
            var vm = new CreateCaseWindowViewModel();
            var msgBox = new CreateCaseWindow(vm);
            var res = msgBox.ShowDialog();
            if (res == true) {
                var sCase = vm.SingularityCase;
                return sCase;
            }
            return null;
        }
    }
}
