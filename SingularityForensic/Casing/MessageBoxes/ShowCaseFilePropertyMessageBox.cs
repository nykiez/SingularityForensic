using SingularityForensic.Contracts.Casing;
using SingularityForensic.Casing.Views;

namespace SingularityForensic.Casing.MessageBoxes {
    public static class ShowCaseFilePropertyMessageBox {
        public static ICaseEvidence Show(ICaseEvidence caseFile) {
            var window = new ShowCaseFilePropertyWindow(caseFile);
            var res = window.ShowDialog();
            if (res == true) {
                return caseFile;
                //return window.CFile;
            }
            return default(ICaseEvidence);
        }
    }
}
