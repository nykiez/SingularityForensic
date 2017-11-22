using System;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
	public class Gps : IInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 海拔
        /// </summary>
        public double Altitude { get; set; }

        /// <summary>
        /// 加速度
        /// </summary>
        public double Accuracy { get; set; }
    }
}
