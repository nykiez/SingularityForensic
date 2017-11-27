using Singularity.UI.Case.ViewModels;
using Singularity.UI.Case.Views;

namespace Singularity.UI.Case.MessageBoxes {
    public class CreateCaseMessageBox {
        public static SingularityCase Show() {
            var vm = new CreateCaseWindowViewModel();
            var msgBox = new CreateCaseWindow(vm);
            var res = msgBox.ShowDialog();
            if (res == true) {
                var sCase = vm.SingulartityCase;
                return sCase;
            }
            return null;
        }
    }
}
