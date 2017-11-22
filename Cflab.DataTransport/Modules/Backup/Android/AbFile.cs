using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cflab.DataTransport.Modules.Backup.Android.Tar;
using Cflab.DataTransport.Tools;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Zlib;

namespace Cflab.DataTransport.Modules.Backup.Android
{
    public struct PasswdResult
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 本次密码错误后，是否继续请求输入密码
        /// </summary>
        public bool Conntinue { get; set; }
    }

	public class AbFile
	{
        /// <summary>
        /// 文件头
        /// </summary>
	    private AbHeader header;

		/// <summary>
		/// 密码
		/// </summary>
		private string password;

		/// <summary>
		/// 备份文件流
		/// </summary>
		public Stream Stream { get; private set; }

		/// <summary>
		/// 获取密码回调
		/// </summary>
		public Func<bool, PasswdResult> PasswdHandler { get;private set; }

		#region 获取实例

		/// <summary>
		/// 外部无法实例化
		/// </summary>
		private AbFile(){}

		/// <summary>
		/// 读取备份文件，获取实例
		/// </summary>
		/// <param name="path"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static AbFile With(string path, Func<bool, PasswdResult> handler)
		{
			var file = new AbFile
			{
				PasswdHandler = handler
			};
		    try
		    {
		        // 读取备份文件流
		        file.Stream = File.OpenRead(path);
		    }
		    catch (Exception exception)
		    {
                // 记录日志
                Logger.Error(exception);
		        return null;
		    }
			file.header = AbHeader.From(file.Stream);
			return file;
		}

		#endregion

		#region 解析备份文件
		/// <summary>
		/// 解压文件
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="progress"></param>
		/// <returns></returns>
		public bool Extract(string dir, Action<long,long> progress)
		{
			if (!header.Valid)
			{
				return false;
			}
			var streame = Stream;
            // 指示当前大小
			var current = 0L;
			// 获取大小
			var size = GetLength();
			if (size <= 0)
			{
				return false;
			}
			// 处理流:解密，解压缩
			if (header.IsEncrypted)
			{
				streame = GetCipherStream(password);
			}
            // 加密数据流为空
			if (streame == null)
			{
				return false;
			}
			streame = header.Compressed ? new ZInputStream(streame) : streame;

			// 开始解压
			var	tar = TarArchive.CreateInputTarArchive(streame);
			tar.ProgressMessageEvent += delegate (TarArchive archive, TarEntry entry, string message)
			{
				current += entry.Size;
			    progress?.Invoke(current, size);
			};
			tar.ExtractContents(dir);

			// 关闭数据流
			streame.Close();
			return true;
		}

		/// <summary>
		/// 获取解析后的备份文件总长度
		/// </summary>
		/// <returns></returns>
		private long GetLength()
		{
			var streame = Stream;
			var temp = streame.Position;
			if (header.IsEncrypted)
			{
				var flag = true;
				var error = false;
				while (flag)
				{
					if (PasswdHandler == null)
					{
						break;
					}
					var res = PasswdHandler.Invoke(error);
					// 根据返回结果设置密码错误时是否继续询问
					flag = res.Conntinue;
					streame = GetCipherStream(res.Password);
					if (streame != null)
					{
						password = res.Password;
						break;
					}
					error = true;
				}
			}
			if (streame == null)
			{
				return 0;
			}
			streame = header.Compressed ? new ZInputStream(streame) : streame;
			var len = TarArchive.CreateInputTarArchive(streame).GetLength();
			Stream.Position = temp;
			return len;
		}

		#endregion

		#region 加解密相关

		/// <summary>
		/// 获取CipherStream
		/// </summary>
		/// <returns></returns>
		private CipherStream GetCipherStream(string password)
		{
			if (!header.IsEncrypted || !header.Valid)
			{
				return null;
			}
			try
			{
				// 计算用户密钥
				var userKey = GetUserKey(password, false);
				var ivSpec = header.UserIv;

				// 初始化密钥
				var cipher = CipherUtilities.GetCipher(AbHeader.ENCRYPTION_MECHANISM);
				var param = new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES256", userKey), ivSpec);
				cipher.Init(false, param);

				// 解密MasterKey
				var mkBlob = cipher.DoFinal(header.MasterKey);

				// master key IV
				var offset = 0;
				int len = mkBlob[offset++];
				var iv = Arrays.CopyOfRange(mkBlob, offset, offset + len);
				//  then the master key itself
				offset = offset + len;
				len = mkBlob[offset++];
				var mk = Arrays.CopyOfRange(mkBlob, offset, offset + len);
				//  and finally the master key checksum hash
				offset = offset + len;
				len = mkBlob[offset++];
				var mkChecksum = Arrays.CopyOfRange(mkBlob, offset, offset + len);

				var useUtf8 = header.Version >= AbHeader.BACKUP_FILE_V2;

				var calc = MakeKeyChecksum(mk, !useUtf8);

				if (!Arrays.AreEqual(mkChecksum, calc))
				{
					//Console.WriteLine("Checksum Error!");
				}

				param = new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES256", mk), iv);
				cipher.Init(false, param);
				return new CipherStream(Stream, cipher, null);
			}
			catch (Exception e)
			{
                Logger.Error(e);
				return null;
			}
		}

		/// <summary>
		/// 获取用户密钥
		/// </summary>
		/// <param name="key"></param>
		/// <param name="utf8"></param>
		/// <returns></returns>
		private byte[] GetUserKey(string key, bool utf8)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				return null;
			}
			var gen = new Pkcs5S2ParametersGenerator();
			var bytes = utf8
				? PbeParametersGenerator.Pkcs5PasswordToUtf8Bytes(key.ToCharArray())
				: PbeParametersGenerator.Pkcs5PasswordToBytes(key.ToCharArray());
			gen.Init(bytes, header.UserSalt, header.Rounds);
			var param = (KeyParameter) gen.GenerateDerivedParameters("AES256", AbHeader.PBKDF2_KEY_SIZE);
		    var userKey = param.GetKey();
		    return userKey;
		}

		/// <summary>
		/// 计算校验和
		/// </summary>
		/// <param name="mk"></param>
		/// <param name="utf8"></param>
		/// <returns></returns>
		private byte[] MakeKeyChecksum(IReadOnlyList<byte> mk, bool utf8)
		{
			// Widening Primitive Conversion : https://docs.oracle.com/javase/specs/jls/se8/html/jls-5.html#jls-5.1.2
			var mkAsSigned = new sbyte[mk.Count]; //sign extension
			for (var i = 0; i < mk.Count; i++)
			{
				mkAsSigned[i] = (sbyte) mk[i];
			}

			// Narrowing Primitive Conversion : https://docs.oracle.com/javase/specs/jls/se8/html/jls-5.html#jls-5.1.3
			var unSigned16Bits = new ushort[mkAsSigned.Length];
			for (var i = 0; i < mkAsSigned.Length; i++)
			{
				unSigned16Bits[i] = (ushort) (mkAsSigned[i] & 0xFFFF);
			}

			/***
			The Java programming language represents text in sequences of 16 - bit code UNITS, using the UTF-16 encoding.
			https://docs.oracle.com/javase/specs/jls/se8/html/jls-3.html#jls-3.1
			***/
			var byteArray = unSigned16Bits.SelectMany(BitConverter.GetBytes).ToArray();


			/***
			https://developer.android.com/reference/javax/crypto/spec/PBEKeySpec.html
			\"Different PBE mechanisms may consume different bits of each password character. 
			For example, the PBE mechanism defined in PKCS #5 looks at only the low order 8 bits of each character, 
			whereas PKCS #12 looks at all 16 bits of each character. \"  
			***/
			var curr = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, byteArray);

			// COnverting to char array
			var mkAsChar = new char[curr.Length];
			for (var i = 0; i < curr.Length; i++)
			{
				mkAsChar[i] = (char) curr[i];
			}
			return GetUserKey(new string(mkAsChar), utf8);
		}

		#endregion
	}
}