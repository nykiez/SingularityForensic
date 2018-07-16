using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Local {
    /// <summary>
    /// 本地目录解析单位;
    /// </summary>
    public static class LocalDirParser {
        public static IDirectory GetDirectory(string path,IProgressReporter reporter = null) {
            if(path == null) {
                throw new ArgumentNullException(nameof(path));
            }

            if (!Directory.Exists(path)) {
                return null;
            }

            var di = new DirectoryInfo(path);
            return SearchDir(di,reporter);
        }
        
        private static IDirectory SearchDir(DirectoryInfo dirInfo,IProgressReporter progressReporter) {
            if(dirInfo == null) {
                throw new ArgumentNullException(nameof(dirInfo));
            }

            var thisDir = GetDirectory(dirInfo);

            //检查该文件夹是否处于系统保护状态,如果是将不能继续访问子文件;
            var ctrl = dirInfo.GetAccessControl();
            if (ctrl == null) {
                return thisDir;
            }
            if (ctrl.AreAccessRulesProtected) {
                return thisDir;
            }
            
            //递归搜索子文件夹;
            foreach (var dirInfoNode in dirInfo.GetDirectories()) {
                thisDir.Children.Add(SearchDir(dirInfoNode,progressReporter));
                progressReporter?.ReportProgress(50, dirInfoNode.Name,string.Empty);
            }
            
            foreach (var fileInfoNode in dirInfo.GetFiles()) {
                thisDir.Children.Add(GetRegFileInternal(fileInfoNode));
                progressReporter?.ReportProgress(50, fileInfoNode.Name, string.Empty);
            }

            return thisDir;
        }

        /// <summary>
        /// 根据本地目录信息得到目录结构;
        /// </summary>
        /// <param name="dirInfo"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        private static IDirectory GetDirectory(DirectoryInfo dirInfo) {
            if(dirInfo == null) {
                throw new ArgumentNullException(nameof(dirInfo));
            }

            var dir = FileFactory.CreateDirectory(Constants.DirectoryKey_LocalDir);
            var stoken = dir.GetStoken(Constants.DirectoryKey_LocalDir);
            stoken.TypeGuid = Constants.DirectoryType_LocalDir;
            ApplyFileSystemInfoToStokenBase(dirInfo, stoken);
            stoken.SetInstance(dirInfo, Constants.DirectoryTag_LocalDirectoryInfo);
            return dir;
        }

        /// <summary>
        /// 根据本地文件信息得到常规文件结构;
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        private static IFile GetRegFileInternal(FileInfo fileInfo) {
            if (fileInfo == null) {
                throw new ArgumentNullException(nameof(fileInfo));
            }

            var regFile = FileFactory.CreateRegularFile(Constants.RegularFileKey_LocalRegFile);
            var stoken = regFile.GetStoken(Constants.RegularFileKey_LocalRegFile);
            stoken.TypeGuid = Constants.RegularFileType_LocalRegFile;
            ApplyFileSystemInfoToStokenBase(fileInfo, stoken);
            stoken.SetInstance(fileInfo, Constants.RegularFileTag_LocalFileInfo);
            return regFile;
        }
        
        /// <summary>
        /// 应用本地文件系统信息至FileStoken;
        /// </summary>
        /// <param name="fsInfo"></param>
        /// <param name="stokenBase2"></param>
        private static void ApplyFileSystemInfoToStokenBase(FileSystemInfo fsInfo,FileStokenBase2 stokenBase2) {
            if(fsInfo == null) {
                throw new ArgumentNullException(nameof(fsInfo));
            }

            if(stokenBase2 == null) {
                throw new ArgumentNullException(nameof(stokenBase2));
            }

            stokenBase2.AccessedTime = fsInfo.LastAccessTime;
            stokenBase2.ModifiedTime = fsInfo.LastWriteTime;
            stokenBase2.CreateTime = fsInfo.CreationTime;

            stokenBase2.Name = fsInfo.Name;
        }
    }
}
