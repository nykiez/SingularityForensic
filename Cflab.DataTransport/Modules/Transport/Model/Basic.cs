using System;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
	public class Basic : IInfo
    {
        /// <summary>
        /// SIM卡状态
        /// </summary>
        public int State { get; set; }

        public string Imei { get; set; }

        public string Imsi { get; set; }

        public string Release { get; set; }

        /// <summary>
        /// 运营商识别号
        /// </summary>
        public string Inet { get; set; }

        /// <summary>
        /// SIM卡序列号
        /// </summary>
        public string Isim { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// CPU ABI
        /// </summary>
        public string Abi { get; set; }

        /// <summary>
        /// 时区
        /// </summary>
        public int Zone { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 处理器型号
        /// </summary>
        public string Board { get; set; }


        public string Hardware { get; set; }

        public string Device { get; set; }

        //public string WifiMac { get; set; }

        //public string BtMac { get; set; }
    }
}