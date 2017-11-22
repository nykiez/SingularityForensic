using System;
using System.IO;
using System.Text;
using System.Threading;
using Cflab.DataTransport.Tools;
using Cflab.DataTransport.Tools.Adb.Handler;

namespace Cflab.DataTransport.Modules.Transport
{
	/// <inheritdoc />
	/// <summary>
	/// 获取文件
	/// </summary>
	public class FileHandler : TcpHandler
	{
		///// <summary>
		///// 当前文件长度
		///// </summary>
		//private long current;

		/// <summary>
		/// 文件总长度
		/// </summary>
		private long total = -1;

        /// <summary>
        /// 进度处理委托
        /// </summary>
        public Action<long, long> DataHandler { get; set; }

        /// <summary>
        /// 退出事件监听委托
        /// </summary>
        public Action<bool> Exit { get; set; }


        public FileHandler(string serial) : base(serial)
		{

		}

		/// <summary>
		/// 开始获取文件
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="des"></param>
		/// <param name="error"></param>
		public void BeginGetFile(string cmd, string des, Action<ErrorResult> error)
		{
            // 1.连接到TCP端口
			Connect(10101, error);
			if (socket == null)
			{
                OnExit(false);
				return;
			}
		    // 2.发送获取文件命令
		    socket.Send(Encoding.UTF8.GetBytes(cmd));
		    var current = 0L;
		    // 3.开始接收数据
		    ThreadPool.QueueUserWorkItem(back =>
		    {
		        // 4.等待获取文件大小
		        var flag = WaittingFileSize();
		        if (!flag)
		        {
		            OnExit(false);
		            return;
		        }
		        // 总数量为0时直接退出
		        if (total <= 0)
		        {
		            // 处理数据输出回调,通知调用者文件大小为0
		            DataHandler?.Invoke(current, total);
		            SocketHelper.SendTransportCommand(socket, Command.CLIENT_OVER);
		            OnExit(false);
		            return;
		        }
		        try
		        {
		            var file = File.OpenWrite(des);
		            // 循环接收文件
		            while (true)
		            {
                        // 接收并解析信息
		                var buffer = new byte[1024 * 1024];
		                var len = SocketHelper.Read(socket, buffer, buffer.Length);
		                file.Write(buffer, 0, len);
		                file.Flush();
		                current += len;
		                // 回调进度
		                DataHandler?.Invoke(current, total);
                        if (current == total)
                        {
                            SocketHelper.SendTransportCommand(socket, Command.CLIENT_OVER);
                            break;
                        }
		                SocketHelper.SendTransportCommand(socket, Command.FILE_RECIVED);
                    }
                    file.Close();
		        }
		        catch (Exception e)
		        {
		            Console.WriteLine(e);
		        }
                OnExit(true);
            });
		}

	    /// <summary>
	    /// 等待获取消息数量
	    /// </summary>
	    /// <returns></returns>
	    private bool WaittingFileSize()
	    {
	        var flag = false;
	        while (true)
	        {
	            var msg = SocketHelper.GetTrandportMessage(socket);
	            // 读取到的消息为空，直接返回
	            if (string.IsNullOrEmpty(msg))
	            {
	                break;
	            }
	            // 解析收到的命令
	            var command = Command.Parse(msg);
	            // 处理命令，没有权限，或者是文件大小
	            flag = HandleCommand(command);
	            break;
	        }
	        // 报告收到数量信息，HandleCommand里边已经处理了
	        // SocketHelper.SendTransportCommand(socket, Command.INFO_RECIVED);
	        return flag;
	    }

        /// <summary>
        /// 处理命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool HandleCommand(Command command)
		{
			if (command == null)
			{
				return true;
			}
			switch (command.Cmd)
			{
				// 解析文件大小
				case Command.FILE_SIZE:
					if (long.TryParse(command.Arg, out total))
					{
						SocketHelper.SendTransportCommand(socket, Command.FILE_RECIVED);
					}
					break;

				// 收到没有权限提示
				case Command.LACK_PERMISITION:
				    Error(ConnectError.ApkLackPermisions, "APK缺少部分必须权限！");
					return false;
			}
			return true;
		}

	    protected override void OnExit(bool success)
	    {
            Exit?.Invoke(success);
	        base.OnExit(success);
	    }
	}
}
