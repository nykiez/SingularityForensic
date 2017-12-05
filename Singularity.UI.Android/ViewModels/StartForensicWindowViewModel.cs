using Singularity.Android.Models;
using Singularity.UI.Info.Models;
using Singularity.UI.Info.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Singularity.UI.Info.Android.ViewModels {
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal class AndStartForensicWindowViewModel : StartForensicWindowViewModel<AndroidDeviceCaseEvidence> {
        static AndStartForensicWindowViewModel() {
            
        }


        [ImportingConstructor]
        public AndStartForensicWindowViewModel(
            [ImportMany]IEnumerable<CheckGroupTreeItem<AndroidDeviceCaseEvidence>> groups,
            [ImportMany]IEnumerable<CheckItemTreeItem<AndroidDeviceCaseEvidence>> items):base(groups,items) {
            
        }
        
    }
}
