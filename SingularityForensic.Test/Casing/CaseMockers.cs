using Moq;
using SingularityForensic.Contracts.Casing;

namespace SingularityForensic.Test.Casing {
    static class CaseMockers {
        private static ICaseDialogService _caseDialogServiceMocker;

        internal static ICaseDialogService CaseDialogServiceMocker {
            get {
                if(_caseDialogServiceMocker == null) {
                    var csDialogService = new Mock<ICaseDialogService>();
                    csDialogService.Setup(p => p.CreateCase()).Returns(() => {
                        var cs = new SingularityForensic.Casing.Case(CaseFolder , CaseName) {
                            CaseDes = "das",
                            CaseInfo = "Info",
                            CaseType = "dd",
                            CaseNum = "001",
                            CaseTime = "2013131",
                            GUID = "317983179831"
                        };
                        return cs;
                    });
                    _caseDialogServiceMocker = csDialogService.Object;
                }
                return _caseDialogServiceMocker;
            }
        }

        //测试案件文件夹;
        public static string CaseFolder = "E://Cases";

        //测试案件名称;
        public static string CaseName = "Case001";

    }
    
}
