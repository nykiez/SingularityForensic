using System;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
	public class Sms : IInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public long Date { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 是否为已读
        /// </summary>
        public int Read { get; set; }

        /// <summary>
        /// 协议
        /// </summary>
        public int Protocol { get; set; }
    }
}
