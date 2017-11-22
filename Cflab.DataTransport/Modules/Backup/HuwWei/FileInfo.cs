using Chloe.Entity;

namespace Cflab.DataTransport.Modules.Backup.HuwWei
{
    [Table("apk_file_info")]
    public class FileInfo
    {
        [Column("gid")]
        public long? Gid { get; set; }

        [Column("uid")]
        public long? Uid { get; set; }

        [Column("permission")]
        public long? Permision { get; set; }

        [Column("file_index")]
        public long? Index { get; set; }

        [Column("file_path")]
        public string Path { get; set; }

        [Column("file_link")]
        public string Link { get; set; }
    }
}
