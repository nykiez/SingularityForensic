using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Cflab.DataTransport.Modules.Transport.Model;
using Cflab.DataTransport.Tools;
using Cflab.DataTransport.Tools.Adb.Handler;
using Newtonsoft.Json;

namespace Cflab.DataTransport.Modules.Transport
{
    public class InfoHandler<TInfo> : TcpHandler where TInfo : IInfo
	{
        /// <summary>
        /// 数据总条数
        /// </summary>
		private int total = -1;

        /// <summary>
        /// 数据输出回调
        /// </summary>
	    public Action<List<TInfo>, int, int> DataHandler { get; set; }

        /// <summary>
        /// 退出回调，
        /// </summary>
        public Action<bool> Exit { get; set; }

        /// <summary>
        /// 构造函数，手机序列号
        /// </summary>
        /// <param name="serial"></param>
        public InfoHandler(string serial) : base(serial)
		{

		}
		/// <summary>
		/// 获取消息
		/// </summary>
		public void BeginGetInfo(string cmd, Action<ErrorResult> error)
		{
            // 1.连接到TCP端口
			Connect(10101, error);
			if (socket == null)
			{
                OnExit(false);
				return;
			}
			// 2.发送获取数据命令
			socket.Send(Encoding.UTF8.GetBytes(cmd));
			var current = 0 ;
			// 3.开始接收数据
			ThreadPool.QueueUserWorkItem(back =>
			{
                // 4.等待获取消息数量
			    var flag = WaittingInfoSize();
                if (!flag)
                {
                    OnExit(false);
                    return;
                }
                // 总数量为0时直接退出
			    if (total <= 0)
                {
                    // 处理数据输出回调,通知调用者信息数量为0
                    DataHandler?.Invoke(null, current, total);
                    SocketHelper.SendTransportCommand(socket, Command.CLIENT_OVER);
                    OnExit(false);
                    return;
                }
                // 循环接收消息
				while (true)
				{
						// 接收并解析信息
						var info = SocketHelper.GetTrandportMessage(socket);
						// 没有读取到消息
						if (string.IsNullOrWhiteSpace(info))
						{
							break;
						}
						try
						{
							var infos = ParseJson<TInfo>(info);
							current += infos.Count;
                            // 处理数据输出回调
							DataHandler?.Invoke(infos, current, total);
							// 消息接受完毕，结束循环
							if (total == current)
							{
								SocketHelper.SendTransportCommand(socket, Command.CLIENT_OVER);
                                break;
							}
							// 报告消息解析完毕，请求再次发送
							SocketHelper.SendTransportCommand(socket, Command.INFO_RECIVED);
						}
						catch (Exception)
						{
							SocketHelper.SendTransportCommand(socket, Command.CLIENT_OVER);
							break;
						}
				} // 循环结束,正常关闭传输
				OnExit(true);
			});
		}

        /// <summary>
        /// 等待获取消息数量
        /// </summary>
        /// <returns></returns>
	    private bool WaittingInfoSize()
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
	            // 处理命令，没有权限，或者是消息数量
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
		/// <returns>是否成功获取到数据数量</returns>
		private bool HandleCommand(Command command)
		{
			if (command == null)
			{
				return false;
			}
			switch (command.Cmd)
			{
				// 解析消息数量
				case Command.INFO_COUNT:
					if (int.TryParse(command.Arg, out total))
					{
						SocketHelper.SendTransportCommand(socket,Command.INFO_RECIVED);
					}
					break;

				// 收到没有权限提示
				case Command.LACK_PERMISITION:
				    Error(ConnectError.ApkLackPermisions, "APK缺少部分必须权限！");
					return false;
			}
			return true;
		}

	    /// <summary>
	    /// 反序列化Json
	    /// </summary>
	    /// <typeparam name="T"></typeparam>
	    /// <param name="json"></param>
	    /// <returns></returns>
	    public static List<T> ParseJson<T>(string json)
	    {
	        var serializer = new JsonSerializer();
	        //if (json.StartsWith("{[") && json.EndsWith("]}"))
	        //{
	        //	json = json.Substring(1, json.Length - 2);
	        //}
	        var jsr = new JsonTextReader(new StringReader(json));
	        var o = serializer.Deserialize(jsr, typeof(List<T>));
	        var list = o as List<T>;
	        return list;
	    }

	    protected override void OnExit(bool success)
	    {
            // 处理退出事件回调
	        Exit?.Invoke(success);
            base.OnExit(success);
	    }
	}
}
