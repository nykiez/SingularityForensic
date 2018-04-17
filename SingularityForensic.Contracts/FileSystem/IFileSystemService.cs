using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
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
        //初始化;
        void Initialize();

        //挂载流;
        IFile MountStream(Stream stream, string name, XElement xElem, IProgressReporter reporter);

        //卸载文件;
        void UnMountFile(IFile file);

        //所有文件;
        //file为对应的文件管理单元,xElem为信息项,为了避免与案件耦合(试用xElem作为信息媒介);
        IEnumerable<(IFile file,XElement xElem)> MountedFiles { get; }
    }

    public class FSService :GenericServiceStaticInstance<IFileSystemService> {

    }

    public static class FileSystemServiceExtensions {
        public static IFile OpenFile(this IFileSystemService fsService,string url) {
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
