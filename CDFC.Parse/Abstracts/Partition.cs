using CDFC.Parse.Contracts;
using CDFC.Util.IO;
using System;
using System.IO;
using System.Linq;

namespace CDFC.Parse.Abstracts {
    //分区契约;
    public abstract class Partition:BlockDeviceFile{
        /// <summary>
        /// 分区构造方法;
        /// </summary>
        /// <param name="parent">父类型为设备</param>
        public Partition(Device parent):base(parent) {
        }
        
        public virtual long StartLBA { get; protected set; }      //起始LBA(相对设备);

        public override long Size => 0;

        public long EndLBA => StartLBA + Size;

        
        public abstract FileSystemType FSType { get; }              //文件系统类型;

        public virtual Stream GetStream(bool isReadOnly = true) {
            var device = this.GetParent<Device>();
            if (device != null) {
                return InterceptStream.CreateFromStream(device.Stream, StartLBA, Size);
            }
            else {
                EventLogger.Logger.WriteCallerLine($"{nameof(device)} can't be null!");
                return null;
            }
            
        }
        
        public abstract uint ClusterSize { get; }
    }

    /// <summary>
    /// 分区助手;
    /// </summary>
    public static class PartitionHelper {
        /// <summary>
        /// 通过Url找到子文件;
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IFile GetFileByUrl(this Partition part, string url) {
            try {
                url = url.Replace('\\', '/');
                var urlArgs = url.Split('/');
                return GetFileByUrl(part, urlArgs);
            }
            catch(Exception ex) {
                EventLogger.Logger.WriteCallerLine(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// 通过Url参数获得子文件;
        /// </summary>
        /// <param name="part"></param>
        /// <param name="urlArgs"></param>
        /// <returns></returns>
        public static IFile GetFileByUrl(this Partition part,string[] urlArgs) {
            if(urlArgs == null) {
                return null;
            }
            if(urlArgs.Length == 1 && string.IsNullOrEmpty(urlArgs[0])) {
                return part;
            }

            IIterableFile fileNode = part;
            for (int index = 0; index < urlArgs.Length - 1; index++) {
                if (fileNode == null) {
                    throw new FileNotFoundException($"Can't find file {urlArgs[index]}-{urlArgs.Aggregate((a,b) => $"{a}/{b}")}");
                }
                fileNode = fileNode.Children.FirstOrDefault(p => p.Name == urlArgs[index]) as IIterableFile;
            }
            return fileNode.Children.FirstOrDefault(p => p.Name == urlArgs[urlArgs.Length - 1]);
        }
    }
}
