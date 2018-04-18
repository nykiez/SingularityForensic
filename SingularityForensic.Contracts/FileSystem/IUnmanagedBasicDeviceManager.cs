using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// 基础设备(Dos/Gpt)信息管理器,用于处理非托管的状态保存;
    /// </summary>
    public interface IUnmanagedBasicDeviceManager:IDisposable {
        IntPtr BasicDevicePtr { get; }
        UnmanagedStreamAdapter StreamAdpater { get; }
    }

    /// <summary>
    /// 提供单元契约,某个程序设计人员所有解析模块需要依赖同种行为,此工厂是为防止各个文件系统模块可能共用的深度耦合;
    /// </summary>
    public interface IUnmanagedBasicDeviceManagerFactory {
        IUnmanagedBasicDeviceManager Create(Stream stream);
    }

    public class UnMgdBasicDeviceManagerFactory: GenericServiceStaticInstance<IUnmanagedBasicDeviceManagerFactory> {
        public static IUnmanagedBasicDeviceManager Create(Stream stream) => Current?.Create(stream);
    }

}
