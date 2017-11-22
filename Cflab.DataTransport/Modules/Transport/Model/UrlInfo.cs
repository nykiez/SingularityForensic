using System;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
	public abstract class UrlInfo : IInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 尺寸
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 添加到媒体库的日期
        /// </summary>
        public long DateAdd { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public long DateModif { get; set; }
    }
}
