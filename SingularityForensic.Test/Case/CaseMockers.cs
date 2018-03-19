using Moq;
using SingularityForensic.Casing;
using SingularityForensic.Contracts.Casing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Case {
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
                            CaseTime = "2013131"
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
