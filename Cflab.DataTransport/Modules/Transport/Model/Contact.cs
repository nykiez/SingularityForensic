using System;
using System.Collections.Generic;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
	public class Contact : IInfo
    {
        /// <summary>
        /// 联系人ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 号码列表
        /// </summary>
        public List<PhoneNumber> Numbers { get; set; }

        /// <summary>
        /// 邮件列表
        /// </summary>
        public List<EmailAddress> Emails { get; set; }

		/// <summary>
		/// 号码类
		/// </summary>
		[Serializable]
		public class PhoneNumber
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 号码
            /// </summary>
            public string Number { get; set; }

            /// <summary>
            /// 带国家区号的号码
            /// </summary>
            public string FullNumber { get; set; }

            /// <summary>
            /// 地址
            /// </summary>
            public string Location { get; set; }
        }

		/// <summary>
		/// 邮箱类
		/// </summary>
		[Serializable]
		public  class EmailAddress
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 邮箱
            /// </summary>
            public string Address { get; set; }
        }
    }
}
