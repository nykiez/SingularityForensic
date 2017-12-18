using CDFCUIContracts.Models;
using System;

namespace Singularity.Contracts.Case {
    //案件文件节点;
    public class CaseEvidenceUnit<TCaseEvidence> : TreeUnit, ICaseEvidenceUnit<TCaseEvidence> where TCaseEvidence : ICaseEvidence {
        public CaseEvidenceUnit(TCaseEvidence cFile, ITreeUnit parent, string pinKind = null) : base(parent) {
            this.Evidence = cFile;
        }

        private static string GetCaseFileTypeUnit(string pinKind, TCaseEvidence cFile) {
            if (pinKind == null) {
                pinKind = $"{cFile.GetType().Name}CaseFileUnit";
            }
            return pinKind;
        }

        public TCaseEvidence Evidence { get; }
    }
}
