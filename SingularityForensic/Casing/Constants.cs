using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Casing {
    static class Constants {
        //案件类型节点密钥;
        //public const string CaseEvidenceUnitKey = nameof(CaseEvidenceUnitKey);
        //案件模块正在被加载;
        public const string CaseModuleBeingLoaded = nameof(CaseModuleBeingLoaded);

        //案件模块正在被加载;
        public const string CaseConstructingFailed = nameof(CaseConstructingFailed);

        //支持的案件文件后缀类型;
        public const string SupportedCaseFileType = nameof(SupportedCaseFileType);

        //确认是否关闭案件;
        public const string ConfirmToCloseAndOpen = nameof(ConfirmToCloseAndOpen);

        //打开案件失败;
        public const string FailedToOpenCase = nameof(FailedToOpenCase);

        //打开案件;
        public const string LoadingCase = nameof(LoadingCase);
    }
}
