using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.Casing {
    public interface ICase {
        /// <summary>
        /// 案件时间;
        /// </summary>
        string CaseTime { get; }
        /// <summary>
        /// 案件类型;
        /// </summary>
        string CaseType { get; }
        /// <summary>
        /// 案件编号;
        /// </summary>
        string CaseNum { get; }
        /// <summary>
        /// 案件描述;
        /// </summary>
        string CaseDes { get; }
        /// <summary>
        /// 案件详细;
        /// </summary>
        string CaseInfo { get; }
        /// <summary>
        /// 标识GUID;
        /// </summary>
        string GUID { get; }
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
        /// <summary>
        /// 保存;
        /// </summary>
        void Save();
    }
    

}
