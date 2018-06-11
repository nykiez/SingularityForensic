using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// 文件服务;
    /// </summary>
    public interface IFileService {
        /// <summary>
        /// 初始化;
        /// </summary>
        void Initialize();
        /// <summary>
        /// 获取文件流;
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Stream GetInputStream(IFile file);

    }


    public class FileService : GenericServiceStaticInstance<IFileService> {
        public static Stream GetInputStream(IFile file) => Current.GetInputStream(file);
    }

}
