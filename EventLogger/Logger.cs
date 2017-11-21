using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace EventLogger {
    public static  class Logger {
        private static object locker = new object();
        //public static void WriteLineWithCaller(string line) {
        //    WriteLine($"{callerName}->{line}");
        //}

        public static void WriteLine(string line) {
            lock (locker) {
                var sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/log.txt", true);
                string record = $"{DateTime.Now.ToLongTimeString()}-{DateTime.Now.ToLongDateString()}\t{line}";
                sw.WriteLine(record);
                sw.Close();
            }
        }

        public static void WriteCallerLine(string line, [CallerMemberName]string callerName = null) {
            StackFrame st = new StackFrame(1);
            var sm = st.GetMethod();
            WriteLine($"{sm?.ReflectedType?.FullName} -> {callerName} : {line}");
        }

            //public static void WriteLineWithStack(string line,int skipFrameCount [CallerMemberName]string callerName = null,bool writeStack = true) {

            //}

        public static void Write(string word) {
            lock (locker) {
                var sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/log.txt", true);
                sw.Write(word);
                sw.Close();
            }
        }
    }
}
