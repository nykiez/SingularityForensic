using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing {
    public static class Constants {
        //案件节点类型;
        public const string CaseEvidenceUnit = nameof(CaseEvidenceUnit);

        //未知镜像案件文件类型;
        public const string UnKnownDeviceImg = nameof(UnKnownDeviceImg);

        //ITunes案件文件(夹)类型;
        public const string ITunesBackUpFolder = nameof(ITunesBackUpFolder);

        /// <summary>
        /// 案件节点类型;
        /// </summary>
        public const string TreeUnitType_Case = nameof(TreeUnitType_Case);

        /// <summary>
        /// 案件文件节点;
        /// </summary>
        public const string TreeUnitTag_CaseEvidence = nameof(TreeUnitTag_CaseEvidence);
    }
}
