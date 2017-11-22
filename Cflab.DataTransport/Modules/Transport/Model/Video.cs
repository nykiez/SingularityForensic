using System;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
	public class Video : UrlInfo
    {
        /// <summary>
        /// 相册
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
    }
}
