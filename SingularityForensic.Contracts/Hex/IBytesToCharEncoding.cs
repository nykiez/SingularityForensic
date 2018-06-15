using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hex {
    /// <summary>
    /// 字符转换器;
    /// </summary>
    public interface IBytesToCharEncoding {
        /// <summary>
        /// 字符的字节长度;
        /// </summary>
        int BytePerChar { get; }

        char Convert(byte[] bytesToConvert);

        /// <summary>
        /// 格式器标识;
        /// </summary>
        string GUID { get; }

        /// <summary>
        /// 编码种类;
        /// </summary>
        string EncodingName { get; }

        /// <summary>
        /// 排序;
        /// </summary>
        int Sort { get; }
    }
}
