using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFC.Parse.Modules.Contracts;
using CDFC.Util.PInvoke;
using EventLogger;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Modules.Pictures {
    //八爷签名搜索;
    public partial class RecompositeSearcher:IFileSearcher {
        public RecompositeSearcher(BlockDeviceFile device, int secSize = 512) {
            if (device is IHaveHandle) {
                Handle = (device as IHaveHandle).Handle;
            }
            else {
                Logger.WriteLine($"{nameof(PictureSearcher)}->{nameof(PictureSearcher)}:not a valid device:{device.GetType()}");
                throw new NotSupportedException($"{nameof(device)} Type is not supported!");
            }
            SecSize = secSize;
            fileCountPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));
            Marshal.WriteInt64(fileCountPtr, 0);
            Device = device;
        }
        public int SecSize { get; private set; }
        public SafeFileHandle Handle { get; private set; }
        public BlockDeviceFile Device { get; }
        public long CurOffset {
            [HandleProcessCorruptedStateExceptions]
            get {
                try {
                    var s = photo_get_offset();
                    return (long)s;
                }
                catch {
                    return 0;
                }
            }
        }

        public int CurFileCount {
            get {
                try {
                    if (fileCountPtr != IntPtr.Zero) {
                        return (int)Marshal.ReadInt64(fileCountPtr);
                    }
                    return 0;
                }
                catch {
                    return 0;
                }
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public List<IFileNode> GetFileList(string extensionName) {
            IntPtr fileptrNode = IntPtr.Zero;
            IntPtr filePtr = IntPtr.Zero;
            var extensionPtr = Marshal.StringToHGlobalAnsi(extensionName);
            try {
                var nodes = new List<IFileNode>();
                filePtr = photo_getflie(extensionPtr);
                fileptrNode = filePtr;
                while (fileptrNode != IntPtr.Zero) {
                    var stNode = fileptrNode.GetStructure<StPhotoFileNode>();
                    var node = new FileNode { StPhotoFileNode = stNode };
                    nodes.Add(node);

                    if (stNode.Next == fileptrNode) {
                        break;
                    }
                    fileptrNode = stNode.Next;
                }
                return nodes;
            }
            catch {
                return null;
            }
            finally {
                try {
                    Marshal.FreeHGlobal(extensionPtr);
                    photo_exit(filePtr);
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(PictureSearcher)}->{nameof(GetFileList)}:{extensionName}-{ex.Message}");
                }
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public bool SearchStart(long startLBA, long byteCount) {
            try {
                return photo_searchfile(Handle, (ulong)startLBA, SecSize, (ulong)byteCount, fileCountPtr);
            }
            catch {
                return false;
            }
        }
        
        private IntPtr fileCountPtr = IntPtr.Zero;

        public bool Stopping { get; private set; }
        [HandleProcessCorruptedStateExceptions]
        public bool Stop() {
            try {
                photo_stop();
                Stopping = true;
                return true;
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(PictureSearcher)}->{nameof(Stop)}:{ex.Message}");
                return false;
            }
        }
        [HandleProcessCorruptedStateExceptions]
        public void Dispose() {
            try {
                photo_first();
                if (fileCountPtr != IntPtr.Zero) {
                    Marshal.FreeHGlobal(fileCountPtr);
                    fileCountPtr = IntPtr.Zero;
                }
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(PictureSearcher)}->{nameof(Dispose)}:{ex.Message}");
            }
        }
    }
    public partial class RecompositeSearcher {
        [DllImport("cdfcphoto.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool photo_searchfile(SafeFileHandle hFile, ulong startSec, int secSize, ulong nSize, IntPtr fileCount);
        [DllImport("cdfcphoto.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr photo_getflie(IntPtr extentsionPtr);
        [DllImport("cdfcphoto.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern ulong photo_get_offset();
        [DllImport("cdfcphoto.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void photo_first();
        [DllImport("cdfcphoto.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void photo_exit(IntPtr stFile);
        [DllImport("cdfcphoto.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void photo_stop();
    }
}
