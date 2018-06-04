using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Ext {
    class ExtUnmanagedManager : IDisposable {
        private const string extAsm = "ExtRecover.dll";
        
        [DllImport(extAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr ExtX_Init(IntPtr stStream);

        [DllImport(extAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static void ExtX_Exit(IntPtr stPartition);

        public ExtUnmanagedManager(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            try {
                StreamAdpater = new UnmanagedStreamAdapter(stream);
                ExtManagerPtr = ExtX_Init(StreamAdpater.StreamPtr);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }

        private bool _disposed;
        public void Dispose() {
            if (_disposed) {
                throw new ObjectDisposedException(nameof(ExtUnmanagedManager));
            }

            //释放适配器实例;
            if (ExtManagerPtr != IntPtr.Zero) {
                try {
                    ExtX_Exit(ExtManagerPtr);
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

        public IntPtr ExtManagerPtr { get; }

        /// <summary>
        /// 流适配器实例;
        /// </summary>
        public UnmanagedStreamAdapter StreamAdpater { get; }
    }
}
