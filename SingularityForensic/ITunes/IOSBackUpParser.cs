using CDFC.Util.PInvoke;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.ITunes {
    /// <summary>
    /// ITunes备份管理器;
    /// </summary>
    public class ITunesBackUpManager {
        /// <summary>
        /// 实际存储的内容;
        /// </summary>
        public IDirectory Directory { get; internal set; }
        public IOSBasicStruct? BasicInfo { get; internal set; }
    }
    
    public static partial class IOSBackUpParser {  
        private const string MBDBName = "Manifest.mbdb";
        private const string DBName = "Manifest.db";

        //开始解析备份文件;
        [HandleProcessCorruptedStateExceptions]
        public static ITunesBackUpManager DoParse(string backUpPath) {
            var di = new DirectoryInfo(backUpPath);
            if (!di.Exists) {
                throw new DirectoryNotFoundException(backUpPath);
            }


            var manager = new ITunesBackUpManager();
            var fileList = new List<IOSFileStruct>();

            var szPtr = Marshal.StringToHGlobalAnsi(di.FullName);
            try {
                var ptr = IntPtr.Zero;
                if (di.GetFiles().FirstOrDefault(p => p.Name == MBDBName) != null) {
                    ptr = parse(szPtr, 1);
                }
                else if (di.GetFiles().FirstOrDefault(p => p.Name == DBName) != null) {
                    ptr = parse(szPtr, 1000);
                }

                while (ptr != IntPtr.Zero) {
                    var st = ptr.GetStructure<IOSFileStruct>();
                    ptr = st.next;
                    fileList.Add(st);
                }
                //manager.Files = fileList;
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
            finally {
                parse_exit();
                Marshal.FreeHGlobal(szPtr);
            }

            return manager;
        }

        /// <summary>
        /// 获得基本信息结构体;
        /// </summary>
        /// <returns></returns>
        [HandleProcessCorruptedStateExceptions]
        public static IOSBasicStruct? GetBasicInfo(DirectoryInfo diInfo) {
            try {
                //查看是否存在所需文件;
                if (diInfo.GetFiles().FirstOrDefault(p => p.Name == "Info.plist") != null) {
                    var infoListPath = $"{diInfo.FullName}\\Info.plist";
                    var szFilePtr = Marshal.StringToHGlobalAnsi(infoListPath);
                    var plPtr = GetPlist(szFilePtr);
                    var pList = plPtr.GetStructure<IOSBasicStruct>();

                    Marshal.FreeHGlobal(szFilePtr);
                    DeletePlist(plPtr);
                    return pList;
                }
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            return null;
        }
    }

    public partial class IOSBackUpParser {
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
