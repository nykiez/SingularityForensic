using CDFC.Parse.Contracts;
using System;
using System.IO;
using System.Linq;

namespace CDFC.Parse.Abstracts {
    //设备契约;
    public abstract class Device : BlockDeviceFile {
        public Device() : base(null) { }

        public abstract int SecSize { get; protected set; }             //扇区大小;
        
        public abstract Stream Stream { get; }                          //设备文件流;

        public abstract void Exit();                                    //设备退出接口;

        public abstract PartsType PartsType { get; }                    //分区表类型;
    }

    public static class DeviceHelper {
        /// <summary>
        /// 通过Url找到子文件;
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IFile GetFileByUrl(this Device device,string url) {
            try {
                url = url.Replace('\\', '/');
                if(url == "/") {
                    return device;
                }

                var args = url.Split('/');
                var partArgs = args.ToList().GetRange(1,args.Length - 1);

                var partIndex = int.Parse(args[0]);
                return (device.Children.ElementAt(partIndex) as Partition).GetFileByUrl(partArgs.ToArray()); 
            }
            catch(Exception ex) {
                EventLogger.Logger.WriteCallerLine(ex.Message);
            }
            return null;
        }
    }
}
