using System;
using System.IO;

namespace EventLogger {
    //注册信息日志记录器;
    public static class RegisterLogger {
        public static void WriteLine(string line) {
            var sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Registrylog.txt", true);
            string record = string.Format("{0}-{1}\t{2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), line);
            sw.WriteLine(record);
            sw.Close();
        }
    }
}
