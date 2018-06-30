using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.App
{
    /// <summary>
    /// 应用程序;
    /// </summary>
    public interface IAppService {
        /// <summary>
        /// 获取App名称;
        /// </summary>
        string AppName { get; }

        /// <summary>
        /// 获取程序的AppData路径(对应本程序);
        /// </summary>
        string AppDataFolder { get; }

        /// <summary>
        /// 临时目录;
        /// </summary>
        string AppTempFolder { get; }

        /// <summary>
        /// 程序存储资源文件的目录;
        /// </summary>
        string AppResourceFolder { get; }

        /// <summary>
        /// 当前字符集;
        /// </summary>
        Encoding AppEncoding { get; }

        /// <summary>
        /// 获取配置文件配置节的值;
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        string GetSettingValue(string keyName);

        /// <summary>
        /// 设定配置文件配置节的值;
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        void SetSettingValue(string keyName, string value);
    }

    public class AppService : GenericServiceStaticInstance<IAppService> {
        public static string AppName => Current?.AppName;
        public static string AppDataFolder => Current?.AppDataFolder;
        public static string AppTempFolder => Current?.AppTempFolder;
        public static string AppResourceFolder => Current?.AppResourceFolder;
    }
}
