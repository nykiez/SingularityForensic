using Singularity.Contracts.Case;
using System;

namespace Singularity.UI.ITunes.Models {
    /// <summary>
    /// ITunes备份案件文件;
    /// </summary>
    public class ITunesBackUpCaseFile : CaseEvidence {
        public ITunesBackUpCaseFile(string name, string interLabel, DateTime dateAdded): 
            base(nameof(ITunesBackUpCaseFile), name, interLabel, dateAdded) {

        }

        //备份本地路径;
        public string LocalBackUpPath => InterLabel;
    }
}
