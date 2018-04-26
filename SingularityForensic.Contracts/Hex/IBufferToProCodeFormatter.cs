using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hex {
    /// <summary>
    /// 缓冲区转化为格式化代码契约;
    /// </summary>
    public interface IBufferToCodeFormatter {
        /// <summary>
        /// 格式化代码;
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        string FormatAsCode(byte[] buffer);
        /// <summary>
        /// 格式器标识;
        /// </summary>
        string GUID { get; }
        /// <summary>
        /// 语言种类;
        /// </summary>
        string CodeLanguageName { get; }
        int Sort { get; }
    }
    

}
