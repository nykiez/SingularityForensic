using Prism.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer.Events {
    public class PartitionsBrowserViewModelCreatedEvent:
        PubSubEvent<IPartitionsBrowserViewModel> {
    }
}
