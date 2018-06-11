using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public interface ILoggerService {
        void WriteLine(string msg);
        void WriteCallerLine(string msg,[CallerMemberName] string callerName = null);
        void WriteException(Exception ex, [CallerMemberName] string callerName = null);
        void WriteStack(string msg, [CallerMemberName] string callerName = null);
    }

    /// <summary>
	/// Contains an <see cref="IMessageBoxService"/> instance
	/// </summary>
	public class LoggerService : GenericServiceStaticInstance<ILoggerService> {
        public static void WriteLine(string msg) => Current?.WriteLine(msg);

        public static void WriteCallerLine(string msg, [CallerMemberName] string callerName = null) => Current?.WriteCallerLine(msg,callerName);

        public static void WriteException(Exception ex, [CallerMemberName] string callerName = null) => Current?.WriteException(ex,callerName);

        public static void WriteStack(string msg, [CallerMemberName] string callerName = null) => Current?.WriteStack(msg, callerName);
    }
}
