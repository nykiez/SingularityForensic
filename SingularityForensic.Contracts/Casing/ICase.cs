using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.Casing {
    public interface ICase {
        string CaseTime { get; }
        string CaseType { get; }
        string CaseNum { get; }
        string CaseDes { get; }
        string CaseInfo { get; }
        XDocument XDoc { get; }
        IEnumerable<CaseEvidence> CaseEvidences {get;}
        string Path { get; }
        string CaseName { get; }
        void AddNewCaseEvidence(CaseEvidence csEvidence);
        void LoadCaseEvidence(CaseEvidence csEvidence);
        void LoadCaseEvidence(CaseEvidence csEvidence, ProgressReporter reporter);
        void Save();
    }
    

}
