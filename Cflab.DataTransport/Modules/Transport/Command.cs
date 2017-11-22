using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Cflab.DataTransport.Modules.Transport
{
	internal class Command
	{

		/// <summary>
		/// 所获去的数据的条目数
		/// </summary>
		internal const int INFO_COUNT = 61;
		/// <summary>
		/// 数据已经收到
		/// </summary>
		internal const int INFO_RECIVED = 62;

		/// <summary>
		/// 文件大小
		/// </summary>
		internal const int FILE_SIZE = 71;
		/// <summary>
		/// 文件已经收到
		/// </summary>
		internal const int FILE_RECIVED = 72;

		/// <summary>
		/// 测试；连接状态
		/// </summary>
		internal const int TEST_CONNECTION = 101;

		/// <summary>
		/// 手机端软件未获取到授权
		/// </summary>
		internal const int LACK_PERMISITION = 102;

		/// <summary>
		/// 链接测试成功
		/// </summary>			
		// ReSharper disable once UnusedMember.Local
		internal const int SUCCESS = 200;

		/// <summary>
		/// 出现错误
		/// </summary>
		// ReSharper disable once UnusedMember.Local
		internal const int ERROR = 404;

		/// <summary>
		/// 当前命令执行完毕
		/// </summary>
		internal const int CLIENT_OVER = 998;
		/// <summary>
		/// 传输结束
		/// </summary>
		// ReSharper disable once UnusedMember.Local
		internal const int SERVER_OVER = 999;

        /// <summary>
        /// 命令编号
        /// </summary>
		public int Cmd { get; set; }

        /// <summary>
        /// 命令参数
        /// </summary>
		public string Arg { get; set; }

        /// <summary>
        /// 构建命令字符串
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("${").Append(Cmd).Append("&&");
			if (string.IsNullOrWhiteSpace(Arg))
			{
				Arg = "null";
			}
			sb.Append(Arg).Append("}\n");
			return sb.ToString();
		}

        /// <summary>
        /// 解析命令字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
		public static Command Parse(string str)
		{
            // 示例 ${777&&false}
			const string regx = @"^\$\{(?<cmd>\d+)&&(?<arg>.*)}";
			var match = Regex.Match(str, regx);
			if (!match.Success)
			{
				return null;
			}
			try
			{
				var cmd = int.Parse(match.Groups["cmd"].Value);
				var arg = match.Groups["arg"].Value;
				return new Command
				{
					Cmd = cmd,
					Arg = arg
				};
			}
			catch (Exception)
			{
				return null;
			}
		}

        /*
            public static final int GET_BASIC = 0x01;
            public static final int GET_PACKAGES = 0x02;
            public static final int GET_CONTACTS = 0x03;
            public static final int GET_CALLLOG = 0x04;
            public static final int GET_MESSAGE = 0x05;
            public static final int GET_GPS = 0x06;
            public static final int GET_IMAGE = 0x07;
            public static final int GET_AUDIO = 0x08;
            public static final int GET_VIDEO = 0x09;
            public static final int GET_CALENDAR = 0x0A;
            // 获取文件列表
            public static final int GET_FILELIST = 0x0B;
            // 获取文件
            public static final int GET_FILE = 0x0C;
             */
	    /// <summary>
	    /// 通过类型名创建Info获取命令
	    /// </summary>
	    /// <param name="name"></param>
	    /// <param name="arg"></param>
	    /// <returns></returns>
	    public static Command Create(string name, string arg = null)
        {
            var cmd = new Command
            {
                Arg = string.IsNullOrEmpty(arg) ? "Null" : arg
            };
            switch (name)
            {
                // 基础信息
                case "Basic":
                    cmd.Cmd = 0x01;
                    break;
                // 安装包
                case "Package":
                    cmd.Cmd = 0x02;
                    break;
                // 联系人
                case "Contact":
                    cmd.Cmd = 0x03;
                    break;
                // 通话记录
                case "CallLog":
                    cmd.Cmd = 0x04;
                    break;
                // 短信
                case "Sms":
                    cmd.Cmd = 0x05;
                    break;
                // GPS信息
                case "Gps":
                    cmd.Cmd = 0x06;
                    break;
                // 图片
                case "Image":
                    cmd.Cmd = 0x07;
                    break;
                // 音频
                case "Audio":
                    cmd.Cmd = 0x08;
                    break;
                // 视频
                case "Video":
                    cmd.Cmd = 0x09;
                    break;
                // 日历
                case "Calendar":
                    cmd.Cmd = 0x0A;
                    break;
                // 文件列表
                case "AnFile":
                    cmd.Cmd = 0x0B;
                    break;
                // 下载文件
                case "File":
                    cmd.Cmd = 0x0C;
                    break;
                default:
                    cmd = null;
                    break;
            }
            return cmd;
        }
	}
}
