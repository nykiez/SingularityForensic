using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Converters;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SingularityForensic.FileExplorer {
   
    abstract class FileNameMetaDataProviderBase : FileMetaDataProvider {
        public override string MetaDataName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_Name);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.FileMetaDataGUID_Name;

        public override int Order => 2;

        public override object GetMetaData(FileBase file) => file?.Name;
    }

    
    abstract class FileSizeMetaDataProviderBase : IFileMetaDataProvider {
        public string MetaDataName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_Size);

        public Type MetaDataType => typeof(long?);

        public string GUID => Constants.FileMetaDataGUID_Size;

        public abstract int Order { get; }

        public object GetMetaData(FileBase file) => file.Size;

        public virtual IValueConverter Converter => ByteSizeToSizeConverter.StaticInstance;

        public virtual DataTemplate CellTemplate => null;
    }

    /// <summary>
    /// 文件名元数据提供器;
    /// </summary>
    [Export(typeof(IFileMetaDataProvider))]
    class FileNameMetaDataProvider : FileNameMetaDataProviderBase {
        
    }

    

    [Export(typeof(IFileMetaDataProvider))]
    class FileDeletedMetaDataProvider : FileMetaDataProvider {
        public override string MetaDataName => 
            LanguageService.FindResourceString(Constants.FileMetaDataName_Deleted);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.FileMetaDataGUID_Deleted;

        public override int Order => 12;

        public override object GetMetaData(FileBase file) {
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
    class FileTypeMetaDataProvider : FileMetaDataProvider {
        public override string MetaDataName => 
            LanguageService.FindResourceString(Constants.FileMetaDataName_FileType);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.FileMetaDataGUID_FileType;

        public override int Order => 4;
        
        private string dirType;
        private string regType;
        private string unknownType;

        public override object GetMetaData(FileBase file) {
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

    [Export(typeof(IPartitionMetaDataProvider))]
    class PartitionSizeMetaDataProvider : FileSizeMetaDataProviderBase, IPartitionMetaDataProvider {
        public override int Order => 2;

        public object GetMetaData(Partition file) {
            return file.Size;
        }

        public override IValueConverter Converter => ByteSizeToSizeConverter.StaticInstance;
    }

    [Export(typeof(IPartitionMetaDataProvider))]
    class PartitionNameMetaDataProvider : FileNameMetaDataProviderBase, IPartitionMetaDataProvider {
        public object GetMetaData(Partition part) => part.Name;
    }

    [Export(typeof(IPartitionMetaDataProvider))]
    class PartitionTypeMetaDataProvider : PartitionMetaDataProvider {
        public override string MetaDataName =>
            LanguageService.FindResourceString(Constants.PartMetaDataName_PartType);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.PartMetaDataGUID_PartType;

        public override int Order => 4;

        public override object GetMetaData(Partition part) {
            return part.PartTypeName;
        }
    }

    

    [Export(typeof(IPartitionMetaDataProvider))]
    class PartitionStartLBAMetaDataProvider : PartitionMetaDataProvider {
        public override string MetaDataName => LanguageService.FindResourceString(Constants.PartMetaDataName_StartLBA);

        public override Type MetaDataType => typeof(long?);

        public override string GUID => Constants.PartMetaDataGUID_StartLBA;

        public override int Order => 10;
        
        public override object GetMetaData(Partition part) {
            if(part.Parent is Device device) {
                return device.GetStartLBA(part);
            }
            return null;
        }
    }

    [Export(typeof(IPartitionMetaDataProvider))]
    class ParitionLastMountTimeMetaDataProvider : PartitionMetaDataProvider {
        public override string MetaDataName => LanguageService.FindResourceString(Constants.PartitionMetaDataName_LastMountTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.PartitionMetaDataGUID_LastMountTime;

        public override int Order => 12;

        public override object GetMetaData(Partition part) {
            var dt = part.GetExtensionTime(Contracts.FileSystem.Constants.PartitionExtendTime_LastMount);
            if(dt == null) {
                return DBNull.Value;
            }
            return dt; 
        }
    }

    [Export( typeof(IFileMetaDataProvider))]
    class FileMTimeMetaDataProvider : FileMetaDataProvider {
        public override string MetaDataName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_ModifiedTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.FileMetaDataGUID_ModifiedTime;

        public override int Order => 6;

        public override object GetMetaData(FileBase file) {
            var mTime = (file as IHaveFileTime)?.ModifiedTime;
            if(mTime != null) {
                return mTime.Value;
            }
            return DBNull.Value;
        }
    }
    
    [Export( typeof(IFileMetaDataProvider))]
    class FileATimeMetaDataProvider : FileMetaDataProvider {
        public override string MetaDataName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_AccessedTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.FileMetaDataGUID_AccessedTime;

        public override int Order => 8;

        public override object GetMetaData(FileBase file) {
            var accessTime = (file as IHaveFileTime)?.AccessedTime;
            if(accessTime != null) {
                return accessTime.Value;
            }
            return DBNull.Value;
        }

    }
    
    [Export(typeof(IFileMetaDataProvider))]
    class FileCTimeMetaDataProvider : FileMetaDataProvider {
        public override string MetaDataName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_CreateTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.FileMetaDataGUID_CreateTime;

        public override int Order => 10;

        public override object GetMetaData(FileBase file) {
            var cTime = (file as IHaveFileTime)?.CreateTime;
            if(cTime != null) {
                return cTime.Value;
            }
            return DBNull.Value;
        }

    }

    //[Export(typeof(IFileMetaDataProvider))]
    //class FileImgMetaDataProvider : FileMetaDataProviderBase {
    //    public override string MetaDataName => "测试";

    //    public override Type MetaDataType => typeof(BitmapImage);

    //    public override string GUID => "dad";

    //    public override int Order => 1;

    //    public override object GetDataObject(FileBase file) {
    //        return new BitmapImage(IconResources.DirectoryUnitIcon);
    //    }
    //}
}
