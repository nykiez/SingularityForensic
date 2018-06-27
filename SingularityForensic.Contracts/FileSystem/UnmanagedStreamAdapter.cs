using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// 非托管流适配器，可映射任意流至非托管环境下的一个UnmanagedStream对象;
    /// <!--本类实现了IDisposable,实例对象保存在Static队列中,当且仅当在调用了Dispose()后实例才可能被回收-->
    /// </summary>
    public partial class UnmanagedStreamAdapter : IDisposable {
        public UnmanagedStreamAdapter(Stream stream) {
            OriStream = stream ?? throw new ArgumentNullException(nameof(stream));
            StreamPtr = CreateUnManagedStream();

            _instances.Add(this);

            InitializePtr();
        }

        //在向非托管环境传递委托实例时,如无其它托管引用该实例,
        //由于非托管环境在托管堆标记委托实例的引用,
        //则委托实例可能在被下一次垃圾回收时被回收,
        //所以此处需要单独引用对应的该委托实例,已确保委托实例不会被不正确地回收。
        List<Delegate> delegates = new List<Delegate>();
        
        private long OnGetPos() => OriStream.Position;
        private void OnSetPos(long pos) {
#if DEBUG
            if(pos >= 13878934528 && OriStream.Length == 13878934528) {
                
            }
#endif
            if(pos > OriStream.Length) {
                LoggerService.WriteCallerLine($"{nameof(pos)} out of range.({nameof(pos)}:{pos},available length:{OriStream.Length}");
                OriStream.Position = pos;
                return;
            }
            else if(pos < 0){
                LoggerService.WriteCallerLine($"{nameof(pos)} can't be less than zero.({nameof(pos)}:{pos},available length:{OriStream.Length}");
                return;
            }
            
            OriStream.Position = pos;
        }
        private long OnGetLength() => OriStream.Length;
        private bool OnCanWrite() => OriStream.CanWrite;
        private bool OnCanRead() => OriStream.CanRead;

        private int OnWrite(IntPtr buffer, int count) {
            if (writeBuffer.Length < count) {
                writeBuffer = new byte[count];
            }

            var oldPos = OriStream.Position;
            OriStream.Write(writeBuffer, 0, count);
            Marshal.Copy(buffer, writeBuffer, 0, count);
            return (int)(OriStream.Position - oldPos);
        }

        private int OnRead(IntPtr buffer, int count) {
            if (this.readBuffer.Length < count) {
                this.readBuffer = new byte[count];
            }

            var readCount = OriStream.Read(readBuffer, 0, count);

#if DEBUG
            if (count == 30380032) {

            }
            
            if(count != readCount && OriStream.Length == 13878934528) {

            }
#endif

            Marshal.Copy(readBuffer, 0, buffer, readCount);
            return readCount;
        }

        //private int OnRead(byte[] buffer, int count) {
        //    return OriStream.Read(buffer, 0, count);
        //}

        //初始化非托管指针;
        private void InitializePtr() {
            //位置委托;
            SetInt64Delegate setPos = OnSetPos;
            GetInt64Delegate getPos = OnGetPos;
            delegates.Add(setPos);
            delegates.Add(getPos);

            SetPositionFunc(
                StreamPtr,
                getPos,
                setPos
            );

            //长度委托;
            GetInt64Delegate getLength = OnGetLength;
            delegates.Add(getLength);
            SetGetLengthFunc(StreamPtr, getLength);

            //可读/写委托;
            GetBoolDelegate canRead = OnCanRead;
            GetBoolDelegate canWrite = OnCanWrite;
            delegates.Add(canRead);
            delegates.Add(canWrite);
            SetCanReadFunc(StreamPtr, canRead);
            SetCanWriteFunc(StreamPtr, canWrite);
            
            //写入委托;
            WriteDelegate write = OnWrite;
            delegates.Add(write);
            SetWriteFunc(StreamPtr, write);
            
            //读取委托;
            ReadDelegate read = OnRead;
            delegates.Add(read);
            SetReadFunc(StreamPtr, read);
        }

        private byte[] readBuffer = new byte[4096];
        private byte[] writeBuffer = new byte[4096];

        public Stream OriStream { get; }

        private IntPtr _streamPtr;
        public IntPtr StreamPtr {
            get {
                if (_disposed) {
                    throw new ObjectDisposedException(nameof(UnmanagedStreamAdapter));
                }
                return _streamPtr;
            }
            private set => _streamPtr = value;
        }

        private bool _disposed;

        /// <summary>
        /// 当且仅当在调用了Dispose()后实例才可能被回收
        /// </summary>
        public void Dispose() {
            CloseStream(StreamPtr);
            StreamPtr = IntPtr.Zero;
            _disposed = true;
            if (_instances.Contains(this)) {
                _instances.Remove(this);
            }
        }

        //所有实例必须依托在GCHandle
        //非托管环境意外进行了调用,引发了非法访问内存的错误;
        //只有在调用Dispose方法后,才可解除引用,使得垃圾回收按照预期正常执行;
        private static List<UnmanagedStreamAdapter> _instances = new List<UnmanagedStreamAdapter>();

        ~UnmanagedStreamAdapter() {
            if (!_disposed) {
                Dispose();
            }
        }
    }

    public partial class UnmanagedStreamAdapter {
        private const string streamAsm = "StreamExtension.dll";

        //[return: MarshalAs(UnmanagedType.I8)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long GetInt64Delegate();

        //[return: MarshalAs(UnmanagedType.I8)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SetInt64Delegate(long int64);

        //[return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GetBoolDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int WriteDelegate(IntPtr data, int count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int ReadDelegate(IntPtr buffer, int count);

        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreateUnManagedStream();

        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetGetLengthFunc(IntPtr stream, [MarshalAs(UnmanagedType.FunctionPtr)] GetInt64Delegate getLengthFunc);

        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetPositionFunc(IntPtr stream, [MarshalAs(UnmanagedType.FunctionPtr)] GetInt64Delegate getPositionFunc,
            [MarshalAs(UnmanagedType.FunctionPtr)]SetInt64Delegate setPositionFunc);

        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetCanReadFunc(IntPtr stream, [MarshalAs(UnmanagedType.FunctionPtr)]GetBoolDelegate canReadFunc);

        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetCanWriteFunc(IntPtr stream, GetBoolDelegate canWriteFunc);

        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetWriteFunc(IntPtr stream, [MarshalAs(UnmanagedType.FunctionPtr)]WriteDelegate writeFunc);

        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetReadFunc(IntPtr stream, [MarshalAs(UnmanagedType.FunctionPtr)]ReadDelegate readFunc);

        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void CloseStream(IntPtr stream);

    }
}
