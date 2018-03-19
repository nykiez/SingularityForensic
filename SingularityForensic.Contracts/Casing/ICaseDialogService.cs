using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing {
    //案件相关对话框功能;
    public interface ICaseDialogService {
        ICase CreateCase();
    }

    public class CsDialogService : GenericServiceStaticInstance<ICaseDialogService> {

    }
}
