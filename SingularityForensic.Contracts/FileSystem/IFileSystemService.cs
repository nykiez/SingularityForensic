using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.FileSystem {
    public interface IFileSystemService {
        /// <summary>
        /// 初始化;
        /// </summary>
        void Initialize();

        /// <summary>
        /// 挂载流;
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="name"></param>
        /// <param name="xElem"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        IFile MountStream(Stream stream, string name, ITextInstanceExtensible ext, IProgressReporter reporter);

        /// <summary>
        /// 挂载现有的文件;
        /// </summary>
        /// <param name="file">在外部构建的文件</param>
        void MountFile(IFile file, XElement xElem);

        /// <summary>
        /// 卸载文件;
        /// </summary>
        /// <param name="file"></param>
        void UnMountFile(IFile file);
        
        /// <summary>
        /// 所有文件;
        /// file为对应的文件管理单元,xElem为信息项,为了避免与案件模块耦合,使用xElem作为信息媒介;
        /// </summary>
        IEnumerable<ITextInstanceExtensible> MountedEntities { get; }
    }

    public class FileSystemService :GenericServiceStaticInstance<IFileSystemService> {

    }

    public static class FileSystemServiceExtensions {
        public static IFile GetFile(this IFileSystemService fsService,string url) {
            if (string.IsNullOrEmpty(url)) {
                return null;
            }
            if(fsService == null) {
                return null;
            }
            
            url = url.Replace('\\', '/');

            var args = url.Split('/');
            if (args.Count() <= 1) {
                LoggerService.Current?.WriteCallerLine($"Invalid args count");
                return null;
            }

            foreach (var ext in fsService.MountedEntities) {
                if (ext[Contracts.Common.Constants.EvidenceGUID] != args.FirstOrDefault()) {
                    continue;
                }
                
                if (file is IDevice device) {
                    return device.GetFileByUrl(url.Substring(url.IndexOf('/') + 1));
                }

                if (file is IHaveFileCollection enumFile) {
                    return enumFile.GetFileByUrl(url.Substring(url.IndexOf('/') + 1));
                }
            }

            return null;
        }
    }
}
