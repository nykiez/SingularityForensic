using CDFC.Util;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Converters;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SingularityForensic.FileExplorer {

    abstract class FileNameMetaDataProviderBase : FileMetaDataProvider {
        public override string DisplayName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_Name);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.FileMetaDataGUID_Name;

        public override int Order => 2;

        public override object GetMetaData(IFile file) => file?.Name;
    }

    abstract class FileSizeMetaDataProviderBase : FileMetaDataProvider {
        public override string DisplayName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_Size);

        public override Type MetaDataType => typeof(long?);

        public override string GUID => Constants.FileMetaDataGUID_Size;

        public override object GetMetaData(IFile file) => file.Size;

        public override IValueConverter Converter => ByteSizeToSizeConverter.StaticInstance;

        public override DataTemplate CellTemplate => null;
    }

    /// <summary>
    /// 文件名元数据提供器;
    /// </summary>
    [Export(typeof(IFileMetaDataProvider))]
    class FileNameMetaDataProvider : FileNameMetaDataProviderBase {
        private DataTemplate _cellTemplate;
        public override DataTemplate CellTemplate {
            get {
                if (_cellTemplate == null) {
                    _cellTemplate = new DataTemplate();
                    var fef = new FrameworkElementFactory(typeof(StackPanel));
                    fef.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

                    var iconFactory = new FrameworkElementFactory(typeof(Image));
                    var iconBinding = new Binding {
                        Path = new PropertyPath(nameof(IFileRowProxy<IFile>.File)),
                        Converter = FileTypeToIconConverter.StaticInstance
                    };
                    iconFactory.SetBinding(Image.SourceProperty, iconBinding);
                    iconFactory.SetValue(Image.WidthProperty, 17.0);

                    var chkFactory = new FrameworkElementFactory(typeof(CheckBox));
                    var chkBinding = new Binding {
                        Path = new PropertyPath(nameof(IFileRowProxy<IFile>.IsChecked)),
                        Mode = BindingMode.TwoWay
                    };
                    chkFactory.SetBinding(CheckBox.IsCheckedProperty, chkBinding);
                    chkFactory.SetValue(CheckBox.IsThreeStateProperty, false);
                    chkFactory.SetValue(CheckBox.PaddingProperty, new Thickness(0, 0, 0, 0));

                    var txbFactory = new FrameworkElementFactory(typeof(TextBlock));
                    var nameBinding = new Binding {
                        Path = new PropertyPath(this.GUID)
                    };
                    txbFactory.SetBinding(TextBlock.TextProperty, nameBinding);

                    fef.AppendChild(chkFactory);
                    fef.AppendChild(iconFactory);
                    fef.AppendChild(txbFactory);

                    _cellTemplate.VisualTree = fef;
                    _cellTemplate.Seal();
                }
                return _cellTemplate;
            }
        }

        public class FileTypeToIconConverter : GenericStaticInstance<FileTypeToIconConverter>, IValueConverter {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
                if (value is IFile file) {
                    if (file is IDirectory) {
                        return IconResources.DirectoryRowIcon;
                    }
                    else if (file is IRegularFile) {
                        return IconResources.RegFileUnitIcon;
                    }
                }
                return null;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
                throw new NotImplementedException();
            }
        }

        public class FileToCheckedConverter : GenericStaticInstance<FileToCheckedConverter>, IValueConverter {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
                if (value is IFile file) {
                    return true;
                }
                return false;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
                return null;
            }
        }
    }


    [Export(typeof(IFileMetaDataProvider))]
    class FileDeletedMetaDataProvider : FileMetaDataProvider {
        public override string DisplayName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_Deleted);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.FileMetaDataGUID_Deleted;

        public override int Order => 12;

        public override object GetMetaData(IFile file) {
            if (file is IDeletable deletable) {
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
        public override string DisplayName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_FileType);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.FileMetaDataGUID_FileType;

        public override int Order => 4;

        private string dirType;
        private string regType;
        private string unknownType;

        public override object GetMetaData(IFile file) {
            if (file is IDirectory) {
                return dirType ?? (dirType = LanguageService.FindResourceString(Constants.FileType_Directory));
            }
            else if (file is IRegularFile) {
                return regType ?? (regType = LanguageService.FindResourceString(Constants.FileType_RegularFile));
            }
            else {
                return unknownType ?? (unknownType = LanguageService.FindResourceString(Constants.FileType_Unknown));
            }
        }
    }

    [Export(typeof(IPartitionMetaDataProvider))]
    class PartitionSizeMetaDataProvider : FileSizeMetaDataProviderBase, IPartitionMetaDataProvider {
        public override int Order => 4;

        public object GetMetaData(IPartition file) {
            return file.Size;
        }

        public override IValueConverter Converter => ByteSizeToSizeConverter.StaticInstance;
    }

    [Export(typeof(IPartitionMetaDataProvider))]
    class PartitionNameMetaDataProvider : FileNameMetaDataProviderBase, IPartitionMetaDataProvider {
        public object GetMetaData(IPartition part) {
            return FileExtensions.GetPartFixAndName(part);
        }

        public override int Order => 2;
    }

    [Export(typeof(IPartitionMetaDataProvider))]
    class PartitionTypeMetaDataProvider : PartitionMetaDataProvider {
        public override string DisplayName =>
            LanguageService.FindResourceString(Constants.PartMetaDataName_PartType);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.PartMetaDataGUID_PartType;

        public override int Order => 4;

        public override object GetMetaData(IPartition part) {
            return part.PartType?.PartTypeName;
        }
    }

    [Export(typeof(IPartitionMetaDataProvider))]
    class PartitionStartLBAMetaDataProvider : PartitionMetaDataProvider {
        public override string DisplayName => LanguageService.FindResourceString(Constants.PartMetaDataName_StartLBA);

        public override Type MetaDataType => typeof(long?);

        public override string GUID => Constants.PartMetaDataGUID_StartLBA;

        public override int Order => 10;

        public override object GetMetaData(IPartition part) {
            if (part.Parent is IDevice device) {
                return device.GetStartLBA(part);
            }
            return null;
        }
    }

    [Export(typeof(IPartitionMetaDataProvider))]
    class ParitionLastMountTimeMetaDataProvider : PartitionMetaDataProvider {
        public override string DisplayName => LanguageService.FindResourceString(Constants.PartitionMetaDataName_LastMountTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.PartitionMetaDataGUID_LastMountTime;

        public override int Order => 12;

        public override object GetMetaData(IPartition part) {
            var dt = part.GetExtensionTime(Contracts.FileSystem.Constants.PartitionExtendTime_LastMount);
            if (dt == null) {
                return DBNull.Value;
            }
            return dt;
        }
    }

    [Export(typeof(IFileMetaDataProvider))]
    class FileMTimeMetaDataProvider : FileMetaDataProvider {
        public override string DisplayName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_ModifiedTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.FileMetaDataGUID_ModifiedTime;

        public override int Order => 6;

        public override object GetMetaData(IFile file) {
            var mTime = (file as IHaveFileTime)?.ModifiedTime;
            if (mTime != null) {
                return mTime.Value;
            }
            return DBNull.Value;
        }
    }

    [Export(typeof(IFileMetaDataProvider))]
    class FileATimeMetaDataProvider : FileMetaDataProvider {
        public override string DisplayName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_AccessedTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.FileMetaDataGUID_AccessedTime;

        public override int Order => 8;

        public override object GetMetaData(IFile file) {
            var accessTime = (file as IHaveFileTime)?.AccessedTime;
            if (accessTime != null) {
                return accessTime.Value;
            }
            return DBNull.Value;
        }

    }

    [Export(typeof(IFileMetaDataProvider))]
    class FileCTimeMetaDataProvider : FileMetaDataProvider {
        public override string DisplayName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_CreateTime);

        public override Type MetaDataType => typeof(DateTime);

        public override string GUID => Constants.FileMetaDataGUID_CreateTime;

        public override int Order => 10;

        public override object GetMetaData(IFile file) {
            var cTime = (file as IHaveFileTime)?.CreateTime;
            if (cTime != null) {
                return cTime.Value;
            }
            return DBNull.Value;
        }

    }

    [Export(typeof(IFileMetaDataProvider))]
    class FilePathMetaDataProvider : FileMetaDataProvider {
        public override string DisplayName => LanguageService.FindResourceString(Constants.FileMetaDataName_Path);

        public override Type MetaDataType => typeof(string);

        public override string GUID => Constants.FileMetaDataGUID_Path;

        public override int Order => 12;

        public override bool ShowDistinctFilters => false;

        public override object GetMetaData(IFile file) {
            if (file == null) {
                return null;
            }

            return file.GetFullFileName();

        }
    }

    //[Export(typeof(IFileMetaDataProvider))]
    //class FileHashMetaDataProvider : FileMetaDataProvider {

    //}
}
