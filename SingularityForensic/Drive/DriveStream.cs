using Microsoft.Win32.SafeHandles;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace SingularityForensic.Drive {
    //特殊文件流(本地盘符/硬盘等数据);
    //class DriveStream : FileStream {
    //    /// <summary>
    //    /// 特殊文件流的构造方法;
    //    /// </summary>
    //    /// <param name="handle">文件句柄</param>
    //    /// <param name="sectorSize">扇区大小</param>
    //    public DriveStream(SafeFileHandle handle,int sectorSize) : base(handle, FileAccess.Read) {
    //        if(sectorSize == 0) {
    //            throw new ArgumentException($"Invalid parameter {nameof(sectorSize)}-{sectorSize}");
    //        }

    //        SectorSize = sectorSize;
    //        _position = base.Position;
    //    }

    //    //由于无法直接通过Handle/SafeFileHandle获得长度,在本程序集内部直接指定长度;
    //    internal long InternalLength { get; set; }
    //    public override long Length => InternalLength;

    //    //对外所描述的指针位置;
    //    private long _position;
    //    //由于调整位置时,若传入了非扇区整数倍的位置,会导致异常,Position在此须特殊处理;
    //    public override long Position {
    //        get => _position;
    //        set {
    //            if(_position >= Length) {
    //                throw new ArgumentOutOfRangeException(
    //                    "Position can't be greater than Length"
    //                    +$"{nameof(Length)}:{Length} {nameof(value)}{value}");
    //            }
    //            if(_position < 0) {
    //                throw new ArgumentOutOfRangeException(
    //                    "Position can't be less than zero"
    //                    + $"{nameof(value)}{value}");
    //            }

    //            _position = value;
    //        }
    //    }

    //    /// <summary>
    //    /// //为防止在Write/Read中分别编写同样的代码,减少错误率,编写了这个方法;
    //    /// </summary>
    //    /// <param name="readOrWriteFunc">读取/写入委托</param>
    //    /// <param name="array">缓冲区</param>
    //    /// <param name="offset">缓冲区偏移</param>
    //    /// <param name="count">读取/写入数量</param>
    //    /// <returns>成功读取/写入数量</returns>
    //    private int ReadOrWriteCore(
    //        Func<(byte[] array, int offset, int count), int> readOrWriteFunc,
    //        byte[] array,
    //        int offset,
    //        int count) {
    //        if (readOrWriteFunc == null) {
    //            throw new ArgumentNullException(nameof(readOrWriteFunc));
    //        }

    //        //成功读取/写入的数量;
    //        var readWriteCount = 0;
    //        //若当前位置与原始文件流位置一致(读入大小可以不受限制),则直接读取/写入;
    //        if (Position == base.Position) {
    //            readWriteCount = readOrWriteFunc((array, offset, count));
    //        }

    //        //若当前位置在扇区整数倍上,则可以直接调整原始文件流至对外位置并读取/写入;
    //        if (Position % SectorSize == 0) {
    //            base.Position = Position;
    //            readWriteCount = readOrWriteFunc((array, offset, count));
    //        }

    //        //否则,将间接跳转到Position位置并读取/写入;
    //        base.Position = Position / SectorSize * SectorSize;
    //        base.Read(TempBuffer, 0, (int)(Position % SectorSize));

    //        readWriteCount = readOrWriteFunc((array, offset, count));

    //        //手动调整外部位置;
    //        Position += readWriteCount;

    //        return readWriteCount;
    //    }

    //    //按照扇区整数倍读取;
    //    public override int Read(byte[] array, int offset, int count) {
    //        return ReadOrWriteCore(
    //            tuple => {
    //                return base.Read(tuple.array, tuple.offset, count);
    //            },
    //            array,
    //            offset,
    //            count
    //        );
    //    }

    //    //ReadByte并未调用Read(ReadCore),需单独重写;
    //    public override int ReadByte() {
    //        var bts = new byte[1];
    //        Read(bts,0,1);
    //        return bts[0];
    //    }

    //    //WriteByte并未调用Write(WriteCore),需单独重写;
    //    public override void WriteByte(byte value) {
    //        var bts = new byte[1];
    //        Write(bts, 0, 1);
    //    }

    //    //按照扇区整数倍写入;
    //    public override void Write(byte[] array, int offset, int count) {
    //        ReadOrWriteCore(
    //            tuple => {
    //                base.Write(tuple.array, tuple.offset, count);
    //                return 0;
    //            },
    //            array,
    //            offset,
    //            count
    //        );
    //    }

    //    //临时内存,用作存储读取/写入时间接跳转的存储;
    //    private byte[] _tempBuffer;
    //    private byte[] TempBuffer => _tempBuffer ?? (_tempBuffer = new byte[SectorSize - 1]);
    //    public int SectorSize { get; }
    //}

    class DriveStream : FileStream {
        [DllImport("DevManager.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static bool cdfc_devices_init();

        [DllImport("DevManager.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static long DevManager_read(SafeFileHandle hDisk, ulong nPosition, IntPtr szBuffer, long nSize, ref int nRetSize);

        [DllImport("DevManager.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static long DevManager_write(SafeFileHandle hDisk, ulong nPosition, IntPtr szBuffer, long nSize, ref int nRetSize);

        //[DllImport("DevManager.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //private extern static bool DevManager_SetPosition(HANDLE hDisk, unsigned __int64 nPosition );

        /// <summary>
        /// 特殊文件流的构造方法;
        /// </summary>
        /// <param name="handle">文件句柄</param>
        /// <param name="sectorSize">扇区大小</param>
        public DriveStream(SafeFileHandle handle, int sectorSize) : base(handle, FileAccess.Read) {
            if (sectorSize == 0) {
                throw new ArgumentException($"Invalid parameter {nameof(sectorSize)}-{sectorSize}");
            }

            SectorSize = sectorSize;
            _position = base.Position;
        }

        //由于无法直接通过Handle/SafeFileHandle获得长度,在本程序集内部直接指定长度;
        internal long InternalLength { get; set; }
        public override long Length => InternalLength;

        //对外所描述的指针位置;
        private long _position;
        public override long Position {
            get => _position;
            set {
                if (value >= Length) {
                    throw new ArgumentOutOfRangeException(
                        "Position can't be greater than Length"
                        + $"{nameof(Length)}:{Length} {nameof(value)}{value}");
                }
                if (value < 0) {
                    throw new ArgumentOutOfRangeException(
                        "Position can't be less than zero"
                        + $"{nameof(value)}{value}");
                }

                _position = value;
            }
        }

        //读写锁;
        private AutoResetEvent evt = new AutoResetEvent(true);
        
        /// <summary>
        /// 读取核心;
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private int ReadCore(IntPtr buffer, int count) {
            if (buffer == IntPtr.Zero) {
                throw new ArgumentNullException(nameof(buffer));
            }

            var timeOut = !evt.WaitOne(TimeSpan.FromMilliseconds(4000));
            var retSize = 0;

            if (timeOut) {
                throw new TimeoutException($"Read operation has been time out due to the {nameof(evt.WaitOne)}");
            }

            try {
                DevManager_read(this.SafeFileHandle, (ulong)this.Position, buffer, count, ref retSize);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            evt.Set();
            this.Position += retSize;
            return retSize;
        }


        /// <summary>
        /// 读取;
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(byte[] array, int offset, int count) {
            if (array == null) {
                throw new ArgumentNullException(nameof(array));
            }

            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            
            try {
                var buffer = Marshal.UnsafeAddrOfPinnedArrayElement(array, offset);
                if (buffer == null) {
                    throw new InvalidDataException($"{nameof(buffer)} is null.");
                }

                return ReadCore(buffer, count);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
            finally {
                handle.Free();
            }
            
            return 0;
        }
        
        //ReadByte并未调用Read(ReadCore),需单独重写;
        public override int ReadByte() {
            var btPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));
            if(btPtr == IntPtr.Zero) {
                return 0;
            }
            Marshal.WriteByte(btPtr, 0);
            try {
                ReadCore(btPtr, 1);
                var bt = Marshal.ReadByte(btPtr);
                return bt;
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                return 0;
            }
            finally {
                Marshal.FreeHGlobal(btPtr);
            }
        }

        //WriteByte并未调用Write(WriteCore),需单独重写;
        public override void WriteByte(byte value) {
            var bts = new byte[1];
            Write(bts, 0, 1);
        }

        /// <summary>
        /// 写入核心;
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        private void WriteCore(IntPtr buffer, int count) {
            if (buffer == null) {
                throw new ArgumentNullException(nameof(buffer));
            }

            var timeOut = !evt.WaitOne(TimeSpan.FromMilliseconds(4000));
            var retSize = 0;

            if (timeOut) {
                throw new TimeoutException($"Write operation has been time out due to the {nameof(evt.WaitOne)}");
            }

            try {
                DevManager_write(SafeFileHandle, (ulong)this.Position, buffer, count, ref retSize);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            this.Position += retSize;
            evt.Set();
            return;
        }
        //按照扇区整数倍写入;
        public override void Write(byte[] array, int offset, int count) {
            if (array == null) {
                throw new ArgumentNullException(nameof(array));
            }

            var handle = GCHandle.Alloc(array, GCHandleType.Pinned);

            try {
                var buffer = Marshal.UnsafeAddrOfPinnedArrayElement(array, offset);
                if (buffer == null) {
                    throw new InvalidDataException($"{nameof(buffer)} is null.");
                }

                WriteCore(buffer, count);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
            finally {
                handle.Free();
            }
        }

        //临时内存,用作存储读取/写入时间接跳转的存储;
        private byte[] _tempBuffer;
        private byte[] TempBuffer => _tempBuffer ?? (_tempBuffer = new byte[SectorSize - 1]);
        public int SectorSize { get; }

        
    }
}
