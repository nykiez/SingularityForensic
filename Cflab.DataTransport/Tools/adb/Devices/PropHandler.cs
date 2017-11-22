using System;
using Cflab.DataTransport.Tools.Adb.Handler;

namespace Cflab.DataTransport.Tools.Adb.Devices
{
	/// <summary>
	/// 
	/// </summary>
	public class PropHandler
	{
		/// <summary>
		/// 设备序列号
		/// </summary>
		private readonly string serial;

        /// <summary>
        /// 是否可以执行命令
        /// </summary>
	    private readonly Action<ErrorResult> error;

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="serial"></param>
	    /// <param name="error"></param>
	    public PropHandler(string serial, Action<ErrorResult> error)
		{
		    this.serial = serial;
		    this.error = error;
		}

		/// <summary>
		/// 获取设备配置信息
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string GetProp(string name)
		{
		    return ShellExecuter.Execute(serial, "getprop " + name, error);
		}

        /// <summary>
        /// 设备是否被ROOT过
        /// </summary>
        /// <returns></returns>
	    public bool IsRoot()
	    {
	        var str = ShellExecuter.SuExecute(serial, "lsd", error);
	        return !string.IsNullOrEmpty(str) && str.ToLower().Contains("root");
	    }

		// 获取厂商名称
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetBrand()
		{
			return GetProp("ro.product.brand");
		}

		// 获取设备型号
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetModel()
		{
			return GetProp("ro.product.model");
		}

		// 获取Android版本号
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetRelease()
		{
			return GetProp("ro.build.version.release");
		}

		// 获取设备SDK
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetSdkVersion()
		{
			return GetProp("ro.build.version.sdk");
		}
	}
}
