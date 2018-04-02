using System;

namespace SingularityForensic.FileExplorer {
    public static partial class Constants {
        public const string FileSystemTreeUnit = nameof(FileSystemTreeUnit);

        public const string DeviceNodeContextCommand = nameof(DeviceNodeContextCommand);
    }

    /// <summary>
    /// 语言部分;
    /// </summary>
    public static partial class Constants {
        //文件系统资源管理器模块正在加载;
        public const string FileExploerLoading = nameof(FileExploerLoading);

        /// <summary>
        /// 元数据名部分;
        /// </summary>
        
        public const string FileMetaDataName_Name = nameof(FileMetaDataName_Name);
        public const string FileMetaDataName_Size = nameof(FileMetaDataName_Size);
        public const string FileMetaDataName_ModifiedTime = nameof(FileMetaDataName_ModifiedTime);
        public const string FileMetaDataName_AccessedTime = nameof(FileMetaDataName_AccessedTime);
        public const string FileMetaDataName_CreateTime = nameof(FileMetaDataName_CreateTime);
        public const string FileMetaDataName_StartLBA = nameof(FileMetaDataName_StartLBA);
        public const string FileMetaDataName_Deleted = nameof(FileMetaDataName_Deleted);

        /// <summary>
        /// 元数据区分部分;
        /// </summary>
        
        public const string FileMetaDataGUID_Name = nameof(FileMetaDataGUID_Name);
        public const string FileMetaDataGUID_Size = nameof(FileMetaDataGUID_Size);
        public const string FileMetaDataGUID_ModifiedTime = nameof(FileMetaDataGUID_ModifiedTime);
        public const string FileMetaDataGUID_AccessedTime = nameof(FileMetaDataGUID_AccessedTime);
        public const string FileMetaDataGUID_CreateTime = nameof(FileMetaDataGUID_CreateTime);
        public const string FileMetaDataGUID_StartLBA = nameof(FileMetaDataGUID_StartLBA);
        public const string FileMetaDataGUID_Deleted = nameof(FileMetaDataGUID_Deleted);

        //public const string FileMetaDataName_ = nameof(FileMetaDataName_);
        //public const string FileMetaDataName_ = nameof(FileMetaDataName_);
        //public const string FileMetaDataName_ = nameof(FileMetaDataName_);
        //public const string FileMetaDataName_ = nameof(FileMetaDataName_);
    }

    /// <summary>
    /// 类型部分;
    /// </summary>
    public static partial class Constants {
        public static readonly TypeGenericStaticInstance<string> 
            StringType = new TypeGenericStaticInstance<string>();
        public static readonly TypeGenericStaticInstance<int>
            IntType = new TypeGenericStaticInstance<int>();
        public static readonly TypeGenericStaticInstance<long>
            LongType = new TypeGenericStaticInstance<long>();
        
    }

    public class TypeGenericStaticInstance<TType>{
        private static Type _typeInstance;
        public static Type TypeInstance => _typeInstance ?? (_typeInstance = typeof(TType));
    }
    
}
