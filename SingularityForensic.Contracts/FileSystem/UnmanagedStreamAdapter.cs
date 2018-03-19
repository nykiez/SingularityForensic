using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Parsing {

    /// <summary>
    /// //非托管流适配器;
    /// <!--本类实现了IDisposable,必须在调用了Dispose()后实例才可能被回收-->
    /// </summary>
    public class UnmanagedStreamAdapter : IDisposable {
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
        private delegate int ReadDelegate(IntPtr data, int count);

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

        public UnmanagedStreamAdapter(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            OriStream = stream;
            _streamPtr = CreateUnManagedStream();

            _instances.Add(this);

            InitializePtr();
        }

        //在向非托管环境传递委托实例时,如无其它托管引用该实例,
        //由于非托管环境在托管堆标记委托实例的引用,
        //则委托实例可能在被下一次垃圾回收时被回收,
        //所以此处需要单独引用对应的该委托实例,已确保委托实例不会被不正确地回收。
        List<object> delegates = new List<object>();

        //初始化非托管指针;
        private void InitializePtr() {
            GetInt64Delegate getLengthFunc = () => OriStream.Length;
            delegates.Add(getLengthFunc);
            SetGetLengthFunc(StreamPtr, getLengthFunc);

            GetInt64Delegate getPosFunc = () => OriStream.Position;
            SetInt64Delegate setPosFunc = pos => OriStream.Position = pos;

            SetPositionFunc(
                StreamPtr,
                getPosFunc,
                setPosFunc
            );

            delegates.Add(getPosFunc);
            delegates.Add(setPosFunc);

            GetBoolDelegate canWriteFunc = () => OriStream.CanWrite;
            SetCanWriteFunc(StreamPtr, canWriteFunc);
            delegates.Add(canWriteFunc);

            WriteDelegate writeFunc = (buffer, count) => {
                if (writeBuffer.Length < count) {
                    writeBuffer = new byte[count];
                }

                var oldPos = OriStream.Position;
                OriStream.Write(writeBuffer, 0, count);
                Marshal.Copy(buffer, writeBuffer, 0, count);
                return (int)(OriStream.Position - oldPos);
            };

            SetWriteFunc(StreamPtr, writeFunc);
            delegates.Add(writeFunc);

            GetBoolDelegate canReadFunc = () => OriStream.CanRead;
            SetCanReadFunc(StreamPtr, canReadFunc);
            delegates.Add(canReadFunc);

            ReadDelegate readDel = (buffer, count) => {
                if (this.readBuffer.Length < count) {
                    this.readBuffer = new byte[count];
                }

                var readCount = OriStream.Read(readBuffer, 0, count);
                Marshal.Copy(readBuffer, 0, buffer, readCount);
                return readCount;
            };

            SetReadFunc(StreamPtr, readDel);
            delegates.Add(readDel);
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
        }

        private bool _disposed;
        public void Dispose() {
            CloseStream(StreamPtr);
            _streamPtr = IntPtr.Zero;
            _disposed = true;
            if (_instances.Contains(this)) {
                _instances.Remove(this);
            }
        }

        //所有实例必须保存在本列表中,以防止垃圾回收机制回收了实例后;
        //非托管环境仍然进行了调用,引发了非法访问内存的错误;
        //只有在调用Dispose方法后,才可解除引用,使得垃圾回收按照预期正常执行;
        private static List<UnmanagedStreamAdapter> _instances = new List<UnmanagedStreamAdapter>();

        ~UnmanagedStreamAdapter() {
            if (!_disposed) {
                Dispose();
            }
        }
    }
}
