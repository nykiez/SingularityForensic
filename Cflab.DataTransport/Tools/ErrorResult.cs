// ReSharper disable InconsistentNaming

using System;

namespace Cflab.DataTransport.Tools
{
	/// <summary>
	/// 错误时的返回结果
	/// </summary>
	public class ErrorResult
	{
	    /// <summary>
		/// 代码
		/// </summary>
		public int Code { get; set; }

		/// <summary>
		/// 附加信息
		/// </summary>
		public string Message { get; set; }

        /// <summary>
        /// 执行错误回调
        /// </summary>
        /// <param name="error"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
	    public static void InvokeError(Action<ErrorResult> error,int code, string msg)
	    {
	        var result = new ErrorResult
	        {
	            Code = code,
	            Message = msg
	        };
	        error?.Invoke(result);
	    }
	}

    #region 公共错误代码
    /// <summary>
    /// 公共错误代码
    /// </summary>
    public static class CommonError
    {
        /// <summary>
        /// 文件不存在
        /// </summary>
        public static int FileNotExists = 9001;

        /// <summary>
        /// 目录不存在或创建失败
        /// </summary>
        public static int InvalidDirectory = 9002;

        /// <summary>
        /// 写入文件失败
        /// </summary>
        public static int FailToWrite = 9003;

        /// <summary>
        /// 参数不能为空
        /// </summary>
        public static int NullParameter = 9004;
    }
    #endregion

    #region 初始化错误

    public static class InitError
    {
        /// <summary>
        /// Adb文件不存在
        /// </summary>
        public static int AdbNotExists = 1001;

        /// <summary>
        /// Adb端口被占用
        /// </summary>
        public static int InvalidAdbPort = 1101;
    }

    #endregion

    #region 连接错误

    public static class ConnectError
    {
        /// <summary>
        /// 无法连接到ADB服务
        /// </summary>
        public static int AdbServerUnable = 2001;

        /// <summary>
        /// 读取AdbResult时返回失败
        /// </summary>
        public static int AdbResultFail = 2002;

        /// <summary>
        /// 未能进入Transport模式
        /// </summary>
        public static int EnterTransportFail = 2003;

        /// <summary>
        /// 未能进入Tcp端口模式
        /// </summary>
        public static int EnterTcpportFail = 2004;

        /// <summary>
        /// 执行立即退出的Shell命令失败
        /// </summary>
        public static int ShellExitWithError = 2005;

        /// <summary>
        /// Apk缺少相关权限
        /// </summary>
        public static int ApkLackPermisions = 2101;

        /// <summary>
        /// 
        /// </summary>
        public static int EnterSyncFail = 2102;
    }
    #endregion

    #region 执行Shell命令错误

    public static class ShellError
    {
        
    }

    #endregion

    #region 备份错误

    public static class BackupResult
    {
        /// <summary>
        /// 执行ADB备份返回错误
        /// </summary>
        public static int EnterAdbBackupFail = 4001;
        
        /// <summary>
        /// 执行ADB备份后，用户取消
        /// </summary>
        public static int UserCancel = 4002;
    }
  
    #endregion
}
