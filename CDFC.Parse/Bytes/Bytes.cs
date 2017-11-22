using System;

namespace CDFC.Parse.Bytes {
    public static class BytesExtensions {
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


        public static bool IsHeadSame(byte[] arr1, byte[] arr2, int len, int arr1Start = 0, int arr2Start = 0) {
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
    }

}
