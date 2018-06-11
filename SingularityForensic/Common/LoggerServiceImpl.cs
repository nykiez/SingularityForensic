using EventLogger;
using SingularityForensic.Contracts.Common;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SingularityForensic.Common {
    [Export(typeof(ILoggerService))]
    class LoggerServiceImpl : ILoggerService {
        public void WriteCallerLine(string msg,[CallerMemberName] string callerName = null) {
            Logger.WriteCallerLine(msg, callerName);
        }

        public void WriteException(Exception ex, [CallerMemberName] string callerName = null) {
            Logger.WriteCallerLine($"{nameof(Exception)}:{ex.Message}");
            Logger.WriteCallerLine($"{nameof(ex.StackTrace)}:{ex.StackTrace}");
            var ie = ex.InnerException;
            while(ie != null) {
                Logger.WriteLine($"\t{nameof(Type)}:{ie.GetType()}\t{nameof(ie.Message)}:{ie.Message}");
                //if(ie is System.ComponentModel.Composition.CompositionException ce) {
                //    foreach (var err in ce.Errors) {
                //        Logger.WriteLine(err.Description);
                //    }
                //}
                
                ie = ie.InnerException;
            }
        }

        public void WriteLine(string msg) {
            Logger.WriteLine(msg);
        }

        public void WriteStack(string msg, [CallerMemberName] string callerName = null) {
            var st = new StackFrame(1);
            var sm = st.GetMethod();
            WriteLine($"{sm?.ReflectedType?.FullName} -> {callerName} : {msg}");
        }
    }
}
