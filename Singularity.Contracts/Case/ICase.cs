using System.Collections.Generic;
using System.Xml.Linq;

namespace Singularity.Contracts.Case {
    public interface ICase {
        //案件名称;
        string CaseName { get; set; }

        //案件时间;
        string CaseTime { get; set; }

        //案件类型;
        string CaseType { get; set; }

        //案件编号;
        string CaseNum { get; set; }

        //案件描述;
        string CaseDes {get;set;}
        
        //案件信息;
        string CaseInfo { get; set;}

        //案件路径;
        string Path { get; }

        //所有案件文件;
        IEnumerable<ICaseEvidence> CaseEvidences { get; }

        //案件对应的根文档;
        XDocument XDoc { get; }
    }

    
}
