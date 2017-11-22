using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Cflab.DataTransport.Tools.Adb.Handler
{
	/// <inheritdoc />
	/// <summary>
	/// 执行Shell命令
	/// </summary>
	public class ShellExecuter : AbsHandler
	{
		/// <summary>
		/// 设备序列号
		/// </summary>
		private readonly string serial;

		/// <summary>
		/// 运行的命令
		/// </summary>
		private string command;

        /// <summary>
        /// 指示是否成功进入shell
        /// </summary>
	    private bool ready;

		/// <summary>
		/// 构造器
		/// </summary>
		/// <param name="serial">设备序列号</param>
		public ShellExecuter(string serial)
		{
			this.serial = serial;
		}

		/// <inheritdoc />
		/// <summary>
		/// 初始化，由Begin调用
		/// </summary>
		protected override void OnInit()
		{
		    // 1.进入 transport
		    var cmd = "host:transport:" + serial;
		    socket.Send(Encoding.ASCII.GetBytes($"{cmd.Length:X4}{cmd}"));
		    // 读取Transport状态,成功时不带消息，错误时带消息
		    var result = SocketHelper.GetResult(socket, true);
		    if (!result.Okay)
		    {
		        // 未成功进入Transport状态，直接退出
		        Error(ConnectError.EnterTransportFail, result.Data);
		        return;
		    }
            // 2.执行Shell命令，等待退出
		    socket.Send(Encoding.ASCII.GetBytes($"{command.Length:X4}{command}"));
            // 读取第一条Shell命令执行状态,错误时带消息
            result = SocketHelper.GetResult(socket, true);
		    ready = result.Okay;
		    if (!result.Okay)
		    {
		        // 未成执行第一条命令，直接退出
		        Error(ConnectError.ShellExitWithError, result.Data);
		    }
		}

        /// <summary>
        ///  执行立即退出的shell命令
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="error">错误处理</param>
        private string Execute(string cmd, Action<ErrorResult> error)
		{
			if (socket != null || string.IsNullOrEmpty(cmd))
			{
                Error(CommonError.NullParameter,nameof(cmd));
				return string.Empty;
			}
            // 设置命令
		    command = "shell:" + cmd;
            // 开始初始化，并执行命令
            var success = Begin(error);
			if (!success || !ready)
			{
				return string.Empty;
			}
            // 读取输出
		    var sb = new StringBuilder();
            var buffer = new byte[1024 * 64];
		    var len = SocketHelper.Read(socket, buffer, buffer.Length);
		    while(true)
            {
                sb.Append(Encoding.UTF8.GetString(buffer, 0, len));
                len = SocketHelper.Read(socket, buffer, buffer.Length);
                if (len <= 0)
                {
                     break;
                }
            }
		    return sb.ToString();
		}


	    /// <summary>
	    ///  执行立即退出的shell命令
	    /// </summary>
	    /// <param name="cmd">命令</param>
	    /// <param name="error">错误处理</param>
	    private string SuExecute(string cmd, Action<ErrorResult> error)
	    {
	        if (socket != null || string.IsNullOrEmpty(cmd))
	        {
	            Error(CommonError.NullParameter, nameof(cmd));
	            return string.Empty;
	        }
	        // 设置命令为SU
	        command = "shell:"+"su";
	        // 开始初始化，并执行命令
	        var success = Begin(error);
	        if (!success || !ready)
	        {
	            return string.Empty;
	        }
            // 进入成功，执行命令
            if (!cmd.EndsWith("\n"))
            {
                cmd += "\n";
            }
	        socket.Send(Encoding.ASCII.GetBytes(cmd));
	        // 读取输出
	        var sb = new StringBuilder();
	        var buffer = new byte[1024 * 64];
	        var len = SocketHelper.Read(socket, buffer, buffer.Length);
	        while (true)
	        {
	            sb.Append(Encoding.UTF8.GetString(buffer, 0, len));
	            len = SocketHelper.Read(socket, buffer, buffer.Length);
                if (len <= 0)
	            {
	                break;
	            }
	            socket.Send(Encoding.ASCII.GetBytes("exit\n"));
            }
            return sb.ToString();
	    }

        /// <summary>
        /// 执行Shell命令并返回结果
        /// </summary>
        /// <param name="serial">序列号</param>
        /// <param name="cmd">要执行的命令</param>
        /// <param name="error">错误回调</param>
        /// <returns></returns>
        public static string Execute(string serial, string cmd, Action<ErrorResult> error)
		{
            var are = new AutoResetEvent(false);
            var sb = new StringBuilder();
		    var shell = new ShellExecuter(serial);
            ThreadPool.QueueUserWorkItem(back =>
		    {
		        var str = shell.Execute(cmd, error);
		        sb.Append(str);
		        are.Set();
		    });
            // 等待30秒，如果没有退出，手动结束
		    are.WaitOne(30 * 1000);
		    // shell.socket?.Send(Encoding.ASCII.GetBytes("QUIT"));
		    // 等待30秒，如果没有退出，强制退出
		    // are.WaitOne(30 * 1000);
            // shell.OnExit(false);
            return sb.ToString();
		}

	    /// <summary>
	    /// 执行Shell命令并返回结果
	    /// </summary>
	    /// <param name="serial">序列号</param>
	    /// <param name="cmd">要执行的命令</param>
	    /// <param name="error">错误回调</param>
	    /// <returns></returns>
	    public static string SuExecute(string serial, string cmd, Action<ErrorResult> error)
	    {
	        var are = new AutoResetEvent(false);
	        var sb = new StringBuilder();
	        var shell = new ShellExecuter(serial);
	        ThreadPool.QueueUserWorkItem(back =>
	        {
	            var str = shell.SuExecute(cmd, error);
	            sb.Append(str);
	            are.Set();
	        });
	        // 等待30秒，如果没有退出，手动结束
	        are.WaitOne(30 * 1000);
	        // shell.socket?.Send(Encoding.ASCII.GetBytes("QUIT"));
	        // 等待30秒，如果没有退出，强制退出
	        // are.WaitOne(30 * 1000);
	        // shell.OnExit(false);
	        return sb.ToString();
	    }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="success"></param>
        protected override void OnExit(bool success)
	    {
	        if (socket == null)
	        {
	            return;
	        }
	        try
	        {
	            socket.Shutdown(SocketShutdown.Both);
	            socket.Close();
	        }
	        catch (Exception)
	        {
	            // ignored
	        }
	        socket = null;
	    }
    }
}