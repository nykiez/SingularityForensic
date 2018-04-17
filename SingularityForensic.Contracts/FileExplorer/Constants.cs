using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public static class Constants {
        public const string HexDataContextTag_BlockedStream = nameof(HexDataContextTag_BlockedStream);

        /// <summary>
        /// 文件系统节点;
        /// </summary>
        public const string TreeUnitTag_FileSystem_File = nameof(TreeUnitTag_FileSystem_File);
        
        /// <summary>
        /// 文件系统下子节点内容;
        /// </summary>
        public const string TreeUnitType_InnerFile = nameof(TreeUnitType_InnerFile);
        
        /// <summary>
        /// 案件文件节点类型;
        /// </summary>
        public const string TreeUnitType_CaseEvidence = nameof(TreeUnitType_CaseEvidence);

        /// <summary>
        /// 文件关联实体单位;
        /// </summary>
        public const string DocumentTag_File = nameof(DocumentTag_File);

        public const string HexDataContext_FolderBrowser_Partition = nameof(HexDataContext_FolderBrowser_Partition);

        public const string HexDataContext_FolderBrowser_File = nameof(HexDataContext_FolderBrowser_File);
        //public const string FileMetaDataType_File = nameof(FileMetaDataType_File);
        //public const string FileMetaDataType_Partition = nameof(FileMetaDataType_Partition);
    }
}
