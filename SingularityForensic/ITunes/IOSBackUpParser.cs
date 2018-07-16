using CDFC.Util.PInvoke;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace SingularityForensic.ITunes {
    /// <summary>
    /// ITunes备份管理器;
    /// </summary>
    public class ITunesBackUpManager {
        /// <summary>
        /// 实际存储的内容;
        /// </summary>
        public IDirectory Directory { get; internal set; }
        public StIOSBasicInfo? BasicInfo { get; internal set; }
    }
    
    public static partial class IOSBackUpParser {  
        private const string MBDBName = "Manifest.mbdb";
        private const string DBName = "Manifest.db";
        private const string InfoPlistName = "Info.plist";

        //开始解析备份文件;
        [HandleProcessCorruptedStateExceptions]
        public static ITunesBackUpManager DoParse(string backUpPath) {
            var di = new DirectoryInfo(backUpPath);
            if (!di.Exists) {
                throw new DirectoryNotFoundException(backUpPath);
            }


            var manager = new ITunesBackUpManager();
            manager.Directory = GetDirectory(di);
            manager.BasicInfo = GetBasicInfo(di);
            
            return manager;
        }

        /// <summary>
        /// 获取文件列表;
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        private static IDirectory GetDirectory(DirectoryInfo di) {
            var szPtr = Marshal.StringToHGlobalAnsi(di.FullName);
            try {
                var ptr = IntPtr.Zero;
                if (di.GetFiles().FirstOrDefault(p => p.Name == MBDBName) != null) {
                    ptr = parse(szPtr, 1);
                }
                else if (di.GetFiles().FirstOrDefault(p => p.Name == DBName) != null) {
                    ptr = parse(szPtr, 1000);
                }
                var direct = FileFactory.CreateDirectory(Constants.DirectoryKey_ITunesBackup);
                var dirStoken = direct.GetStoken(Constants.DirectoryKey_ITunesBackup);
                dirStoken.TypeGuid = Constants.DirectoryType_ITunesBackUpDir;
                dirStoken.Name = di.Name;
#if DEBUG
                int index = 0;
#endif
                while (ptr != IntPtr.Zero) {
                    var st = ptr.GetStructure<IOSFileStruct>();
                    
                    var regFile = FileFactory.CreateRegularFile(Constants.RegularFileKey_ITunesBackUp);
                    var regFileStoken = regFile.GetStoken(Constants.RegularFileKey_ITunesBackUp);
                    regFileStoken.TypeGuid = Constants.RegularFileKey_ITunesBackUp;
                    regFileStoken.SetInstance<IOSFileStruct?>(st, Constants.RegularFileTag_ITunesBackUp);

                    try {
                        regFileStoken.Name = Path.GetFileName(st.PhonePath);
                        regFileStoken.CreateTime = st.CreateTime;
                        regFileStoken.ModifiedTime = st.ModifiedTime;
                        regFileStoken.AccessedTime = st.AccessTime;
                        //if (File.Exists(st.LocalPath)) {
                        //    var fileInfo = new FileInfo(st.LocalPath);
                        //    regFileStoken.Size = fileInfo.Length;
                        //}
                        
                    }
                    catch (Exception ex) {
                        LoggerService.WriteCallerLine(ex.Message);
                    }

                    direct.Children.Add(regFile);
                    ptr = st.next;

#if DEBUG
                    index++;
                    if(index >= 20) {
                        break;
                    }
#endif
                }

                return direct;
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                return null;
            }
            finally {
                parse_exit();
                Marshal.FreeHGlobal(szPtr);
            }
        }
         
        /// <summary>
        /// 获得基本信息结构体;
        /// </summary>
        /// <returns></returns>
        [HandleProcessCorruptedStateExceptions]
        private static StIOSBasicInfo? GetBasicInfo(DirectoryInfo diInfo) {
            try {
                //查看是否存在所需文件;
                if (diInfo.GetFiles().FirstOrDefault(p => p.Name == InfoPlistName) == null) {
                    LoggerService.WriteCallerLine($"{InfoPlistName} is null.");
                    return null;
                }

                var infoListPath = $"{diInfo.FullName}\\Info.plist";
                var szFilePtr = Marshal.StringToHGlobalAnsi(infoListPath);
                var plPtr = GetPlist(szFilePtr);
                if(plPtr == IntPtr.Zero) {
                    return null;
                }

                var pList = plPtr.GetStructure<StIOSBasicInfo>();

                Marshal.FreeHGlobal(szFilePtr);
                DeletePlist(plPtr);
                return pList;
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                return null;
            }

        }
    }

    public static partial class IOSBackUpParser {
        [DllImport("iosparse.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr parse(IntPtr szDir, int nType);

        [DllImport("iosparse.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void parse_exit();

        [DllImport("PListCpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GetPlist(IntPtr pszFilePath);

        [DllImport("PListCpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void DeletePlist(IntPtr stPlist);
    }
}
