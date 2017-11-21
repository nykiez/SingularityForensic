using Singularity.UI.FileSystem.Models;
using Singularity.UI.Info.Models;
using Singularity.UI.Info.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Singularity.UI.Info.Android.ViewModels {
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal class AndStartForensicWindowViewModel : StartForensicWindowViewModel<AndroidDeviceCaseFile> {
        static AndStartForensicWindowViewModel() {
            
        }


        [ImportingConstructor]
        public AndStartForensicWindowViewModel(
            [ImportMany]IEnumerable<CheckGroupTreeItem<AndroidDeviceCaseFile>> groups,
            [ImportMany]IEnumerable<CheckItemTreeItem<AndroidDeviceCaseFile>> items):base(groups,items) {
            
        }
        
    }
}
