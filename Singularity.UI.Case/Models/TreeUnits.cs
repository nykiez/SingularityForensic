using CDFCUIContracts.Commands;
using CDFCUIContracts.Models;
using Singularity.UI.Case.Contracts;
using Singularity.UI.Case.Resources;
using SingularityForensic.Modules.MainPage.Models;
using System;
using System.Collections.ObjectModel;

namespace Singularity.UI.Case.Models {
    /// <summary>
    /// 案件节点;
    /// </summary>
    public class CaseTreeUnit : TreeUnit {
        public CaseTreeUnit(SingularityCase sCase) : base(null) {
            SingularityCase = sCase ?? throw new ArgumentNullException(nameof(sCase));
            Label = sCase.CaseName;
            Icon = IconSources.CaseUnitIcon;
        }

        public override ObservableCollection<ICommandItem> ContextCommands { get; set; } = null;

        public SingularityCase SingularityCase { get; private set; }

        public override ObservableCollection<ITreeUnit> Children { get; set; } = new ObservableCollection<ITreeUnit>();
    }

    //案件文件节点;
    public class CaseFileUnit<TCaseFile> : ExtTreeUnit<TCaseFile> where TCaseFile : ICaseFile {
        public CaseFileUnit(TCaseFile cFile, TreeUnit parent, string pinKind = null) : base(cFile, parent, (GetCaseFileTypeUnit(pinKind, cFile))) {
        }
        
        private static string GetCaseFileTypeUnit(string pinKind, TCaseFile cFile) {
            if (pinKind == null) {
                pinKind = $"{cFile.GetType().Name}CaseFileUnit";
            }
            return pinKind;
        }
        
        public override ObservableCollection<ITreeUnit> Children { get; set; } = new ObservableCollection<ITreeUnit>();

        public override ObservableCollection<ICommandItem> ContextCommands { get; set; }
    }
}
