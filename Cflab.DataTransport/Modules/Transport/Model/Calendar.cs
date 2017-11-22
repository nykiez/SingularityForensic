using System;

namespace Cflab.DataTransport.Modules.Transport.Model
{
    [Serializable]
    public class Calendar : IInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public long Start { get; set; }


        /// <summary>
        /// 结束时间
        /// </summary>
        public long End { get; set; }
    }
}
