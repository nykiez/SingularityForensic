using CDFC.Parse.ITunes.DeviceObjects;
using CDFC.Parse.ITunes.Models;
using CDFC.Util.PInvoke;
using EventLogger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace CDFC.Parse.ITunes {
    /// <summary>
    /// ITunes备份解析器;
    /// </summary>
    public partial class IOSBackUpParser {
        public IOSBackUpParser(string backUpPath) {
            DirectoryInfo di = new DirectoryInfo(backUpPath);
            if (!di.Exists) {
                throw new DirectoryNotFoundException(backUpPath);
            }
            _directInfo = di;
        }

        private DirectoryInfo _directInfo;

        //开始解析备份文件;
        [HandleProcessCorruptedStateExceptions]
        public List<IOSBackUpFile> DoParse() {
            var szPtr = Marshal.StringToHGlobalAnsi(_directInfo.FullName);
            var files = new List<IOSBackUpFile>();
            try {
                var ptr = IntPtr.Zero;
                if(_directInfo.GetFiles().FirstOrDefault(p => p.Name == "Manifest.mbdb") != null){
                    ptr = parse(szPtr,1);
                }
                else if (_directInfo.GetFiles().FirstOrDefault(p => p.Name == "Manifest.db") != null){
                    ptr = parse(szPtr, 1000);
                }
                
                while (ptr != IntPtr.Zero) {
                    var st = ptr.GetStructure<IOSFileStruct>();
                    var file = new IOSBackUpFile(st);
                    files.Add(file);
                    ptr = st.next;
                }
            }
            catch (Exception ex) {
                Logger.WriteCallerLine(ex.Message);
            }
            finally {
                parse_exit();
                Marshal.FreeHGlobal(szPtr);
            }

            return files;
        }

        /// <summary>
        /// 获得基本信息结构体;
        /// </summary>
        /// <returns></returns>
        [HandleProcessCorruptedStateExceptions]
        public IOSBasicStruct? GetBasicInfo() {
            try {
                //查看是否存在所需文件;
                if (_directInfo.GetFiles().FirstOrDefault(p => p.Name == "Info.plist") != null) {
                    var infoListPath = $"{_directInfo.FullName}\\Info.plist";
                    var szFilePtr = Marshal.StringToHGlobalAnsi(infoListPath);
                    var plPtr = GetPlist(szFilePtr);
                    var pList = plPtr.GetStructure<IOSBasicStruct>();

                    Marshal.FreeHGlobal(szFilePtr);
                    DeletePlist(plPtr);
                    return pList;
                }
            }
            catch (Exception ex) {
                Logger.WriteCallerLine(ex.Message);
            }

            return null;
        }
    }

    public partial class IOSBackUpParser {
        [DllImport("iosparse.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr parse(IntPtr szDir,int nType);

        [DllImport("iosparse.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void parse_exit();

        [DllImport("PListCpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GetPlist(IntPtr pszFilePath);

        [DllImport("PListCpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void DeletePlist(IntPtr stPlist);
    }
}
