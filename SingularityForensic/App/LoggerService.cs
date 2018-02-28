using EventLogger;
using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.App {
    [Export(typeof(ILoggerService))]
    class LoggerService : ILoggerService {
        public void WriteCallerLine(string msg,[CallerMemberName] string callerName = null) {
            Logger.WriteCallerLine(msg, callerName);
        }

        public void WriteLine(string msg) {
            Logger.WriteLine(msg);
        }
    }
}
