using System;
using System.Net.Sockets;
using System.Text;

namespace Cflab.DataTransport.Tools.Adb.Handler
{
	/// <summary>
	/// 用于链接到手机端的TCP端口
	/// </summary>
	public class TcpHandler : AbsHandler
	{
		/// <summary>
		/// 序列号
		/// </summary>
		private readonly string serial;

		/// <summary>
		/// 要连接的端口
		/// </summary>
		private int port;

		public TcpHandler(string serial)
		{
			this.serial = serial;
		}

		protected override void OnInit()
		{
			// 1.打开Transport
			var cmd = "host:transport:" + serial;
			socket.Send(Encoding.ASCII.GetBytes($"{cmd.Length:X4}{cmd}"));
			// 成功时不带消息体
			var result = SocketHelper.GetResult(socket, true);
			if (!result.Okay)
			{
                // 未能成功进入Transport
                Error(ConnectError.EnterTransportFail, result.Data);
				return;
			}
			// 2.进入TCP状态
			cmd = "tcp:" + port;
			socket.Send(Encoding.ASCII.GetBytes($"{cmd.Length:X4}{cmd}"));
			// 成功时不带消息体
			result = SocketHelper.GetResult(socket, true);
			if (!result.Okay)
			{
				Error(ConnectError.EnterTcpportFail, result.Data);
			}
		}

		/// <summary>
		/// 链接到Android端指定的端口
		/// </summary>
		/// <param name="port"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public Socket Connect(int port, Action<ErrorResult> error)
		{
			this.port = port;
			if (port <= 1024 || port >= 65535)
			{
				Error(CommonError.NullParameter,"端口号为1025-65535");
			}
			Begin(error);
            // 如果进入失败，会直接调用OnExit，销毁Socket
            return socket;
		}

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
