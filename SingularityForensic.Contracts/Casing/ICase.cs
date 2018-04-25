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
        IEnumerable<ICaseEvidence> CaseEvidences {get;}
        string Path { get; }
        string CaseName { get; }
        void AddNewCaseEvidence(ICaseEvidence csEvidence);
        void LoadCaseEvidence(ICaseEvidence csEvidence);
        void LoadCaseEvidence(ICaseEvidence csEvidence, IProgressReporter reporter);
        /// <summary>
        /// 移除证据项;
        /// </summary>
        /// <param name="evidence"></param>
        void RemoveCaseEvidence(ICaseEvidence evidence);

        void Save();
    }
    

}
