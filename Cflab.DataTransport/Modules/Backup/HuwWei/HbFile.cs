using System;
using System.IO;
using System.Text.RegularExpressions;
using Cflab.DataTransport.Tools;
using Chloe.SQLite;

namespace Cflab.DataTransport.Modules.Backup.HuwWei
{
    public class HbFile
    {

        /// <summary>
        /// 错误处理
        /// </summary>
        private readonly Action<ErrorResult> error;

        /// <summary>
        /// SQLite链接上下文
        /// </summary>
        private readonly SQLiteContext context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="error"></param>
        private HbFile(SQLiteContext context, Action<ErrorResult> error)
        {
            this.context = context;
            this.error = error;
        }

        /// <summary>
        /// 创建华为备份文件实例
        /// </summary>
        /// <param name="path"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static HbFile Create(string path, Action<ErrorResult> error)
        {
            try
            {
                var context = SQLiteFactory.NewContext(path);
                var file = new HbFile(context, error);
                return file;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        /// <summary>
        /// 解析提取备份文件
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public bool Extract(string dest, Action<long,long> progress)
        {
            if (context == null)
            {
                return false;
            }
            // 检查是否为备份文件
            if (!Check())
            {
                return false;
            }
            // 获取文件总长度
            var total = context.Query<FileData>().Sum(data => data.Length);
            var current = 0L;
            // 查询非文件夹文件
            var infos = context.Query<FileInfo>().Where(info => info.Index >= 0);
            // 遍历解析文件
            foreach (var info in infos.OrderBy(info => info.Index).ToList())
            {
                // 确保路径可用
                var path = dest + info.Path.Replace("/", "\\");

                var regex = new Regex($@"[{string.Concat(Path.GetInvalidFileNameChars())}]");
                path = regex.Replace(path, "_");

                // 提取文件
                try
                {
                    // 创建文件夹
                    var dir = Path.GetDirectoryName(path);
                    if (dir != null)
                    {
                        Directory.CreateDirectory(dir);
                    }
                    // 创建文件
                    var stream = File.OpenWrite(path);
                    var blobs = context.Query<FileData>().Where(data => data.FileIndex == info.Index);
                    // 循环写入文件
                    foreach (var blob in blobs.OrderBy(data => data.DataIndex).ToList())
                    {
                        current += blob.Length;
                        stream.Write(blob.Data, 0, Convert.ToInt32(blob.Length));
                        // 进度回调
                        progress?.Invoke(current , total);
                    }
                    stream.Close();
                }
                catch (Exception e)
                {
                    ErrorResult.InvokeError(error, CommonError.InvalidDirectory, path);
                    Logger.Error(e);
                }
            }
            return true;
        }

        /// <summary>
        /// 检查是否是可用的备份
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            var flag = 0;
            try
            {
                var query = context.Query<SQLiteMaster>();
                query.ToList().ForEach(master =>
                {
                    switch (master.Name)
                    {
                        case "apk_info":
                        case "apk_file_info":
                        case "apk_file_data":
                            flag += 1;
                            break;
                    }
                });
            }
            catch (Exception e)
            {
                Logger.Error(e.InnerException ?? e);
            }
            return flag >= 3;
        }
    }
}
