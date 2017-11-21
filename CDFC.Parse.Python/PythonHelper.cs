using System;
using System.Diagnostics;

namespace CDFC.Parse.Python {
    public static class PythonHelper {
        public const string PythonName = "python.exe";
        public const string PythonPath = "Python34/";

        /// <summary>
        /// 执行Python；
        /// </summary>
        /// <param name="pyName"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Process GetProcess(string pyName, string arg, UriKind pyKind = UriKind.Relative) {
            var stInfo = GetProcessStartInfo(pyName, arg, pyKind);
            return new Process() {
                StartInfo = stInfo
            };
        }

        public static ProcessStartInfo GetProcessStartInfo(string pyName,string arg,UriKind pyKind = UriKind.Relative) {
            var stInfo = new ProcessStartInfo {
                //Arguments = $"\"D:/C# Console/Python/Pys/Pys/Git/{pyName}\" \"{arg}\"",
                Arguments = $"\"{AppDomain.CurrentDomain.BaseDirectory}/Pys/Git/{pyName}\" \"{arg}\" ",
                FileName = $"{AppDomain.CurrentDomain.BaseDirectory}/{PythonPath}{PythonName}",

                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            return stInfo;
        }

        public static void ShellExecute(string arg,string pyname = null) {

        }
    }
}
