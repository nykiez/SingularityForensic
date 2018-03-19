using SingularityForensic.Contracts.Casing;
using SingularityForensic.Casing.Views;

namespace SingularityForensic.Casing.MessageBoxes {
    public static class ShowCaseFilePropertyMessageBox {
        //public static TCaseFile Show<TCaseFile>(TCaseFile caseFile) where TCaseFile : ICaseFile {
        //    //ServiceLocator.Current.GetInstance<>
        //}

        public static CaseEvidence Show(CaseEvidence caseFile) {
            var window = new ShowCaseFilePropertyWindow(caseFile);
            var res = window.ShowDialog();
            if (res == true) {
                return caseFile;
                //return window.CFile;
            }
            return default(CaseEvidence);
        }
    }
}
