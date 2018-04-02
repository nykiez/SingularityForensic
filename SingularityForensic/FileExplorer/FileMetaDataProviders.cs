using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer {
    [Export(typeof(IFileMetaDataProvider))]
    public class FileNameMetaDataProvider : IFileMetaDataProvider {
        public string MetaDataName => 
            LanguageService.FindResourceString(Constants.FileMetaDataName_Name);

        public Type MetaDataType => typeof(string);

        public string GUID => Constants.FileMetaDataGUID_Name;

        public int Order => 2;

        public object GetDataObject(FileBase file) => file?.Name;
    }

    [Export(typeof(IFileMetaDataProvider))]
    public class FileSizeMetaDataProvider : IFileMetaDataProvider {
        public string MetaDataName => 
            LanguageService.FindResourceString(Constants.FileMetaDataName_Size);

        public Type MetaDataType => typeof(long);

        public string GUID => Constants.FileMetaDataGUID_Size;

        public int Order => 4;

        public object GetDataObject(FileBase file) => file.Size;
    }

    [Export(typeof(IFileMetaDataProvider))]
    public class FileMTimeMetaDataProvider : IFileMetaDataProvider {
        public string MetaDataName =>
            LanguageService.FindResourceString(Constants.FileMetaDataName_ModifiedTime);

        public Type MetaDataType => typeof(DateTime?);

        public string GUID => Constants.FileMetaDataGUID_ModifiedTime;

        public int Order => 6;

        public object GetDataObject(FileBase file) => (file as ITimeable)?.ModifiedTime;
    }



}
