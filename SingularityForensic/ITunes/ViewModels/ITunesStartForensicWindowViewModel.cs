using Singularity.UI.Info.Models;
using Singularity.UI.Info.ViewModels;
using Singularity.UI.ITunes.Models;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Singularity.UI.ITunes.ViewModels {
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ITunesStartForensicWindowViewModel:StartForensicWindowViewModel<ITunesBackUpCaseFile> {
        [ImportingConstructor]
        public ITunesStartForensicWindowViewModel([ImportMany]IEnumerable<CheckGroupTreeItem<ITunesBackUpCaseFile>> groups,
            [ImportMany] IEnumerable<CheckItemTreeItem<ITunesBackUpCaseFile>> items) : base(groups, items) {

        }
        //[ImportingConstructor]
        //public ITunesStartForensicWindowViewModel( IEnumerable<CheckGroupTreeItem<ITunesBackUpCaseFile>> groups) : base(groups, null) {

        //}
    }
}
