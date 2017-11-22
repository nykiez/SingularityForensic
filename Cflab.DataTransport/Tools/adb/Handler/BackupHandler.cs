using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Cflab.DataTransport.Tools.Adb.Handler
{
	/// <summary>
	/// 系统备份工具
	/// </summary>
	public class BackupHandler : AbsHandler
	{
		/// <summary>
		/// 请求在手机上确认备份
		/// </summary>
		public Func<bool> Confirme { get; set; }

		/// <summary>
		/// 备份完成,退出回调
		/// </summary>
		public Action<bool> Exit { get; set; }

		/// <summary>
		/// 序列号
		/// </summary>
		private readonly string serial;

		public BackupHandler(string serial)
		{
			this.serial = serial;
		}

		/// <summary>
		/// 进行Android备份
		/// </summary>
		/// <param name="path"></param>
		/// <param name="error"></param>
		public void BeginBackup(string path, Action<ErrorResult> error)
		{
			var dir = Path.GetDirectoryName(path);
			try
			{
				if (!string.IsNullOrEmpty(dir))
				{
					Directory.CreateDirectory(dir);
				}
				var stream = File.OpenWrite(path);
				// 连接到Socket,连接失败则退出
				if (!Begin(error))
				{
					return;
				}
				// 2.请求备份
				const string cmd = "backup:-all";
				socket.Send(Encoding.ASCII.GetBytes($"{cmd.Length:X4}{cmd}"));
				// 错误时读取附加信息
				var result = SocketHelper.GetResult(socket, true);
				if (!result.Okay)
				{
				    Error(BackupResult.EnterAdbBackupFail, result.Data);
					return;
				}
                // 请求确认备份
				if (!Confirme?.Invoke() ?? true)
				{
					OnExit(false);
					return;
				}
                // 开启线程循环接受数据
				ThreadPool.QueueUserWorkItem(back =>
				{
					var buffer = new byte[1024 * 64];
					var len = SocketHelper.Read(socket, buffer, buffer.Length);
					while (len > 0)
					{
						stream.Write(buffer, 0, len);
						len = SocketHelper.Read(socket, buffer, buffer.Length);
					}
					// 用户点击了不备份时，没有数据写入，所以Position为0
				    var flag = stream.Position == 0;
				    stream.Close();
                    if (flag)
					{
						
						Error(BackupResult.UserCancel,"用户点击了取消备份！");
						return;
					}
					OnExit(true);
				});
			}
			catch (Exception e)
			{
			    if (e is DirectoryNotFoundException || e is IOException)
			    {
			        Error(CommonError.InvalidDirectory,"创建文件或目录时出错！");
			    }
			}
		}

		protected override void OnInit()
		{
			// 1.打开Transport
			var cmd = "host:transport:" + serial;
			socket?.Send(Encoding.ASCII.GetBytes($"{cmd.Length:X4}{cmd}"));
			// 错误时读取附加信息
			var result = SocketHelper.GetResult(socket, true);
			if (result.Okay)
			{
				return;
			}
            // 进入Transport出错
            Error(ConnectError.EnterTransportFail, result.Data);
		}

		protected override void OnExit(bool success)
		{
			Exit?.Invoke(success);
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
