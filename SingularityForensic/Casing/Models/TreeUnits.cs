using CDFCUIContracts.Commands;
using CDFCUIContracts.Models;
using SingularityForensic.Contracts.Casing;
using System;
using System.Collections.ObjectModel;

namespace SingularityForensic.Casing.Models {
    /// <summary>
    /// 案件节点;
    /// </summary>
    public class CaseTreeUnit : TreeUnit {
        public CaseTreeUnit(ICase sCase) : base(null) {
            SingularityCase = sCase ?? throw new ArgumentNullException(nameof(sCase));
            Label = sCase.CaseName;
            Icon = IconSources.CaseUnitIcon;
        }

        public override ObservableCollection<ICommandItem> ContextCommands { get; set; } = null;

        public ICase SingularityCase { get; private set; }

        public override ObservableCollection<ITreeUnit> Children { get; set; } = new ObservableCollection<ITreeUnit>();
    }

    
}
