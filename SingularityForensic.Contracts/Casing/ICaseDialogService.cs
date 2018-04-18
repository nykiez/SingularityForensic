using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing {
    //案件相关对话框功能;
    public interface ICaseDialogService {
        /// <summary>
        /// 创建一个新的案件;
        /// </summary>
        /// <returns></returns>
        ICase CreateCase();
        /// <summary>
        /// 显示案件文件信息;
        /// </summary>
        /// <param name="cs"></param>
        void ShowCaseProperty(ICase cs);
        /// <summary>
        /// 显示证据项信息;
        /// </summary>
        /// <param name="evidence"></param>
        void ShowCaseEvidenceProperty(ICaseEvidence evidence);
    }

    public class CaseDialogService : GenericServiceStaticInstance<ICaseDialogService> {

    }
}
