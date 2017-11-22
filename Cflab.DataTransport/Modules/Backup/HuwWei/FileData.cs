using Chloe.Entity;

namespace Cflab.DataTransport.Modules.Backup.HuwWei
{
    /// <summary>
    /// 文件数据
    /// </summary>
    [Table("apk_file_data")]
    public class FileData
    {
        /// <summary>
        /// 数据索引
        /// </summary>
        [Column("data_index")]
        public long? DataIndex { get; set; }

        /// <summary>
        /// 文件索引
        /// </summary>
        [Column("file_index")]
        public long FileIndex { get; set; }

        /// <summary>
        /// 本条记录对应的文件长度
        /// </summary>
        [Column("file_length")]
        public long Length { get; set; }

        /// <summary>
        /// 文件数据对应的字节数组
        /// </summary>
        [Column("file_data")]
        public byte[] Data { get; set; }
    }
}
