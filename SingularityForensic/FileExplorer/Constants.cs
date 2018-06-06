using System;

namespace SingularityForensic.FileExplorer {
    public static partial class Constants {
        public const string DeviceNodeContextCommand = nameof(DeviceNodeContextCommand);
        
        /// <summary>
        /// 设备分区UI对象名;
        /// </summary>
        public const string PartitionsBrowserView = nameof(PartitionsBrowserView);

        /// <summary>
        /// 分区-文件UI对象名;
        /// </summary>
        public const string FolderBrowserView = nameof(FolderBrowserView);
        
        public const string HexDataContext_PartitionBrowser_Device = nameof(HexDataContext_PartitionBrowser_Device);

        public const string HexDataContext_PartitionBrowser_Partition = nameof(HexDataContext_PartitionBrowser_Partition);

        public const string PreviewerContext_PartitionBrowser = nameof(PreviewerContext_PartitionBrowser);

        public const string Document_FilePreviewer = nameof(Document_FilePreviewer);

        public const string Prefix_Partition = nameof(Prefix_Partition);

        public const string PartitionKey_CustomSignSearch = nameof(PartitionKey_CustomSignSearch);

        public const string RegularFileKey_CustomSignSearch = nameof(RegularFileKey_CustomSignSearch);

        public const string DocumentTitle_CustomSignSearch = nameof(DocumentTitle_CustomSignSearch);

        public const string RegularFileName_CustomSignSearch = nameof(RegularFileName_CustomSignSearch);

        public const string DefaultFileExtension_CustomSignSearch = nameof(DefaultFileExtension_CustomSignSearch);
        
        public const string DocumentTag_FilePreviewer = nameof(DocumentTag_FilePreviewer);
        

        public const string StatusBarItemGUID_FilesCount = nameof(StatusBarItemGUID_FilesCount);
        
        public const string StatusBarItemGUID_RegFileCount = nameof(StatusBarItemGUID_RegFileCount);


        public const string StatusBarItemGUID_DirectoryCount = nameof(StatusBarItemGUID_DirectoryCount);


        /// <summary>
        /// 打开方式,右键;
        /// </summary>
        public const string CommandItemGUID_OpenFileWith = nameof(CommandItemGUID_OpenFileWith);

        public const string ViewerProgram_ConfigFile = "ViewerPrograms.xml";

        public const string XmlElemName_Viewer_Root = "Programs";

        public const string XmlElemName_Viewer_Pro = "ViewerProgram";
        
        public const string XmlElemName_View_Pro_Path = "Path";

        public const string XmlAttrName_View_Pro_Name = "Name";
    }

    /// <summary>
    /// 语言部分;
    /// </summary>
    public static partial class Constants {

        public const string OpenFileFilter_Execute = nameof(OpenFileFilter_Execute);

        //文件系统资源管理器模块正在加载;
        public const string FileExploerLoading = nameof(FileExploerLoading);

        /// <summary>
        /// 分区树形节点名;
        /// </summary>
        public const string UnitLabel_Paritition = nameof(UnitLabel_Paritition);

        /// <summary>
        /// 元数据名部分;
        /// </summary>

        public const string FileMetaDataName_Name = nameof(FileMetaDataName_Name);
        public const string FileMetaDataName_FileType = nameof(FileMetaDataName_FileType);
        public const string FileMetaDataName_Size = nameof(FileMetaDataName_Size);
        public const string FileMetaDataName_ModifiedTime = nameof(FileMetaDataName_ModifiedTime);
        public const string FileMetaDataName_AccessedTime = nameof(FileMetaDataName_AccessedTime);
        public const string FileMetaDataName_CreateTime = nameof(FileMetaDataName_CreateTime);
        public const string FileMetaDataName_StartLBA = nameof(FileMetaDataName_StartLBA);

        public const string FileMetaDataName_Deleted = nameof(FileMetaDataName_Deleted);
        public const string FileMetaDataName_Path = nameof(FileMetaDataName_Path);

        public const string FileDeleted_Yes = nameof(FileDeleted_Yes);
        public const string FileDeleted_No = nameof(FileDeleted_No);
        public const string FileDeleted_Unknown = nameof(FileDeleted_Unknown);

        

        /// <summary>
        /// 元数据区分部分;
        /// </summary>

