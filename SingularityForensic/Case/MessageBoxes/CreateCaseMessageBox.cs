using Singularity.Contracts.Case;
using Singularity.UI.Case.ViewModels;
using Singularity.UI.Case.Views;

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
