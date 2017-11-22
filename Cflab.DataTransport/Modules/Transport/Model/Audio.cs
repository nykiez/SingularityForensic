using System;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
	public class Audio : UrlInfo
    {
        /// <summary>
        /// 所属相册
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artlist { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 时常
        /// </summary>
        public int Duration { get; set; }
    }
}