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
        /// <param name="xElem">拓展元素,与案件相关</param>
        void MountFile(IFile file, XElement xElem);

        /// <summary>
        /// 卸载文件;
        /// </summary>
        /// <param name="file"></param>
        void UnMountFile(IFile file);
        
        /// <summary>
        /// 所有文件;
        /// </summary>
        IEnumerable<IMountedUnit> MountedUnits { get; }

        /// <summary>
        /// 从文件系统中根据某个路径获取文件;
        /// </summary>
        /// <param name="path">路径的首个参数应为对应案件文件的GUID,路径分割符("/"或"\")将只能同时出现一次</param>
        /// <returns></returns>
        IFile GetFile(string path);

        /// <summary>
        /// 根据文件获取完整路径;
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string GetPath(IFile file);

        /// <summary>
        /// 获取文件所在的挂载单元根文件(案件相关);
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        IMountedUnit GetOwnMountUnit(IFile file);
    }

    public interface IMountedUnit {
        /// <summary>
        /// file为对应的文件管理单元;
        /// </summary>
        IFile File { get; }
        /// <summary>
        /// xElem为信息项,为了避免与案件模块耦合,使用xElem作为信息媒介;
        /// </summary>
        XElement XElem { get; }
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

            foreach (var unit in fsService.MountedUnits) {
                
                if(unit.XElem.Element(nameof(ICaseEvidence.EvidenceGUID))?.Value == args.FirstOrDefault()) {
                    if(unit.File is IDevice device) {
                        return device.GetFileByUrl(url.Substring(url.IndexOf('/') + 1));
                    }

                    if(unit.File is IHaveFileCollection enumFile)  {
                        return enumFile.GetFileByUrl(url.Substring(url.IndexOf('/') + 1));
                    }
                    
                }
                
            }

            return null;
        }
    }
}
