using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public static class Constants {
        public const string HexDataContextTag_StreamFile = nameof(HexDataContextTag_StreamFile);

        /// <summary>
        /// 文件系统节点类型;
        /// </summary>
        public const string TreeUnitTag_FileSystem_File = nameof(TreeUnitTag_FileSystem_File);

        /// <summary>
        /// 节点存储的内部文件;
        /// </summary>
        public const string TreeUnitTag_InnerFile = nameof(TreeUnitTag_InnerFile);

        /// <summary>
        /// 文件系统下子目录节点内容;
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

        /// <summary>
        /// 文件-资源管理器实体关联;
        /// </summary>
        public const string DocumentTag_FolderBrowserViewModel = nameof(DocumentTag_FolderBrowserViewModel);

        /// <summary>
        /// 设备-分区资源管理器实体关联;
        /// </summary>
        public const string DocumentTag_PartitionsBrowserViewModel = nameof(DocumentTag_PartitionsBrowserViewModel);


        public const string HexDataContext_FolderBrowser_Partition = nameof(HexDataContext_FolderBrowser_Partition);

        public const string HexDataContext_FolderBrowser_File = nameof(HexDataContext_FolderBrowser_File);
        //public const string FileMetaDataType_File = nameof(FileMetaDataType_File);
        //public const string FileMetaDataType_Partition = nameof(FileMetaDataType_Partition);
    }
}
