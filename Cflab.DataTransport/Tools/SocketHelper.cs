using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Cflab.DataTransport.Modules.Transport;
using Cflab.DataTransport.Tools.Adb;

namespace Cflab.DataTransport.Tools
{
	public static class SocketHelper
	{
		/// <summary>
		/// 向客户端发送命令
		/// </summary>
		/// <param name="socket"></param>
		/// <param name="cmd"></param>
		/// <param name="arg"></param>
		public static void SendTransportCommand(Socket socket,int cmd, string arg = null)
		{
			var command = new Command
			{
				Cmd = cmd,
				Arg = arg
			};
			var bytes = Encoding.UTF8.GetBytes(command.ToString());
			socket?.Send(bytes);
		}

		/// <summary>
		/// 获取服务端响应结果
		/// </summary>
		/// <param name="socket"></param>
		/// <param name="iffial"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public static AdbResult GetResult(Socket socket, bool iffial = false, bool state = true)
		{
			var res = new AdbResult();
			// 读取状态
			if (state)
			{
				res.State = Read(socket, 4, Encoding.ASCII);
				res.Okay = string.Equals(res.State, "OKAY");
				// 判断是否为错误时才读取消息
				if (iffial && res.Okay)
				{
					return res;
				}
			}
			// 读取消息长度,读取失败或长度为0时返回
			try
			{
				var bytes = Read(socket, 4, Encoding.ASCII);
				// 不读取状态字符串时，根据数据长度设置状态
				if (!state && bytes.Length == 4)
				{
					res.Okay = true;
				}
				var len = Convert.ToInt32(bytes, 16);
				if (len <= 0)
				{
					return res;
				}
				// 根据长度读取消息
				res.Data = Read(socket, len, Encoding.ASCII);
			}
			catch (Exception)
			{
				// ignored
			}
			return res;
		}

		/// <summary>
		/// 获取SYNC交互状态,错误时读取错误信息
		/// </summary>
		/// <param name="socket"></param>
		/// <param name="length"></param>
		/// <param name="success"></param>
		/// <param name="fail"></param>
		/// <returns></returns>
		public static AdbResult GetState(Socket socket, out int length, string success = "DATA", string fail = "FAIL")
		{
			var res = new AdbResult
			{
				// 读取状态代码
				State = Read(socket, 4, Encoding.ASCII)
			};
			length = -1;

			// 读取长度
			var bytes = new byte[4];
			var len = Read(socket, bytes, 4);
			if (len != 4)
			{
				return res;
			}
			var slen = BitConverter.ToInt32(bytes, 0);
			length = slen;

			// 判断响应状态是否为成功
			res.Okay = string.Equals(res.State, success);
			if (string.Equals(res.State, fail))
			{
				res.Data = Read(socket, slen, Encoding.UTF8);
			}
			return res;
		}

		/// <summary>
		/// 读取固定长度的字节
		/// </summary>
		/// <param name="socket"></param>
		/// <param name="buffer"></param>
		/// <param name="len"></param>
		/// <returns></returns>
		public static int Read(Socket socket, byte[] buffer, int len)
		{
			if (socket == null)
			{
				return 0;
			}
			var are = new AutoResetEvent(false);
			socket.BeginReceive(buffer, 0, len, SocketFlags.None, ar =>
			{
				try
				{
					len = socket.EndReceive(ar);
				}
				catch (Exception)
				{
					len = 0;
				}
				are.Set();
			}, null);
			// 等待异步读取返回
			are.WaitOne();
			return len;
		}

		/// <summary>
		/// 读取固定长度的字符串
		/// </summary>
		/// <param name="socket"></param>
		/// <param name="len"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static string Read(Socket socket, int len, Encoding encoding)
		{
			var sb = new StringBuilder();
			var are = new AutoResetEvent(false);
			var buffer = new byte[len];
			socket.BeginReceive(buffer, 0, len, SocketFlags.None, ar =>
			{
				try
				{
					var rlen = socket.EndReceive(ar);
					if (rlen > 0)
					{
						sb.Append(encoding.GetString(buffer, 0, rlen));
					}
					are.Set();
				}
				catch (Exception)
				{
					are.Set();
				}
			}, null);
			are.WaitOne();
			return sb.ToString();
		}

		/// <summary>
		/// 读取手机端发送来的字符串,
		/// </summary>
		public static string GetTrandportMessage(Socket socket)
		{
			var sb = new StringBuilder();
			// 获取待接收的数据长度，6位十进制数的字符串
			// 长度指的是带接受的消息用UTF8编码后的字节数目
			var bytes = new byte[6];
			var len = Read(socket, bytes, 6);
			if (len != 6)
			{
				return sb.ToString();
			}
			// 获取本次消息的总长度
			var slen = Encoding.ASCII.GetString(bytes);
			int.TryParse(slen, out var tlen);
			// 如果长度超过65536，则分片，即一次最多读取65536字节
			var buffer = new byte[Math.Min(1024 * 64, tlen)];
			while (tlen > 0)
			{
				len = Read(socket, buffer, buffer.Length);
				sb.Append(Encoding.UTF8.GetString(buffer, 0, len));
				tlen -= len;
			}
			return sb.ToString();
		}

		/// <summary>
		/// 连接到Tcp服务端口,链接失败则返回空
		/// </summary>
		/// <param name="server">IP地址，默认为本机IP</param>
		/// <param name="port">端口，默认为ADB服务默认端口5037</param>
		/// <returns></returns>
		public static Socket Connect(string server = "127.0.0.1", int port = 5037)
		{
			var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				socket.Connect(new IPEndPoint(IPAddress.Parse(server), port));
			}
			catch (Exception)
			{
                // 链接失败，返回null
			    socket = null;
			}
			return socket;
		}
	}
}
