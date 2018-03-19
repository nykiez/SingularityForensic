using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing.Events {
    //案件卸载时发生;
    public class CaseUnloadingEvent:PubSubEvent<CancelEventArgs> {

    }
}
