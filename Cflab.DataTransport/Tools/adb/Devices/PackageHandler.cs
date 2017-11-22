using System;
using System.Threading;
using Cflab.DataTransport.Tools.Adb.Handler;

namespace Cflab.DataTransport.Tools.Adb.Devices
{
	/// <summary>
	/// 基于Adb的包管理器
	/// </summary>
	public class PackageHandler
	{
		/// <summary>
		/// 序列号
		/// </summary>
		private readonly string serial;

		/// <summary>
		/// 错误处理回调
		/// </summary>
		private readonly Action<ErrorResult> error;

        /// <summary>
        /// 安装软件相关
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="error"></param>
		public PackageHandler(string serial, Action<ErrorResult> error = null)
		{
			this.serial = serial;
			this.error = error;
		}

		/// <summary>
		/// 安装Apk，先检测是否安装过相应版本
		/// </summary>
		/// <param name="path"></param>
		/// <param name="option"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public bool Install(string path, string option = null, Action<ErrorResult> error = null)
		{
			var flag = true;
			// 上传Apk
			var are = new AutoResetEvent(false);
		    var sync = new SyncHandler(serial)
		    {
		        Exit = success =>
		        {
		            flag = success;
		            are.Set();
		        }
		    };
		    sync.BeginPush(path, "/data/local/tmp/base.apk", 0100777, error ?? this.error);
			// 等待文件上传成功
			are.WaitOne();
			if (!flag)
			{
				return flag;
			}
			// 执行安装命令
		    var cmd = $"pm install {option} /data/local/tmp/base.apk";
		    var res = ShellExecuter.Execute(serial, cmd, error ?? this.error);
			flag = !string.IsNullOrEmpty(res) && res.Contains("Success");
			return flag;
		}

		/// <summary>
		/// 启动软件
		/// </summary>
		/// <param name="package"></param>
		/// <param name="error"></param>
		public bool Launch(string package, Action<ErrorResult> error)
		{
			// 已经在运行,直接返回结果
			if (IsRunning(package,error))
			{
				return true;
			}
			// 执行启动命令
			var cmd = $"monkey -p {package} -c android.intent.category.LAUNCHER 1";
		    var str = ShellExecuter.Execute(serial, cmd, error);
		    // 判断是否启动成功
			return IsRunning(package, error);
        }

		/// <summary>
		/// 判断给定的Activity是否在最前端
		/// </summary>
		/// <param name="package"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public bool IsOnTop(string package, Action<ErrorResult> error)
		{
		    var flag = false;
		    const string cmd = "dumpsys activity activities";
		    var res = ShellExecuter.Execute(serial, cmd, error);
		    if (string.IsNullOrEmpty(res))
		    {
		        return false;
		    }
		    foreach (var line in res.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
		    {
		        flag = line.Trim().StartsWith("mFocusedActivity") && line.Contains(package);
		        if (flag)
		        {
		            break;
		        }
		    }
		    return flag;
        }

		/// <summary>
		///判断给定的Activity是否在运行中！
		/// "mFocusedActivity"
		/// </summary>
		/// <param name="package"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public bool IsRunning(string package, Action<ErrorResult> error)
		{
			var flag = false;
			//const string cmd = "dumpsys activity activities | grep \"Run\"";
			// 有些手机没有grep  转到本地判断
			const string cmd = "dumpsys activity activities";
		    var res = ShellExecuter.Execute(serial, cmd, error);
		    if (string.IsNullOrEmpty(res))
		    {
		        return false;
		    }
		    foreach (var line in res.Split(new[]{ "\n" }, StringSplitOptions.RemoveEmptyEntries))
		    {
		        flag = line.Trim().StartsWith("Run") && line.Contains(package);
		        if (flag)
		        {
		            break;
		        }
		    }
		    return flag;
		}

	    /// <summary>
	    /// 检测是否已经安装
	    /// </summary>
	    /// <param name="package">包名</param>
	    /// <param name="version">版本号</param>
	    /// <param name="error"></param>
	    /// <returns></returns>
	    public bool CheckInstall(string package, string version, Action<ErrorResult> error)
		{
			// var cmd = "pm list packages | grep " + package;
			// const string cmd = "pm list packages";
			var cmd = "dumpsys package "+package;
		    var res = ShellExecuter.Execute(serial, cmd, error);
		    return !string.IsNullOrEmpty(res) && res.Contains("versionName=" + version);
		}
	} 
}
