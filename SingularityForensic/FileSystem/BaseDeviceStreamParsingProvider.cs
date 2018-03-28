using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.FileSystem {
    
    internal abstract class BaseDeviceStreamParsingProvider : IStreamParsingProvider {
        protected const string partAsm = "PartitionManager.dll";
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        protected extern static IntPtr Partition_Init(IntPtr stStream);
        
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        protected extern static bool Partition_B_Gpt(IntPtr stPartition);

        //StGptPTable* Partition_Get_GptPTable(IntPtr stPartition);
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        protected extern static IntPtr Partition_Get_GptPTable(IntPtr stPartition);
        

        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        protected extern static void Partition_Exit(IntPtr stPartition);

        public abstract int Order { get; }

        //子类实现签名断言;
        protected abstract bool AssertIsValid(IntPtr streamPtr);

        public bool CheckIsValidStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            var tuple = GetManagerAndStreamPtr(stream);
            if(tuple == null) {
                LoggerService.WriteCallerLine($"{nameof(tuple)} can't be null.");
                return false;
            }

            (UnmanagedStreamAdapter adapter,IntPtr mgrPtr) = tuple.Value;

            //判断是否是符合"签名";
            var isDos = false;
            try {
                isDos = AssertIsValid(mgrPtr);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
            
            //释放适配器实例;
            try {
                if(adapter == null) {
                    LoggerService.WriteCallerLine($"{nameof(adapter)} can't be null.");
                }
                else {
                    adapter.Dispose();
                }
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            //释放解析接口管理单元;
            try {
                if(mgrPtr == IntPtr.Zero) {
                    LoggerService.WriteCallerLine($"{nameof(mgrPtr)} can't be Zero");
                }
                else {
                    Partition_Exit(mgrPtr);
                }
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
            

            return isDos;
        }

        /// <summary>
        /// 通过一个流获取非托管管理单元适配器实例以及对应的适配器实例;
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        protected (UnmanagedStreamAdapter adpater,IntPtr mgrPtr)? GetManagerAndStreamPtr(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            var adpter = new UnmanagedStreamAdapter(stream);
            var mgrPtr = IntPtr.Zero;

            //构建解析管理单元;
            try {
                mgrPtr = Partition_Init(adpter.StreamPtr);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            if (mgrPtr == IntPtr.Zero) {
                LoggerService.WriteCallerLine($"{nameof(mgrPtr)} can't be null.");
            }

            return (adpter, mgrPtr);
        }

        public abstract FileBase ParseStream(Stream stream, string name, XElement xElem, ProgressReporter reporter);
    }
         
}
