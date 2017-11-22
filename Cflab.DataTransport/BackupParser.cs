using System;
using System.IO;
using Cflab.DataTransport.Modules.Backup.Android;
using Cflab.DataTransport.Modules.Backup.HuwWei;
using Cflab.DataTransport.Tools;

namespace Cflab.DataTransport
{
    /// <summary>
    /// 备份文件解析
    /// </summary>
    public class BackupParser
    {
        /// <summary>
        /// 备份文件路径
        /// </summary>
        private readonly string path;

        /// <summary>
        /// 解析结果存储位置
        /// </summary>
        private readonly string des;

        /// <summary>
        /// 错误回调
        /// </summary>
        private readonly Action<ErrorResult> error;

        private BackupParser(string path, string des, Action<ErrorResult> error)
        {
            this.path = path;
            this.des = des;
            this.error = error;
        }

        /// <summary>
        /// 创建备份解析器
        /// </summary>
        /// <param name="path"></param>
        /// <param name="des"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static BackupParser Create(string path, string des, Action<ErrorResult> error)
        {
            try
            {
                // 创建保存目录
                Directory.CreateDirectory(des);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                ErrorResult.InvokeError(error, CommonError.InvalidDirectory, des);
                return null;
            }
            return new BackupParser(path, des, error);
        }

        /// <summary>
        /// 解析ADB备份
        /// </summary>
        /// <param name="passwd"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public bool ParseAdbBackup(Func<bool, PasswdResult> passwd, Action<long,long> progress)
        {
            // 创建备份文件
            var ab = AbFile.With(path, passwd);
            // 解析提取备份文件
            return ab.Extract(des, progress);
        }

        /// <summary>
        /// 解析华为备份
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public bool ParseHuaWeiBackup(Action<int,string> progress)
        {
            var flag = false;
            if (!Directory.Exists(path))
            {
                ErrorResult.InvokeError(error, CommonError.InvalidDirectory, path);
                return false;
            }
            // 解析文件夹
            foreach (var file in Directory.GetFiles(path))
            {
                var hb = HbFile.Create(file, error);
                //flag = hb.Extract(des, pro =>
                //{
                //    progress?.Invoke(pro, file);
                //});
            }
            return flag;
        }
    }
}
