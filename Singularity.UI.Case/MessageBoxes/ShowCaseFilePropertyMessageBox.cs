using Singularity.UI.Case.Contracts;
using Singularity.UI.Case.Views;

namespace Singularity.UI.Case.MessageBoxes {
    public static class ShowCaseFilePropertyMessageBox {
        //public static TCaseFile Show<TCaseFile>(TCaseFile caseFile) where TCaseFile : ICaseFile {
        //    //ServiceLocator.Current.GetInstance<>
        //}

        public static TCaseFile Show<TCaseFile>(TCaseFile caseFile) {
            var window = new ShowCaseFilePropertyWindow(caseFile as ICaseFile);
            var res = window.ShowDialog();
            if (res == true) {
                return caseFile;
                //return window.CFile;
            }
            return default(TCaseFile);
        }
    }
}
