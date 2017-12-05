using CDFCUIContracts.Abstracts;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.Contracts.FileExplorer.Events {

    //内部Tab选择变更事件;
    public class InnerTabSelectedChangedEvent : PubSubEvent<ITabModel> {
    }
}
