using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.InteropServices;

/// <summary>
/// 非托管的各种状态封装,例如分区/设备管理;
/// </summary>
namespace SingularityForensic.BaseDevice {
    /// <summary>
    /// 基础设备(Dos/Gpt)信息管理器,用于处理非托管的状态保存;
    /// </summary>
    internal class UnmanagedBasicDeviceManager : IUnmanagedBasicDeviceManager {
        private const string partAsm = "PartitionManager.dll";
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static void Partition_Exit(IntPtr stPartition);
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Partition_Init(IntPtr stStream);

        private bool _disposed;
        public void Dispose() {
            if (_disposed) {
                throw new ObjectDisposedException(nameof(UnmanagedBasicDeviceManager));
            }

            //释放适配器实例;
            if (BasicDevicePtr != IntPtr.Zero) {
                try {
                    Partition_Exit(BasicDevicePtr);
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            //释放非托管接口管理单元;
            try {
                StreamAdpater?.Dispose();
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            _disposed = true;
        }

        /// <summary>
        /// 通过一个流实例获取非托管管理单元适配器实例以及对应的适配器实例;
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public UnmanagedBasicDeviceManager(Stream stream) {
            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            try {
                StreamAdpater = new UnmanagedStreamAdapter(stream);
                BasicDevicePtr = Partition_Init(StreamAdpater.StreamPtr);
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }

        /// <summary>
        /// 非托管单元指针;
        /// </summary>
        public IntPtr BasicDevicePtr { get; }

        /// <summary>
        /// 流适配器实例;
        /// </summary>
        public UnmanagedStreamAdapter StreamAdpater { get; }
    }

    [Export(typeof(IUnmanagedBasicDeviceManagerFactory))]
    internal class UnmanagedBasicDeviceManagerFactoryImpl : IUnmanagedBasicDeviceManagerFactory {
        public IUnmanagedBasicDeviceManager Create(Stream stream) {
            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            return new UnmanagedBasicDeviceManager(stream);
        }
    }
}
