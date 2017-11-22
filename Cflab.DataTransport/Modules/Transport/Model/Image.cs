using System;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
	public class Image : UrlInfo
    {

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

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