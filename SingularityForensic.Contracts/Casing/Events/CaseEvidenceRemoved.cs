using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing.Events {
    /// <summary>
    /// 案件文件被移除时发生;
    /// </summary>
    public class CaseEvidenceRemovedEvent:PubSubEvent<ICaseEvidence> {

    }

    public interface ICaseEvidenceRemovedEventHandler : IEventHandler<ICaseEvidence> {

    }
}
