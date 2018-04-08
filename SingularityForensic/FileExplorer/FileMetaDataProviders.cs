using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer {
    abstract class FileMetaDataProviderBase : IFileMetaDataProvider {
        public abstract string MetaDataName { get; }

        public abstract Type MetaDataType { get; }

        public abstract string GUID { get; }

        public abstract int Order { get; }

        public virtual bool AutoConvert => true;

        public abstract object GetDataObject(FileBase file);
    }

    abstract class FileNameMetaDataProviderBase : FileMetaDataProviderBase {
        public override string MetaDataName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_Name);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.FileMetaDataGUID_Name;

        public override int Order => 2;

        public override bool AutoConvert => false;

        public override object GetDataObject(FileBase file) => file?.Name;
    }

    abstract class PartMetaDataProviderBase : IPartMetaDataProvider {
        public abstract string MetaDataName { get;}

        public abstract Type MetaDataType { get; }

        public abstract string GUID { get; }

        public abstract int Order { get; }

        public virtual bool AutoConvert => true;

        public abstract object GetDataObject(Partition file);
    }

    abstract class FileSizeMetaDataProviderBase : IFileMetaDataProvider {
        public string MetaDataName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_Size);

        public Type MetaDataType => typeof(long?);

        public string GUID => Constants.FileMetaDataGUID_Size;

        public abstract int Order { get; }

        public object GetDataObject(FileBase file) => file.Size;

        public virtual bool AutoConvert => true;
    }

    /// <summary>
    /// 文件名元数据提供器;
    /// </summary>
    [Export(typeof(IFileMetaDataProvider))]
    class FileNameMetaDataProvider : FileNameMetaDataProviderBase {
        
    }

    

    [Export(typeof(IFileMetaDataProvider))]
    class FileDeletedMetaDataProvider : FileMetaDataProviderBase {
        public override string MetaDataName => 
            LanguageService.FindResourceString(Constants.FileMetaDataName_Deleted);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.FileMetaDataGUID_Deleted;

        public override int Order => 12;

        public override object GetDataObject(FileBase file) {
            if(file is IDeletable deletable) {
                switch (deletable.Deleted) {
                    case true:
                        return LanguageService.FindResourceString(Constants.FileDeleted_Yes);
                    case false:
                        return LanguageService.FindResourceString(Constants.FileDeleted_No);
                    default:
                        return LanguageService.FindResourceString(Constants.FileDeleted_Unknown);
                }
            }
            return LanguageService.FindResourceString(Constants.FileDeleted_Unknown);
        }
    }

    [Export(typeof(IFileMetaDataProvider))]
    class FileSizeMetaDataProvider : FileSizeMetaDataProviderBase {
        public override int Order => 2;
    }

    [Export(typeof(IFileMetaDataProvider))]
    class FileTypeMetaDataProvider : FileMetaDataProviderBase {
        public override string MetaDataName => 
            LanguageService.FindResourceString(Constants.FileMetaDataName_FileType);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.FileMetaDataGUID_FileType;

        public override int Order => 4;
        
        private string dirType;
        private string regType;
        private string unknownType;

        public override object GetDataObject(FileBase file) {
            if(file is Directory) {
                return dirType ?? (dirType = LanguageService.FindResourceString(Constants.FileType_Directory));
            }
            else if(file is RegularFile) {
                return regType ?? (regType = LanguageService.FindResourceString(Constants.FileType_RegularFile));
            }
            else {
                return unknownType ?? (unknownType = LanguageService.FindResourceString(Constants.FileType_Unknown));
            }
        }
    }

    [Export(typeof(IPartMetaDataProvider))]
    class PartitionSizeMetaDataProvider : FileSizeMetaDataProviderBase, IPartMetaDataProvider {
        public override int Order => 2;

        public object GetDataObject(Partition file) {
            return file.Size;
        }
    }

    [Export(typeof(IPartMetaDataProvider))]
    class PartitionNameMetaDataProvider : FileNameMetaDataProviderBase, IPartMetaDataProvider {
        public object GetDataObject(Partition part) => part.Name;
    }

    [Export(typeof(IPartMetaDataProvider))]
    class PartitionTypeMetaDataProvider : PartMetaDataProviderBase {
        public override string MetaDataName =>
            LanguageService.FindResourceString(Constants.PartMetaDataName_PartType);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.PartMetaDataGUID_PartType;

        public override int Order => 4;

        public override object GetDataObject(Partition part) {
            return part.PartTypeName;
        }
    }

    

    [Export(typeof(IPartMetaDataProvider))]
    class PartitionStartLBAMetaDataProvider : PartMetaDataProviderBase {
        public override string MetaDataName => LanguageService.FindResourceString(Constants.PartMetaDataName_StartLBA);

        public override Type MetaDataType => typeof(long?);

        public override string GUID => Constants.PartMetaDataGUID_StartLBA;

        public override int Order => 10;

        public override bool AutoConvert => false;

        public override object GetDataObject(Partition part) {
            if(part.Parent is Device device) {
                return device.GetStartLBA(part);
            }
            return null;
        }
    }

    [Export(typeof(IPartMetaDataProvider))]
    class ParitionLastMountTimeMetaDataProvider : PartMetaDataProviderBase {
        public override string MetaDataName => LanguageService.FindResourceString(Constants.PartitionMetaDataName_LastMountTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.PartitionMetaDataGUID_LastMountTime;

        public override int Order => 12;

        public override object GetDataObject(Partition part) {
            var dt = part.GetExtensionTime(Contracts.FileSystem.Constants.PartitionExtendTime_LastMount);
            if(dt == null) {
                return DBNull.Value;
            }
            return dt; 
        }
    }

    [Export( typeof(IFileMetaDataProvider))]
    class FileMTimeMetaDataProvider : FileMetaDataProviderBase {
        public override string MetaDataName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_ModifiedTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.FileMetaDataGUID_ModifiedTime;

        public override int Order => 6;

        public override object GetDataObject(FileBase file) {
            var mTime = (file as IHaveFileTime)?.ModifiedTime;
            if(mTime != null) {
                return mTime.Value;
            }
            return DBNull.Value;
        }
    }
    
    [Export( typeof(IFileMetaDataProvider))]
    class FileATimeMetaDataProvider : FileMetaDataProviderBase {
        public override string MetaDataName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_AccessedTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.FileMetaDataGUID_AccessedTime;

        public override int Order => 8;

        public override object GetDataObject(FileBase file) {
            var accessTime = (file as IHaveFileTime)?.AccessedTime;
            if(accessTime != null) {
                return accessTime.Value;
            }
            return DBNull.Value;
        }

    }
    
    [Export( typeof(IFileMetaDataProvider))]
    class FileCTimeMetaDataProvider : FileMetaDataProviderBase {
        public override string MetaDataName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_CreateTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.FileMetaDataGUID_CreateTime;

        public override int Order => 10;

        public override object GetDataObject(FileBase file) {
            var cTime = (file as IHaveFileTime)?.CreateTime;
            if(cTime != null) {
                return cTime.Value;
            }
            return DBNull.Value;
        }

    }
    
}
