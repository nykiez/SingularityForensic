using Singularity.Contracts.Case;
using Singularity.UI.Case.Views;

namespace Singularity.UI.Case.MessageBoxes {
    public static class ShowCaseFilePropertyMessageBox {
        //public static TCaseFile Show<TCaseFile>(TCaseFile caseFile) where TCaseFile : ICaseFile {
        //    //ServiceLocator.Current.GetInstance<>
        //}

        public static TCaseFile Show<TCaseFile>(TCaseFile caseFile) where TCaseFile : ICaseEvidence {
            var window = new ShowCaseFilePropertyWindow(caseFile);
            var res = window.ShowDialog();
            if (res == true) {
                return caseFile;
                //return window.CFile;
            }
            return default(TCaseFile);
        }
    }
}