        public const string FileMetaDataGUID_Name = nameof(FileMetaDataGUID_Name);
        public const string FileMetaDataGUID_FileType = nameof(FileMetaDataGUID_FileType);
        public const string FileMetaDataGUID_Size = nameof(FileMetaDataGUID_Size);
        public const string FileMetaDataGUID_ModifiedTime = nameof(FileMetaDataGUID_ModifiedTime);
        public const string FileMetaDataGUID_AccessedTime = nameof(FileMetaDataGUID_AccessedTime);
        public const string FileMetaDataGUID_CreateTime = nameof(FileMetaDataGUID_CreateTime);
        public const string FileMetaDataGUID_StartLBA = nameof(FileMetaDataGUID_StartLBA);

        public const string FileMetaDataGUID_Deleted = nameof(FileMetaDataGUID_Deleted);
        public const string FileMetaDataGUID_Path = nameof(FileMetaDataGUID_Path);

        public const string FileType_Directory = nameof(FileType_Directory);
        public const string FileType_RegularFile = nameof(FileType_RegularFile);
        public const string FileType_Unknown = nameof(FileType_Unknown);

        public const string PartMetaDataGUID_PartType = nameof(PartMetaDataGUID_PartType);
        
        public const string PartMetaDataGUID_StartLBA = nameof(PartMetaDataGUID_StartLBA);
        public const string PartitionMetaDataGUID_LastMountTime = nameof(PartitionMetaDataGUID_LastMountTime);

        public const string PartMetaDataName_PartType = nameof(PartMetaDataName_PartType);
        public const string PartMetaDataName_StartLBA = nameof(PartMetaDataName_StartLBA);
        public const string PartitionMetaDataName_LastMountTime = nameof(PartitionMetaDataName_LastMountTime);

        //文件/分区本体;
        public const string PartMetaDataName_Partition = nameof(PartMetaDataName_Partition);
        public const string PartMetaDataGUID_Partition = nameof(PartMetaDataGUID_Partition);

        public const string FileMetaDataName_File = nameof(FileMetaDataName_File);
        public const string FileMetaDataGUID_File = nameof(FileMetaDataGUID_File);

        public const string DocumentTitle_HexDevice = nameof(DocumentTitle_HexDevice);
        public const string DocumentTitle_HexPartition = nameof(DocumentTitle_HexPartition);
        public const string DocumentTitle_HexFile = nameof(DocumentTitle_HexFile);
        public const string DocumentTitle_FilePreview = nameof(DocumentTitle_FilePreview);

        public const string TreeUnitLabel_FileSystem = nameof(TreeUnitLabel_FileSystem);

        public const string TempDirectoryName = "Temp";

        public const string ContextCommandName_SaveAs = nameof(ContextCommandName_SaveAs);
        public const string ContextCommandName_ViewFile = nameof(ContextCommandName_ViewFile);
        public const string ContextCommandName_OpenFileWith = nameof(ContextCommandName_OpenFileWith);
        public const string ContextCommandName_Navigate = nameof(ContextCommandName_Navigate);
        public const string ContextCommandName_ListBlock = nameof(ContextCommandName_ListBlock);
        public const string ContextCommandName_ComputeHash = nameof(ContextCommandName_ComputeHash);

        public const string ContextCommandName_CustomSignSearch = nameof(ContextCommandName_CustomSignSearch);

        public const string ContextCommandName_OpenFileWithAnotherPro = nameof(ContextCommandName_OpenFileWithAnotherPro);


        public const string ContextCommandName_RecurBrowse = nameof(ContextCommandName_RecurBrowse);



        public const string MsgText_FileSaveDone = nameof(MsgText_FileSaveDone);
        public const string MsgText_FailedToCreateDirectory = nameof(MsgText_FailedToCreateDirectory);

        public const string WindowTitle_ListBlock = nameof(WindowTitle_ListBlock);

        public const string WindowTitle_ComputeHash = nameof(WindowTitle_ComputeHash);
        public const string WindowTitle_CustomSignSearch = nameof(WindowTitle_CustomSignSearch);

        public const string StatusBarItemText_FileCount = nameof(StatusBarItemText_FileCount);

        public const string StatusBarItemText_RegFileCount = nameof(StatusBarItemText_RegFileCount);
        
        public const string StatusBarItemText_DirectoryCount = nameof(StatusBarItemText_DirectoryCount);

        //public const string FileMetaDataName_ = nameof(FileMetaDataName_);
        //public const string FileMetaDataName_ = nameof(FileMetaDataName_);
        //public const string FileMetaDataName_ = nameof(FileMetaDataName_);
        //public const string FileMetaDataName_ = nameof(FileMetaDataName_);
    }
    
}
