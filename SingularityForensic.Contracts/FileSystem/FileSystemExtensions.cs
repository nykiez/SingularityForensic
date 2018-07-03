using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
     public static class FileSystemExtensions {
        /// <summary>
        /// 从文件系统中根据某个路径获取文件;
        /// </summary>
        /// <param name="fsService"></param>
        /// <param name="path">路径,路径分割符("/"或"\")将只能同时出现一次</param>
        /// <returns></returns>
        public static IFile GetFile(this IFileSystemService fsService, string path) {
            if (fsService == null) {
                throw new ArgumentNullException(nameof(fsService));
            }
            if (string.IsNullOrEmpty(path)) {
                throw new ArgumentNullException(nameof(path));
            }

            path = path.Replace('/', Constants.Path_SplitChar);
            var pathParams = path.Split(Constants.Path_SplitChar);
            if(pathParams.Length < 1) {
                throw new ArgumentException($"Invalid {nameof(path)}.");
            }
            var mountedUnit = fsService.MountedUnits.FirstOrDefault(p => p.XElem.GetXElemValue(Contracts.Common.Constants.EvidenceGUID) == pathParams[0]);
            if (!(mountedUnit.File is IHaveFileCollection haveFileCollection)) { 
                return null;
            }
            
            return haveFileCollection.GetFileByUrlArgs(pathParams.Skip(1).ToArray());
        }
    }
}
