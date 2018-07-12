using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public static class ByteExtensions {
        /// <summary>
        /// 比较两个字节数组内容是否相等;
        /// </summary>
        /// <param name="szBuffer1">第一个字节数组</param>
        /// <param name="szBuffer2">第二个字节数组(长度不得小于第一个)</param>
        /// <param name="start2">第二个起始位置</param>
        /// <returns></returns>
        public static bool CompareTo(this byte[] szBuffer1, byte[] szBuffer2, int start2 = 0) {
            if (szBuffer1 == null || szBuffer2 == null) {
                return false;
            }
            if (szBuffer1.Length < 1 || szBuffer1.Length > szBuffer2.Length - start2) {                          //验证长度是否满足;
                return false;
            }

            bool res = true;                                                           //验证是否相等;
            for (int i = 0; i < szBuffer1.Length; i++) {
                if (szBuffer1[i] != szBuffer2[i + start2]) {
                    res = false;
                    break;
                }
            }

            return res;
        }
        
        /// <summary>
        /// 比较两个字节数组从某个起始位置开始的内容是否相等;
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <param name="len"></param>
        /// <param name="arr1Start"></param>
        /// <param name="arr2Start"></param>
        /// <returns></returns>
        public static bool IsHeadSame(this byte[] arr1, byte[] arr2, int len, int arr1Start = 0, int arr2Start = 0) {
            var min = Math.Min(arr1.Length - arr1Start, arr2.Length - arr2Start);
            min = Math.Min(min, len);

            if (min <= 0) {
                return false;
            }

            var index = 0;
            while (index < min) {
                if (arr1[index + arr1Start] != arr2[index + arr2Start]) {
                    return false;
                }
                index++;
            }
            return true;
        }

        /// <summary>
        /// Convert Byte to Char (can be used as visible text)
        /// </summary>
        /// <remarks>
        /// Code from : https://github.com/pleonex/tinke/blob/master/Be.Windows.Forms.HexBox/ByteCharConverters.cs
        /// </remarks>
        public static char ByteToChar(this byte val) => val > 0x1F && !(val > 0x7E && val < 0xA0) ? (char)val : '.';

        /// <summary>
        /// Convert byte to ASCII string
        /// </summary>
        public static string BytesToString(this byte[] buffer) {
            if (buffer == null) return string.Empty;

            var builder = new StringBuilder();

            foreach (var @byte in buffer)
                builder.Append(ByteToChar(@byte));

            return builder.ToString();
        }

        //Convert a byte to Hex char,i.e,10 = 'A'
        public static char ByteToHexChar(this int val) {
            if (val < 10) {
                return (char)(48 + val);
            }
            else if(val < 16) {
                return (char)(val + 55);
            }
            else {
                return 's';
            }
        }
        /// <summary>
        /// Convert a HexChar to byte,i.e,'A' = 10;
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static byte HexCharToByte(char val) {
            if(val < 48) {
                throw new ArgumentException($"{nameof(val)} '{val}' is not a valid hex char.");
            }
            else if(val < 58){
                return (byte)(val - 48);
            }
            else if(val > 64 && val < 71) {
                return (byte)(val - 55);
            }
            else {
                throw new ArgumentException($"{nameof(val)} '{val}' is not a valid hex char.");
            }
        }
        /// <summary>
        /// Converts a byte array to a hex string. For example: {10,11} = "0A0B"
        /// </summary>
        public static string BytesToHexString(this byte[] data) {
            if (data == null) return string.Empty;
            
            var chArr = new char[data.Length * 2];
            for (int i = 0; i < data.Length; i++) {
                chArr[2 * i] = ByteToHexChar(data[i] >> 4);
                chArr[2 * i + 1] = ByteToHexChar(data[i] -((data[i] >> 4) << 4));
            }

            return new string(chArr);
        }
        
        /// <summary>
        /// From HexString To byte[]
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(this string hexString) {
            if (hexString == null) return null;

            var chArr = hexString.ToCharArray();
            var validLength = chArr.Length / 2 * 2;
            var validTime = validLength / 2;
            var bts = new byte[validTime];
            
            for (int i = 0; i < validTime; i++) {
                bts[i] += (byte)(HexCharToByte(chArr[2 * i]) * 16);
                bts[i] += HexCharToByte(chArr[2 * i + 1]);
            }

#if DEBUG
            var str = BytesToHexString(bts);
            if(str != hexString) {

            }
#endif
            return bts;
        }
        
        /// <summary>
        /// Convert Char to Byte
        /// </summary>
        public static byte CharToByte(this char val) {
            if(val > (char)byte.MaxValue) {
                return 0;
            }
            return (byte)val;
        }

        /// <summary>
        /// Convert string to byte array
        /// </summary>
        public static byte[] StringToByte(this string str) => str.Select(CharToByte).ToArray();
    }
}
