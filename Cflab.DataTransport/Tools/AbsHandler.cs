using System;
using System.Net.Sockets;

namespace Cflab.DataTransport.Tools
{

	public abstract class AbsHandler
	{
		/// <summary>
		/// 绑定的Socket
		/// </summary>
		protected Socket socket;

		/// <summary>
		/// 错误处理回调
		/// </summary>
		private Action<ErrorResult> error;

		/// <summary>
		/// 尝试链接到ADB服务，并进行初始化
		/// </summary>
		/// <param name="error"></param>
		/// <param name="server"></param>
		/// <param name="port"></param>
		/// <returns>是否成功连接到Socket</returns>
		protected bool Begin(Action<ErrorResult> error, string server = "127.0.0.1", int port = 5037)
		{
			this.error = error;
			socket = SocketHelper.Connect( server, port);
			if (socket == null)
			{
				// 链接失败，直接退出
                Error(ConnectError.AdbServerUnable,"无法连接到ADB服务！");
				OnExit(false);
			}
			else
			{
				// 链接成功，启动初始化程序
				OnInit();
			}
			return socket != null;
		}

        /// <summary>
        /// 执行错误回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
		protected void Error(int code, string msg)
		{
		    ErrorResult.InvokeError(error, code, msg);
			OnExit(false);
		}

		/// <summary>
		/// 内部使用函数 => 初始化
		/// </summary>
		protected abstract void OnInit();

		/// <summary>
		/// 内部使用函数 => 初始化
		/// </summary>
		protected abstract void OnExit(bool success);
	}
}
