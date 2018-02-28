using CDFC.Parse.Contracts;
using System;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IFileRow : IHavePreviewSource {
        event EventHandler<bool> CheckChanged;
        bool Checked { get; set; }
        void SetChecked(bool val);                       //设定选定状态而不触发事件;

        string FileName { get; }                           //文件名;

        //private string _fileType;
        //文件类型;
        string FileType { get; }

        string FilePermission { get; }
        string GID { get; }

        //标识ID;
        string UID { get; }

        long FileSize { get; }                             //文件大小;
        bool? Deleted { get; }
        FileType RowType { get; }         //行类型;
        DateTime? ModifiedTime { get; } //最后修改时间;
        DateTime? AccessedTime { get; }                         //最后访问时间;        
        DateTime? CreateTime { get; }                           //最后创建时间;    

        string LastMountTime { get; }

        string LastWriteTime { get; }

        //起始扇区;
        long StartSec { get; }

        long EndSec { get; }

        //文件路径
        string FilePath { get; }

        int? PartitionIndex { get; set; }

        /// <summary>
        /// 本地路径(若已经缓存);
        /// </summary>
        string LocalPath { get; set; }

    }

    public interface IFileRow<out TFile> : IFileRow where TFile : IFile {
        TFile File { get; }
    }
}
