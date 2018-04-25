using SingularityForensic.Contracts.FileSystem;
using System;

namespace SingularityForensic.FileSystem {
    /// <summary>
    /// 正常文件入口
    /// </summary>
    [Serializable]
    public class RegularFile : BlockGroupedFileBase<RegularFileStoken>, IRegularFile {
        /// <summary>
        /// 常规文件构造方法;
        /// </summary>
        /// <param name="parent"></param>
        public RegularFile(string key) : base(key) {

        }
    }
}
