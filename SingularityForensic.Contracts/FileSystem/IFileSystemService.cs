using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
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
        IFile MountStream(Stream stream, string name, XElement xElem, IProgressReporter reporter);

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
        IEnumerable<(IFile file,XElement xElem)> MountedFiles { get; }
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

            foreach (var (file, xElem) in fsService.MountedFiles) {
                
                if(xElem.Element(nameof(ICaseEvidence.EvidenceGUID))?.Value == args.FirstOrDefault()) {
                    if(file is IDevice device) {
                        return device.GetFileByUrl(url.Substring(url.IndexOf('/') + 1));
                    }

                    if(file is IHaveFileCollection enumFile)  {
                        return enumFile.GetFileByUrl(url.Substring(url.IndexOf('/') + 1));
                    }
                    
                }
                
            }

            return null;
        }
    }
}
