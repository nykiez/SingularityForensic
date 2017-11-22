using System;
using System.Threading;
using Cflab.DataTransport.Tools;
using Cflab.DataTransport.Tools.Adb.Handler;

namespace Cflab.DataTransport.Modules.Transport
{
	/// <inheritdoc />
	/// <summary>
	/// 用于测试
	/// </summary>
	public class TestHandler : TcpHandler
	{
		/// <summary>
		/// 构造器
		/// </summary>
		/// <param name="serial"></param>
		public TestHandler(string serial) : base(serial)
		{

		}

        /// <summary>
        /// 测试链接
        /// </summary>
        /// <param name="port"></param>
        /// <param name="error"></param>
        /// <returns></returns>
	    public bool TestConnect(int port, Action<ErrorResult> error)
		{
		    // 连接到tcp端口失败
            Connect(port, error);
		    var flag = socket != null;
            // 连接失败，退出
		    if (!flag)
			{
				return false;
			}
            // 发送测试连接的命令
			SocketHelper.SendTransportCommand(socket,101,"test");
			var msg = SocketHelper.GetTrandportMessage(socket);
			var cmd = Command.Parse(msg);
			if (cmd == null)
			{
				return false;
			}
			switch (cmd.Cmd)
			{
				case Command.SUCCESS:
                    // 测试连接成功！
					break;
				case Command.LACK_PERMISITION:
                    // 缺少必须权限
                    Error(ConnectError.ApkLackPermisions,"APK缺少必须权限！");
					return false;
			}
			OnExit(true);
			return true;
		}

        /// <summary>
        /// 请求手机端尝试获取Root
        /// </summary>
        /// <param name="port"></param>
        /// <param name="checker"></param>
        /// <param name="times"></param>
        /// <param name="error"></param>
        /// <returns></returns>
	    public bool TestRoot(int port,Func<bool> checker, int times,Action<ErrorResult> error)
	    {
	        // 连接到tcp端口失败
	        Connect(port, error);
	        var flag = socket != null;
	        // 连接失败，退出
	        if (!flag)
	        {
	            return false;
	        }
	        flag = false;
	        for (var i = 0; i < times; i++)
	        {
	            // 发检查ROOT命令
	            SocketHelper.SendTransportCommand(socket, 777, "test");
	            var conn = checker?.Invoke() ?? false;
	            var msg = SocketHelper.GetTrandportMessage(socket);
	            var cmd = Command.Parse(msg);
	            if (cmd == null)
	            {
	                return false;
	            }
                if (cmd.Cmd != 777)
                {
                    break;
                }
                // 解析获取Root的结果
	            bool.TryParse(cmd.Arg, out flag);
                if (flag || !conn)
                {
                    break;
                }
	        }
	        // 结束线程命令
	        SocketHelper.SendTransportCommand(socket, 998, "test");
            OnExit(flag);
            return flag;
	    }

        /// <summary>
        /// 多次尝试测试
        /// </summary>
        /// <param name="port"></param>
        /// <param name="time"></param>
        /// <param name="error"></param>
        /// <returns></returns>
	    public bool TestConnect(int port, int time, Action<ErrorResult> error)
	    {
	        var flag = false;
            var res = new ErrorResult();
	        for (var i = 0; i < time; i++)
	        {
	            flag = TestConnect(port, err =>
	            {
	                res = err;
	            });
	            if (flag || i == time - 1)
	            {
	                break;
	            }
                Thread.Sleep(250);
	        }
            if (!flag)
            {
                error?.Invoke(res);
            }
	        return flag;
	    }
	}
}
