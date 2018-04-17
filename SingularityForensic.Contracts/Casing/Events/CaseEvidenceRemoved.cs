using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing.Events {
    //案件文件被移除时发生;
    public class CaseEvidenceRemovedEvent:PubSubEvent<ICaseEvidence> {

    }
}
