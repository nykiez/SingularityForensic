using Prism.Events;
using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing.Events {
    /// <summary>
    /// 案件正在加载事件;
    /// </summary>
    /// <typeparam name="CaseEvidence"></typeparam>
    public class CaseEvidenceLoadingEvent:PubSubEvent<(CaseEvidence csEvidence,IProgressReporter reporter)> {

    }
}
