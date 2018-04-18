using SingularityForensic.Casing.MessageBoxes;
using SingularityForensic.Contracts.Casing;
using System.ComponentModel.Composition;

namespace SingularityForensic.Casing {
    [Export(typeof(ICaseDialogService))]
    public class CaseDialogServiceImpl : ICaseDialogService {
        public ICase CreateCase() {
            return CreateCaseMessageBox.Show();
        }

        public void ShowCaseEvidenceProperty(ICaseEvidence evidence) {
            ShowCaseFilePropertyMessageBox.Show(evidence);
        }

        public void ShowCaseProperty(ICase cs) {
            return;
        }
    }
}
