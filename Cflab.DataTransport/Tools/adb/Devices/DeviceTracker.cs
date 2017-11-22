using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Cflab.DataTransport.Modules.Transport.Model;

namespace Cflab.DataTransport.Tools.Adb.Devices
{
	/// <inheritdoc />
	/// <summary>
	/// 设备连接状态监控器
	/// </summary>
	public class DeviceTracker : AbsHandler
	{
		/// <summary>
		/// 要执行的命令
		/// </summary>
		private string cmd;

		/// <summary>
		/// 是否为追踪设备状态
		/// </summary>
		private bool track;

		/// <summary>
		/// 获取到手机列表的回调
		/// </summary>
		private Action<List<Device>> handler;

        /// <summary>
        /// 退出时的回调
        /// </summary>
		private Action exicted;

		/// <summary>
		/// 用于将获取设备列表转换成同步方法
		/// </summary>
		private AutoResetEvent are;


        /// <inheritdoc />
        /// <summary>
        /// 初始化，主动调用Begin，成功链接到Socket后执行
        /// </summary>
		protected override void OnInit()
		{
		    // 非追踪模式启用同步，追踪模式使用异步
            if (!track)
			{
				are = new AutoResetEvent(false);
			}
            // 异步循环读取返回
			ThreadPool.QueueUserWorkItem(back =>
			{
				// 判断命令是否执行成功
				var result = SocketHelper.GetResult(socket);
				if (result.Okay)
				{
					// 解析命令成功时附带的消息
				    var devices = Device.Parse(result.Data,err=>{Error(err.Code,err.Message);});
				    handler?.Invoke(devices);
					// 追踪状态循环读取
					while (track)
					{
						// 只读取消息，同时根据消息确定是否读取成功
						result = SocketHelper.GetResult(socket, false, false);
						// 读取消息失败时结束循环
						if (!result.Okay)
						{
							break;
						}
                        // 获得设备，执行回调
					    devices = Device.Parse(result.Data, err => { Error(err.Code, err.Message); });
                        handler?.Invoke(devices);
					}
				}
				else
				{
                    // 读取结果时读取失败，或返回FAIL
				    Error(ConnectError.AdbResultFail, string.IsNullOrEmpty(result.State)?"读取结果失败！": result.Data);
				}
                // 处理取消完成
				exicted?.Invoke();
                // 处理同步读取完成
				are?.Set();
			});
			// 发送获取设备，或者追踪设备命令
			socket.Send(Encoding.ASCII.GetBytes($"{cmd.Length:X4}{cmd}"));
			if (are == null)
			{
				return;
			}
            // 获取设备列表完成，并退出
			are.WaitOne();
		    OnExit(true);

        }


        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public void GetDevices(Action<List<Device>> handler, Action<ErrorResult> error = null)
	    {
	        cmd = "host:devices";
	        this.handler = handler;
	        Begin(error);
	    }

	    /// <summary>
	    /// 追踪设备连接情况
	    /// </summary>
	    /// <param name="handler"></param>
	    /// <param name="error"></param>
	    public void BeginTrackDevices(Action<List<Device>> handler, Action<ErrorResult> error)
	    {
	        cmd = "host:track-devices";
	        this.handler = handler;
	        // 设置为追踪设备模式
	        track = true;
	        Begin(error);
	    }

	    /// <summary>
	    /// 停止追踪
	    /// </summary>
	    public void StopTrack(Action exicted)
	    {
	        this.exicted = exicted;
	        // 设置停止监听
	        track = true;
	        // 移除监听回调
	        handler = null;
            // 发送退出监听命令
	        try
	        {
	            socket.Send(Encoding.ASCII.GetBytes("QUIT"));
	        }
	        catch (Exception)
	        {
	            // ignore
	        }
	        // 关闭Socket
	        OnExit(true);
	    }

        /// <inheritdoc />
        /// <summary>
        /// 退出时，清理资源
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
