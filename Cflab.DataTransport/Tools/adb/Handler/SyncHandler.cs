using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Cflab.DataTransport.Tools.Adb.Handler
{
	public class SyncHandler : AbsHandler
	{
		/// <summary>
		/// 序列号
		/// </summary>
		private readonly string serial;

		/// <summary>
		/// 退出时的回掉
		/// </summary>
		public Action<bool> Exit { get; set; }

		/// <summary>
		/// 进度更新时的huo掉
		/// </summary>
		public Action<int> Progress { get; set; }

		/// <summary>
		/// 构造器
		/// </summary>
		/// <param name="serial">设备序列号</param>
		public SyncHandler(string serial)
		{
			this.serial = serial;
		}

		/// <inheritdoc />
		/// <summary>
		/// 1.打开Transport
		/// 2.进入Sync状态
		/// 3.数据交换
		/// </summary>
		protected override void OnInit()
		{
			// 1.打开Transport
			var cmd = "host:transport:" + serial;
			socket.Send(Encoding.ASCII.GetBytes($"{cmd.Length:X4}{cmd}"));
			// 成功时不带消息体
			var result = SocketHelper.GetResult(socket, true);
			if (!result.Okay)
			{
				Error(ConnectError.EnterTransportFail, result.Data);
				return;
			}
			// 2.进入Sync状态
			cmd = "sync:";
			socket.Send(Encoding.ASCII.GetBytes($"{cmd.Length:X4}{cmd}"));
			// 成功时不带消息体
			result = SocketHelper.GetResult(socket, true);
			if (!result.Okay)
			{
                // 调用Error后Socket为空
				Error(ConnectError.EnterSyncFail, result.Data);
			}
		}

		protected override void OnExit(bool success)
		{
            // 执行退出回调
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

		/// <summary>
		/// 异步的Push操作
		/// </summary>
		/// <param name="local"></param>
		/// <param name="remote"></param>
		/// <param name="mod"></param>
		/// <param name="error"></param>
		public void BeginPush(string local, string remote, int mod = 0100666, Action<ErrorResult> error = null)
		{
			try
			{
				var stream = File.OpenRead(local);
				if (!Begin(error))
				{
					return;
				}
				ThreadPool.QueueUserWorkItem(back =>
				{
					// 3.进入发送文件状态
					Send("SEND", remote + "," + mod);
					// 4.发送文件流
					Send(stream);
					// 5.发送文件完成状态，文件最后修改时间
				    var utc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    var time = File.GetLastWriteTime(local).ToUniversalTime();
                    Send("DONE", Convert.ToInt32(time.Subtract(utc).Ticks / 10000000));
					// 6.验证发送是否成功,失败时读取附加消息
					var res = SocketHelper.GetResult(socket, true);
					OnExit(res.Okay);
					// 7.结束
					stream.Close();
				});
			}
			catch (Exception e)
			{
                // 读取APK安装包文件失败
			    if (e is FileNotFoundException || e is DirectoryNotFoundException)
			    {
			        Error(CommonError.FileNotExists, local);
                }
            }
		}

		/// <summary>
		/// 开始异步Pull
		/// </summary>
		/// <param name="remote"></param>
		/// <param name="local"></param>
		/// <param name="error"></param>
		public void BeginPull(string local, string remote, Action<ErrorResult> error = null)
		{
			try
			{
				// 确保本地路径可用
				var dir = Path.GetDirectoryName(local);
				if (!string.IsNullOrEmpty(dir))
				{
					if (!Directory.CreateDirectory(dir).Exists)
					{
                        // 为指定的文件路径创建文件夹失败
					    Error(CommonError.InvalidDirectory, local);
						return;
					}
				}
				var stream = File.OpenWrite(local);
				if (!Begin(error))
				{
					return;
				}
				// 获取文件大小
				var size = Convert.ToInt64(GetLength(remote));
				// 3.进入接收文件状态
				Send("RECV", remote);
				ThreadPool.QueueUserWorkItem(back =>
				{
					// 4.开始接收文件流
					var success = true;
					var current = 0L;
					var process = 0;
					var buffer = new byte[1024 * 64];
					while (true)
					{
						var state = SocketHelper.GetState(socket, out var tlen);
						// 验证是否发送完成
						if (string.Equals(state.State,"DONE"))
						{
							break;
						}
						if (!state.Okay)
						{
							// 接收文件流失败
							// Error(ResultType.Error,state.Data ?? "Unkonw Error in Pull!");
							success = false;
							break;
						}
						current += tlen;
						while (tlen > 0)
						{
							var len = SocketHelper.Read(socket, buffer, tlen);
							stream.Write(buffer, 0, len);
							tlen -= len;
						}
                        // 更新进度
						var temp = Convert.ToInt32(100 * current / size);
						if (temp == process)
						{
							continue;
						}
						process = temp;
						Progress?.Invoke(process);
					}
					OnExit(success);
					stream.Close();
				});
			}
			catch (Exception e)
			{
                Console.WriteLine(e.Message);
			}
		}

        /// <summary>
        /// 通过 STAT获取文件状态
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
		private int GetLength(string path)
		{
			Send("STAT", path);
			var bufer = new byte[4];
			SocketHelper.Read(socket, bufer, 4);
			// 状态
			// var id = Encoding.UTF8.GetString(bufer);
			SocketHelper.Read(socket, bufer, 4);
			// mode Linux文件模式，类型+权限相关
			// var mode = BitConverter.ToInt32(bufer, 0);
			SocketHelper.Read(socket, bufer, 4);
			// 文件大小，字节数
			var size = BitConverter.ToInt32(bufer,0);
			// 时间
			SocketHelper.Read(socket, bufer, 4);
			// time
			// var time = BitConverter.ToInt32(bufer, 0);
			return size;
		}

		/// <summary>
		/// 发送Sync命令
		/// </summary>
		/// <param name="sync"></param>
		/// <param name="name"></param>
		private void Send(string sync, string name)
		{
			socket.Send(Encoding.ASCII.GetBytes(sync));
			socket.Send(BitConverter.GetBytes(name.Length));
			socket.Send(Encoding.ASCII.GetBytes(name));
		}

		/// <summary>
		/// 发送Sync命令
		/// </summary>
		/// <param name="sync"></param>
		/// <param name="len"></param>
		private void Send(string sync, int len)
		{
			socket.Send(Encoding.ASCII.GetBytes(sync));
			socket.Send(BitConverter.GetBytes(len));
		}

		/// <summary>
		/// 发送流
		/// </summary>
		/// <param name="stream"></param>
		private void Send(Stream stream)
		{
			var current = 0L;
			var process = 0;
			var total = stream.Length;
			var buffer = new byte[1024 * 64];
			var len = stream.Read(buffer, 0, buffer.Length);
			while (len > 0)
			{
				current += len;
				Send(buffer, len);
				// 写入完成，更新进度
				var tmp = Convert.ToInt32(100 * current / total);
				if (process != tmp)
				{
					Progress?.Invoke(tmp);
					process = tmp;
				}
				len = stream.Read(buffer, 0, buffer.Length);
			}
		}

		/// <summary>
		/// 发送字节数组
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="len"></param>
		private void Send(byte[] buffer, int len)
		{
			socket.Send(Encoding.ASCII.GetBytes("DATA"));
			socket.Send(BitConverter.GetBytes(len));
			socket.Send(buffer, len, SocketFlags.None);
		}

	}
}
