using EventLogger;
using SingularityForensic.Contracts.App;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;

namespace SingularityForensic.App {
    [Export(typeof(ILoggerService))]
    class LoggerServiceImpl : ILoggerService {
        public void WriteCallerLine(string msg,[CallerMemberName] string callerName = null) {
            Logger.WriteCallerLine(msg, callerName);
        }

        public void WriteLine(string msg) {
            Logger.WriteLine(msg);
        }
    }
}
