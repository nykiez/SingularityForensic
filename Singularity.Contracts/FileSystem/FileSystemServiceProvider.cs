using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using Singularity.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.Contracts.FileSystem {
    public interface IFileSystemServiceProvider {
        /// <summary>
        /// 打开某个文件;
        /// </summary>
        /// <param name="evidence">证据</param>
        /// <param name="fileName"></param>
        /// GUID为案件文件的GUID;
        /// <example>{GUID}/"" 空字符串:可能返回设备</example>
        /// <example>{GUID}/"0" 返回第一个分区</example>
        /// <example>{GUID}/"0/file.ext" 返回第一个分区下的一个叫做file.ext的文件</example>
        /// <returns>{GUID}/</returns>
        FSFile OpenFile(string fileName);
    }

    [Export(typeof(IFileSystemServiceProvider))]
    public class FileSystemServiceProvider : IFileSystemServiceProvider {
        public FSFile OpenFile(string fileName) {
            if (string.IsNullOrEmpty(fileName)) {
                return null;
            }

            var csService = ServiceProvider.Current.GetInstance<ICaseService>();
            if(csService?.CurrentCase?.CaseEvidences == null) {
                return null;
            }

            fileName = fileName.Replace('\\', '/');

            var args = fileName.Split('/');
            if(args.Count() <= 1) {
                EventLogger.Logger.WriteCallerLine($"Invalid args count");
                return null;
            }

            foreach (var evi in csService.CurrentCase.CaseEvidences) {
                if (evi is IHaveData<Device> haveDevice) {
                    if (evi.GUID == args.FirstOrDefault()) {
                        IFile file = null;
                        if ((file = haveDevice.Data.GetFileByUrl(fileName.Substring(fileName.IndexOf('/') + 1))) != null) {
                            return new FSFile(file);
                        }
                    }
                }
            }

            return null;
        }
    }
}
