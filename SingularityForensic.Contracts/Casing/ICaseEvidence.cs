using SingularityForensic.Contracts.Common;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.Casing {
    /// <summary>
    /// 案件证据项契约;
    /// </summary>
    public interface ICaseEvidence : ITextInstanceExtensible {
        string[] EvidenceTypeGuids { get; }
        DateTime DateAdded { get; }
        string InterLabel { get; }
        string Name { get; set; }
        string Comments { get; set; }
        XElement XElem { get; }
        string EvidenceGUID { get; }
    }
    
}
