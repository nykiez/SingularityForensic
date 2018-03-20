using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing {
    //案件相关对话框功能;
    public interface ICaseDialogService {
        //创建一个新的案件;
        ICase CreateCase();
        //显示案件文件信息;
        void ShowCaseProperty(ICase cs);
    }

    public class CsDialogService : GenericServiceStaticInstance<ICaseDialogService> {

    }
}
