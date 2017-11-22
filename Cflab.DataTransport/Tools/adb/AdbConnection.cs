using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace Cflab.DataTransport.Tools.Adb
{
	/// <summary>
	/// ADB服务交互时的响应结果
	/// </summary>
	public struct AdbResult
	{
		/// <summary>
		/// 是否成功
		/// </summary>
		public bool Okay { get; set; }

		/// <summary>
		/// 状态字符串
		/// </summary>
		public string State { get; set; }

		/// <summary>
		/// 数据
		/// </summary>
		public string Data { get; set; }
	}

	/// <summary>
	/// ADB链接管理类
	/// </summary>
	public class AdbConnection
	{

	    /// <summary>
	    /// Adb 路径
	    /// </summary>
	    internal static string AdbPath { get; set; } = Environment.CurrentDirectory + @"\Tools\Adb\adb.exe";

	    /// <summary>
	    /// 服务端apk软件的地址
	    /// </summary>
	    internal static string ApkPath { get; set; } = Environment.CurrentDirectory + @"\Tools\Apk\app-release-1.3.2.apk";

        /// <summary>
        /// 软件所使用的ADB内部版本
        /// </summary>
	    private static string AdbVersion => "0027";

        /// <summary>
        /// 软件所使用的APK版本
        /// </summary>
	    public static string ApkVersion => "1.3.2";

        /// <summary>
        /// 初始化ADB后台服务
        /// </summary>
        /// <param name="error">错误处理回调</param>
        /// <returns>初始化ADB服务是否成功</returns>
        public static bool InitService(Action<ErrorResult> error)
		{
            // 尝试连接到ADB服务，并验证版本
		    if (VerifyVersion(error))
		    {
		        return true;
		    }
            // 尝试连接失败，重新开启服务
            var result = ToggleServer(true, error);

            // 再次验证ADB验证ADB版本
		    return result && VerifyVersion(error);
		}

	    /// <summary>
	    /// 开启或关闭Adb服务
	    /// </summary>
	    /// <param name="start">表示开启服务或结束服务</param>
	    /// <param name="error"></param>
	    /// <returns></returns>
	    public static bool ToggleServer(bool start,Action<ErrorResult> error)
		{
		    try
		    {
		        var process = new Process();
		        var info = new ProcessStartInfo
		        {
		            FileName = AdbPath,
		            Arguments = start? "start-server":"kill-server",
		            StandardErrorEncoding = Encoding.UTF8,
		            StandardOutputEncoding = Encoding.UTF8,
		            RedirectStandardError = true,
		            RedirectStandardOutput = true,
		            CreateNoWindow = true,
		            UseShellExecute = false
		        };
		        process.StartInfo = info;
		        process.Start();
		        process.WaitForExit(30 * 1000);
		        return process.ExitCode == 0;
		    }
		    catch (Exception e)
		    {
		        ErrorResult.InvokeError(error, InitError.AdbNotExists, e.Message);
		        return false;
		    }
		}

		/// <summary>
		/// 获取Adb内部版本，可能出现的错误，无法连接
		/// </summary>
		/// <param name="error">错误回调</param>
		/// <returns></returns>
		public static bool VerifyVersion(Action<ErrorResult> error)
		{
            // 连接到ADB服务
			var socket = SocketHelper.Connect();
			if (socket == null)
			{
			    // 无法链接到ADB服务
			    ErrorResult.InvokeError(error, ConnectError.AdbServerUnable, "Socket 无法链接到ADB服务端（5037）端口！");
			    return false;
			}
            // 执行查询内部版本命令
			const string cmd = "host:version";
			socket.Send(Encoding.ASCII.GetBytes($"{cmd.Length:X4}{cmd}"));
            // 读取结果，包含状态和版本
			var res = SocketHelper.GetResult(socket);
		    var flag = res.Okay && string.Equals(res.Data, AdbVersion);
		    try
		    {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
		    }
		    catch (Exception)
		    {
		        // ignored
		    }
		    return flag;
		}
	}
}
