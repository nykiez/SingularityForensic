using SingularityForensic.Casing.MessageBoxes;
using SingularityForensic.Contracts.Casing;
using System.ComponentModel.Composition;

namespace SingularityForensic.Casing {
    [Export(typeof(ICaseDialogService))]
    public class CaseDialogService : ICaseDialogService {
        public ICase CreateCase() {
            return CreateCaseMessageBox.Show();
        }

        public void ShowCaseProperty(ICase cs) {
            return;
        }
    }
}
