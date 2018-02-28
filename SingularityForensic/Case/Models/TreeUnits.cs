using CDFCUIContracts.Commands;
using CDFCUIContracts.Models;
using SingularityForensic.Case;
using System;
using System.Collections.ObjectModel;

namespace SingularityForensic.Case.Models {
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

    
}
