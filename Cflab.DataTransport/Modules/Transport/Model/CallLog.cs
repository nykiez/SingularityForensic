using System;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
	public class CallLog : IInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 号码
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public long Date { get; set; }

        /// <summary>
        /// 时常
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
    }
}
