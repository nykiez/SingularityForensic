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
        /// <summary>
        /// 当前案件的本地路径;
        /// </summary>
        string Path { get; }
        string CaseName { get; }
        /// <summary>
        /// 添加新的证据项;仅作保存,并不会进行加载动作;若要进行加载,请使用<see cref="LoadCaseEvidence(ICaseEvidence,IProgressReporter)"/>
        /// </summary>
        /// <param name="csEvidence"></param>
        void AddNewCaseEvidence(ICaseEvidence csEvidence);
        /// <summary>
        /// 加载证据项;
        /// </summary>
        /// <param name="csEvidence"></param>
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
