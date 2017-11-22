using System;

namespace Cflab.DataTransport.Modules.Backup.Android.Tar
{
	public class TarException : Exception
	{
		public enum ExceptionType
		{
			UnKnown,
			NotSupportse,
			UnexpectedEnd,
			InvalidHeader,
			CreateDir
		}

		public ExceptionType Type { get; private set; }

		private TarException() { }
		private TarException(string message): base(message) { }

		public static TarException With(string message = null,ExceptionType type = ExceptionType.UnKnown)
		{
			var exc = message == null ? new TarException() : new TarException(message);
			exc.Type = type;
			return exc;
		}
		public static TarException UnKnown(string message = null)
		{
			return With(message);
		}

		public static TarException NotSupportse(string message = null)
		{
			return With(message, ExceptionType.NotSupportse);
		}

		public static TarException UnexpectedEnd(string message = null)
		{
			return With(message, ExceptionType.UnexpectedEnd);
		}
		public static TarException InvalidHeader(string message = null)
		{
			return With(message, ExceptionType.InvalidHeader);
		}
		public static TarException CreateDir(string message = null)
		{
			return With(message, ExceptionType.CreateDir);
		}
	}
}
