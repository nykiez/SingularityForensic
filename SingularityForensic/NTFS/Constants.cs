using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS {
    static partial class Constants {

        public const string UsnMetaName_RecordPosition = nameof(UsnMetaName_RecordPosition);


        public const string UsnMetaName_RecordLength = nameof(UsnMetaName_RecordLength);

        public const string UsnMetaName_FileReferenceNumber = nameof(UsnMetaName_FileReferenceNumber);

        public const string UsnMetaName_ParentFileReferenceNumber = nameof(UsnMetaName_ParentFileReferenceNumber);

        public const string UsnMetaName_Usn = nameof(UsnMetaName_Usn);
        public const string UsnMetaName_Reason = nameof(UsnMetaName_Reason);
        public const string UsnMetaName_FileAttributes = nameof(UsnMetaName_FileAttributes);
        public const string UsnMetaName_FileNameLength = nameof(UsnMetaName_FileNameLength);
        public const string UsnMetaName_FileNameOffset = nameof(UsnMetaName_FileNameOffset);
        public const string UsnMetaName_FileName = nameof(UsnMetaName_FileName);


        public const string UsnJrnlPreviewerView = nameof(UsnJrnlPreviewerView);

    }

    /// <summary>
    /// 语言部分;
    /// </summary>
    static partial class Constants {

        public const string DisplayName_LogFileRSTRHeader = nameof(DisplayName_LogFileRSTRHeader);


        public const string DisplayName_LogFileRSTRArea = nameof(DisplayName_LogFileRSTRArea);


        public const string DisplayName_LogFileRSTRClient = nameof(DisplayName_LogFileRSTRClient);


        public const string DisplayName_LogFileRCRDHeader = nameof(DisplayName_LogFileRCRDHeader);


        public const string DisplayName_LogFileLSNRecordHeader = nameof(DisplayName_LogFileLSNRecordHeader);


        public const string LogFileLSNRecordFieldPrefix = "LogFileLSNRecord_";


        public const string LogFileRSTRClientFieldPrefix = "LogFileRSTRClient_";


        public const string LogFileRSTRHeaderFieldPrefix = "LogFileRSTRHeader_";

        public const string LogFileRSTRAreaFieldPrefix = "LogFileRSTRArea_";


        public const string LogFileRCRDHeaderFieldPrefix = "LogFileRCRDHeader_";

    }
}
