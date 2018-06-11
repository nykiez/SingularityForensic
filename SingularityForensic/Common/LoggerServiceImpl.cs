using EventLogger;
using SingularityForensic.Contracts.Common;
using System;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;

namespace SingularityForensic.Common {
    [Export(typeof(ILoggerService))]
    class LoggerServiceImpl : ILoggerService {
        public void WriteCallerLine(string msg,[CallerMemberName] string callerName = null) {
            Logger.WriteCallerLine(msg, callerName);
        }

        public void WriteException(Exception ex, [CallerMemberName] string callerName = null) {
            Logger.WriteCallerLine(ex.Message);
            Logger.WriteCallerLine(ex.StackTrace);
            var ie = ex.InnerException;
            if(ie != null) {
                Logger.WriteLine(ie.Message);
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
    }
}
