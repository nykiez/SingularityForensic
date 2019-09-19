using CDFC.Util.PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public static class EndianUtilities {
        #region Bit Twiddling

        public static void WriteBytesLittleEndian(this byte[] buffer, ushort val, int offset) {
            buffer[offset] = (byte)(val & 0xFF);
            buffer[offset + 1] = (byte)((val >> 8) & 0xFF);
        }

        public static void WriteBytesLittleEndian(this byte[] buffer, uint val, int offset) {
            buffer[offset] = (byte)(val & 0xFF);
            buffer[offset + 1] = (byte)((val >> 8) & 0xFF);
            buffer[offset + 2] = (byte)((val >> 16) & 0xFF);
            buffer[offset + 3] = (byte)((val >> 24) & 0xFF);
        }

        public static void WriteBytesLittleEndian(this byte[] buffer, ulong val, int offset) {
            buffer[offset] = (byte)(val & 0xFF);
            buffer[offset + 1] = (byte)((val >> 8) & 0xFF);
            buffer[offset + 2] = (byte)((val >> 16) & 0xFF);
            buffer[offset + 3] = (byte)((val >> 24) & 0xFF);
            buffer[offset + 4] = (byte)((val >> 32) & 0xFF);
            buffer[offset + 5] = (byte)((val >> 40) & 0xFF);
            buffer[offset + 6] = (byte)((val >> 48) & 0xFF);
            buffer[offset + 7] = (byte)((val >> 56) & 0xFF);
        }

        public static void WriteBytesLittleEndian(this byte[] buffer, short val, int offset) {
            WriteBytesLittleEndian(buffer,(ushort)val, offset);
        }

        public static void WriteBytesLittleEndian(this byte[] buffer, int val, int offset) {
            WriteBytesLittleEndian(buffer, (uint)val,offset);
        }

        public static void WriteBytesLittleEndian(this byte[] buffer, long val, int offset) {
            WriteBytesLittleEndian(buffer, (ulong)val, offset);
        }

        public static void WriteBytesLittleEndian(this byte[] buffer, Guid val, int offset) {
            byte[] le = val.ToByteArray();
            Array.Copy(le, 0, buffer, offset, 16);
        }

        public static void WriteBytesBigEndian(this byte[] buffer, ushort val, int offset) {
            buffer[offset] = (byte)(val >> 8);
            buffer[offset + 1] = (byte)(val & 0xFF);
        }

        public static void WriteBytesBigEndian(this byte[] buffer, uint val, int offset) {
            buffer[offset] = (byte)((val >> 24) & 0xFF);
            buffer[offset + 1] = (byte)((val >> 16) & 0xFF);
            buffer[offset + 2] = (byte)((val >> 8) & 0xFF);
            buffer[offset + 3] = (byte)(val & 0xFF);
        }

        public static void WriteBytesBigEndian(this byte[] buffer, ulong val, int offset) {
            buffer[offset] = (byte)((val >> 56) & 0xFF);
            buffer[offset + 1] = (byte)((val >> 48) & 0xFF);
            buffer[offset + 2] = (byte)((val >> 40) & 0xFF);
            buffer[offset + 3] = (byte)((val >> 32) & 0xFF);
            buffer[offset + 4] = (byte)((val >> 24) & 0xFF);
            buffer[offset + 5] = (byte)((val >> 16) & 0xFF);
            buffer[offset + 6] = (byte)((val >> 8) & 0xFF);
            buffer[offset + 7] = (byte)(val & 0xFF);
        }

        public static void WriteBytesBigEndian(this byte[] buffer, short val, int offset) {
            WriteBytesBigEndian(buffer,(ushort)val, offset);
        }

        public static void WriteBytesBigEndian(this byte[] buffer, int val, int offset) {
            WriteBytesBigEndian(buffer, (uint)val,  offset);
        }

        public static void WriteBytesBigEndian(this byte[] buffer, long val, int offset) {
            WriteBytesBigEndian(buffer, (ulong)val, offset);
        }

        public static void WriteBytesBigEndian(this byte[] buffer, Guid val, int offset) {
            byte[] le = val.ToByteArray();
            WriteBytesBigEndian(buffer,ToUInt32LittleEndian(le, 0),  offset + 0);
            WriteBytesBigEndian(buffer,ToUInt16LittleEndian(le, 4),  offset + 4);
            WriteBytesBigEndian(buffer, ToUInt16LittleEndian(le, 6),  offset + 6);
            Array.Copy(le, 8, buffer, offset + 8, 8);
        }

        public static ushort ToUInt16LittleEndian(this byte[] buffer, int offset) {
            return (ushort)(((buffer[offset + 1] << 8) & 0xFF00) | ((buffer[offset + 0] << 0) & 0x00FF));
        }

        public static uint ToUInt32LittleEndian(this byte[] buffer, int offset) {
            return (uint)(((buffer[offset + 3] << 24) & 0xFF000000U) | ((buffer[offset + 2] << 16) & 0x00FF0000U)
                          | ((buffer[offset + 1] << 8) & 0x0000FF00U) | ((buffer[offset + 0] << 0) & 0x000000FFU));
        }

        public static ulong ToUInt64LittleEndian(this byte[] buffer, int offset) {
            return ((ulong)ToUInt32LittleEndian(buffer, offset + 4) << 32) | ToUInt32LittleEndian(buffer, offset + 0);
        }

        public static short ToInt16LittleEndian(this byte[] buffer, int offset) {
            return (short)ToUInt16LittleEndian(buffer, offset);
        }

        public static int ToInt32LittleEndian(this byte[] buffer, int offset) {
            return (int)ToUInt32LittleEndian(buffer, offset);
        }

        public static long ToInt64LittleEndian(this byte[] buffer, int offset) {
            return (long)ToUInt64LittleEndian(buffer, offset);
        }

        public static ushort ToUInt16BigEndian(this byte[] buffer, int offset) {
            return (ushort)(((buffer[offset] << 8) & 0xFF00) | ((buffer[offset + 1] << 0) & 0x00FF));
        }

        public static uint ToUInt32BigEndian(this byte[] buffer, int offset) {
            uint val = (uint)(((buffer[offset + 0] << 24) & 0xFF000000U) | ((buffer[offset + 1] << 16) & 0x00FF0000U)
                              | ((buffer[offset + 2] << 8) & 0x0000FF00U) | ((buffer[offset + 3] << 0) & 0x000000FFU));
            return val;
        }

        public static ulong ToUInt64BigEndian(this byte[] buffer, int offset) {
            return ((ulong)ToUInt32BigEndian(buffer, offset + 0) << 32) | ToUInt32BigEndian(buffer, offset + 4);
        }

        public static short ToInt16BigEndian(this byte[] buffer, int offset) {
            return (short)ToUInt16BigEndian(buffer, offset);
        }

        public static int ToInt32BigEndian(this byte[] buffer, int offset) {
            return (int)ToUInt32BigEndian(buffer, offset);
        }

        public static long ToInt64BigEndian(this byte[] buffer, int offset) {
            return (long)ToUInt64BigEndian(buffer, offset);
        }

        public static Guid ToGuidLittleEndian(this byte[] buffer, int offset) {
            byte[] temp = new byte[16];
            Array.Copy(buffer, offset, temp, 0, 16);
            return new Guid(temp);
        }

        public static Guid ToGuidBigEndian(this byte[] buffer, int offset) {
            return new Guid(
                ToUInt32BigEndian(buffer, offset + 0),
                ToUInt16BigEndian(buffer, offset + 4),
                ToUInt16BigEndian(buffer, offset + 6),
                buffer[offset + 8],
                buffer[offset + 9],
                buffer[offset + 10],
                buffer[offset + 11],
                buffer[offset + 12],
                buffer[offset + 13],
                buffer[offset + 14],
                buffer[offset + 15]);
        }

        public static byte[] ToByteArray(this byte[] buffer, int offset, int length) {
            byte[] result = new byte[length];
            Array.Copy(buffer, offset, result, 0, length);
            return result;
        }

        public static T ToStruct<T>(this byte[] buffer, int offset)
            where T : IByteArraySerializable, new() {
            T result = new T();
            result.ReadFrom(buffer, offset);
            return result;
        }

        /// <summary>
        /// 通过<see cref="System.Runtime.InteropServices.Marshal"/>从缓冲区中获取结构体;
        /// </summary>
        /// <typeparam name="TStruct"></typeparam>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static TStruct? ToStructWithMarshal<TStruct>(this byte[] buffer,int offset = 0) where TStruct:struct {
            if(buffer == null) {
                throw new ArgumentNullException(nameof(buffer));
            }

            var sz = Marshal.SizeOf(typeof(TStruct));
            var stPtr = Marshal.AllocHGlobal(sz);
            try {
                Marshal.Copy(buffer, offset, stPtr, sz);
                return stPtr.GetStructure<TStruct>();
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                return null;
            }
            finally {
                Marshal.FreeHGlobal(stPtr);
            }
        }
        

        /// <summary>
        /// Primitive conversion from Unicode to ASCII that preserves special characters.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="dest">The buffer to fill.</param>
        /// <param name="offset">The start of the string in the buffer.</param>
        /// <param name="count">The number of characters to convert.</param>
        /// <remarks>The built-in ASCIIEncoding converts characters of codepoint > 127 to ?,
        /// this preserves those code points by removing the top 16 bits of each character.</remarks>
        public static void StringToBytes(string value, byte[] dest, int offset, int count) {
            char[] chars = value.ToCharArray(0, Math.Min(value.Length, count));

            int i = 0;
            while (i < chars.Length && i < count) {
                dest[i + offset] = (byte)chars[i];
                ++i;
            }

            while (i < count) {
                dest[i + offset] = 0;
                ++i;
            }
        }

        /// <summary>
        /// Primitive conversion from ASCII to Unicode that preserves special characters.
        /// </summary>
        /// <param name="data">The data to convert.</param>
        /// <param name="offset">The first byte to convert.</param>
        /// <param name="count">The number of bytes to convert.</param>
        /// <returns>The string.</returns>
        /// <remarks>The built-in ASCIIEncoding converts characters of codepoint > 127 to ?,
        /// this preserves those code points.</remarks>
        public static string BytesToString(byte[] data, int offset, int count) {
            char[] result = new char[count];

            for (int i = 0; i < count; ++i) {
                result[i] = (char)data[i + offset];
            }

            return new string(result);
        }

        /// <summary>
        /// Primitive conversion from ASCII to Unicode that stops at a null-terminator.
        /// </summary>
        /// <param name="data">The data to convert.</param>
        /// <param name="offset">The first byte to convert.</param>
        /// <param name="count">The number of bytes to convert.</param>
        /// <returns>The string.</returns>
        /// <remarks>The built-in ASCIIEncoding converts characters of codepoint > 127 to ?,
        /// this preserves those code points.</remarks>
        public static string BytesToZString(byte[] data, int offset, int count) {
            char[] result = new char[count];

            for (int i = 0; i < count; ++i) {
                byte ch = data[i + offset];
                if (ch == 0) {
                    return new string(result, 0, i);
                }

                result[i] = (char)ch;
            }

            return new string(result);
        }

        #endregion
    }
}
