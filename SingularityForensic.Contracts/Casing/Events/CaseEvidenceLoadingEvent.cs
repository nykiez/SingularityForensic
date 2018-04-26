using Prism.Events;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
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
    public class CaseEvidenceLoadingEvent:PubSubEvent<(ICaseEvidence csEvidence,IProgressReporter reporter)> {

    }

    public interface ICaseEvidenceLoadingEventHandler:IEventHandler<(ICaseEvidence csEvidence, IProgressReporter reporter)> {

    }
}
