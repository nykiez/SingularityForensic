using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cflab.DataTransport.Modules.Backup.Android
{
    /// <summary>
    /// ADB 备份文件头
    /// </summary>
    public class AbHeader
    {
        #region 各种常量
        /// <summary>
        /// 备份清单文件版本
        /// </summary>
        public const int BACKUP_MANIFEST_VERSION = 1;
        /// <summary>
        /// 备份文件头魔术字
        /// </summary>
        public const string HEADER_MAGIC = "ANDROID BACKUP";
        /// <summary>
        /// 各种备份文件版本
        /// </summary>
        public const int BACKUP_FILE_V1 = 1;
        public const int BACKUP_FILE_V2 = 2;
        public const int BACKUP_FILE_V3 = 3;
        public const int BACKUP_FILE_V4 = 4;
        /// <summary>
        /// 加密方法、模式、填充方式
        /// </summary>
        public const string ENCRYPTION_MECHANISM = "AES/CBC/PKCS5Padding";
        /// <summary>
        /// PBKDF2 轮询次数
        /// </summary>
        public const int PBKDF2_HASH_ROUNDS = 10000;
        /// <summary>
        /// PBKDF2密钥大小
        /// </summary>
        public const int PBKDF2_KEY_SIZE = 256;//  bits
        /// <summary>
        /// 主密钥大小
        /// </summary>
        public const int MASTER_KEY_SIZE = 256;//  bits
        /// <summary>
        /// PBKDF2 盐大小
        /// </summary>
        public const int PBKDF2_SALT_SIZE = 512;//  bits
        public const string ENCRYPTION_ALGORITHM = "AES-256";
        #endregion

        #region 文件头属性

        /// <summary>
        /// 魔术字
        /// </summary>
        public string Magic { get; set; }

        /// <summary>
        /// 备份文件版本
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 是否经过压缩
        /// </summary>
        public bool Compressed { get; set; }

        /// <summary>
        /// 加密算法
        /// </summary>
        public string Algorithm { get; set; }

        /// <summary>
        /// 盐
        /// </summary>
        public byte[] UserSalt { get; set; }

        /// <summary>
        /// 盐校验和
        /// </summary>
        public byte[] SaltCheckSum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Rounds { get; set; }

        /// <summary>
        /// 用户向量
        /// </summary>
        public byte[] UserIv { get; set; }

        /// <summary>
        /// 主密钥
        /// </summary>
        public byte[] MasterKey { get; set; }

        /// <summary>
        /// 备份文件是否有效
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// 是否经过加密
        /// </summary>
        public bool IsEncrypted { get; set; }

        #endregion

        private AbHeader() { }
        

        /// <summary>
        /// 从流中读取文件头
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static AbHeader From(Stream stream)
        {
            var header = new AbHeader();
            if (stream == null)
            {
                return null;
            }
            // 魔术字 ==> 1
            header.Magic = ReadLine(stream);
            if (!string.Equals(header.Magic, HEADER_MAGIC))
            {
                return null;
            }
            // 备份文件版本 ==> 2
            header.Version = ReadInt(stream);
            if (header.Version == -1)
            {
                return null;
            }
            // 是否经过压缩 ==> 3
            header.Compressed = ReadInt(stream) == 1;
            // 加密算法 ==> 4
            header.Algorithm = ReadLine(stream);
            if (string.Equals(header.Algorithm, ENCRYPTION_ALGORITHM))
            {
                header.IsEncrypted = true;
            }
            // 如果未加密，文件头解析结束
            if (!header.IsEncrypted)
            {
                header.Valid = true;
                return header;
            }
            // 用户盐 ==> 5
            header.UserSalt = ReadBytes(stream);
            if (header.UserSalt.Length != PBKDF2_SALT_SIZE / 8)
            {
                header.Valid = false;
                return null;
            }
            // 用户盐校验和 ==> 6
            header.SaltCheckSum = ReadBytes(stream);
            // 循环数 ==> 7
            header.Rounds = ReadInt(stream);
            // 用户向量 ==> 8
            header.UserIv = ReadBytes(stream);
            // 主密钥 ==> 9
            header.MasterKey = ReadBytes(stream);
            // Header读取成功
            header.Valid = true;
            return header;
        }

        #region 读写文件流

        /// <summary>
        /// 从流中读取一行字符串
        /// </summary>
        /// <returns></returns>
        private static string ReadLine(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }
            var sb = new StringBuilder();
            var bytes = new byte[1];
            while (stream.Read(bytes, 0, 1) >= 0)
            {
                if (bytes[0] == 0x0A)
                {
                    break;
                }
                sb.Append((char)bytes[0]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 读取Int型数据
        /// </summary>
        /// <returns></returns>
        private static int ReadInt(Stream stream)
        {
            var result = -1;
            if (stream == null)
            {
                return result;
            }
            int.TryParse(ReadLine(stream), out result);
            return result;
        }

        /// <summary>
        /// 读取字节数组
        /// </summary>
        /// <returns></returns>
        private static byte[] ReadBytes(Stream stream)
        {
            var hex = ReadLine(stream);
            return string.IsNullOrWhiteSpace(hex) ? null : HexStringToBytes(hex);
        }

        #endregion

        #region 字节数组 <==> 十六进制字符串

        /// <summary>
        /// 字节数组转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Local
        public static string BytesToHexString(IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append($"{b:X02}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 字符串转换为字节数组
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
        #endregion
    }
}
