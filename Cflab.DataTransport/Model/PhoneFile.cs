using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cflab.DataTransport.Model {
    public abstract class UrlInfo : Info {
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long Size { get; set; }
    }

}
