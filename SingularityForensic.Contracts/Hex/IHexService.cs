using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hex {
    /// <summary>
    /// 十六进制服务提供器,此单位唯一;
    /// </summary>
    public interface IHexService {
        /// <summary>
        /// 创建十六进制上下文;
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        IHexDataContext CreateNewHexDataContext(Stream stream = null);

        /// <summary>
        /// 加载十六进制上下文,这将触发事件通知;
        /// 此方法是加载右键,提示等插件化功能的前提条件,非常重要;
        /// </summary>
        /// <param name="hexDataContext"></param>
        void LoadHexDataContext(IHexDataContext hexDataContext);
    }

    public class HexService:GenericServiceStaticInstance<IHexService> {

    }

}
