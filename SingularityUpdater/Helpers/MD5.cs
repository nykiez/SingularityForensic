using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SingularityUpdater.Helpers {
    public static class MD5 {
        private static MD5CryptoServiceProvider provider;
        public static MD5CryptoServiceProvider Provider =>
            provider ?? (provider = new MD5CryptoServiceProvider());

        public static string GetMD5(string myString) {
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);
            byte[] targetData = Provider.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++) {
                byte2String += targetData[i].ToString("X2");
            }
            var s = byte2String.Length;
            return byte2String.ToUpper();
        }

        public static string ComputeHashByStream(Stream stream) {
            var retVal = Provider.ComputeHash(stream);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++) {
                sb.Append(retVal[i].ToString("X2"));
            }
            var hash = sb.ToString().ToUpper();
            return hash;
        }
    }
}
