using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Cflab.DataTransport.Tools;
using Cflab.DataTransport.Tools.Adb;
using Cflab.DataTransport.Tools.Adb.Devices;
using Cflab.DataTransport.Tools.Adb.Handler;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
	public class Device
    {
        #region 定义设备类型

        /*
         * ro.build.version.sdk	            SDK 版本
         * ro.build.version.release	        Android 系统版本
         * ro.build.version.security_patch	Android 安全补丁程序级别
         * ro.product.model	                型号
         * ro.product.brand	                品牌
         * ro.product.name	                设备名
         * ro.product.board	                处理器型号
         * ro.product.cpu.abilist	        CPU 支持的 abi 列表[节注一]
         * persist.sys.isUsbOtgEnabled	    是否支持 OTG
         * dalvik.vm.heapsize	            每个应用程序的内存上限
         * ro.sf.lcd_density	            屏幕密度
         */
        public enum DeviceState
        {
            Device,     // 正常链接
            Offline,    // 离线
            UnAuth,     // 未授权
            Unknown     // 其他
        }

        #endregion

        #region  设备相关属性
        /// <summary>
        /// 设备的名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string Serial { get; internal set; }

        /// <summary>
        /// 安卓版本号
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// 设备连接状态
        /// </summary>
        public DeviceState State { get; internal set; }

        /// <summary>
        /// SDK级别
        /// </summary>
        public string Sdk { get; internal set; }

        /// <summary>
        /// 设备是否已经root
        /// </summary>
        public bool IsRoot { get; internal set; }

        /// <summary>
        /// 手机厂商
        /// </summary>
        public string Brand { get; internal set; }

        /// <summary>
        /// 手机型号
        /// </summary>
        public string Model { get; internal set; }

        /// <summary>
        /// 用于显示的字符串
        /// </summary>
        public string Disply => State == DeviceState.Device
            ? Brand + " " + Model + " (Android " + Version + " " + Sdk + ")"
            : Serial;

        /// <summary>
        /// 检查ROOT时的回调
        /// </summary>
        public Func<bool> RootChecker { get; set; }

        #endregion

        #region 从字符串中解析设备，并获取设备状态
        /// <summary>
        /// 解析设备
        /// </summary>
        /// <param name="res"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static List<Device> Parse(string res, Action<ErrorResult> error)
        {
            var list = new List<Device>();
            if (string.IsNullOrEmpty(res))
            {
                return list;
            }
            var regex = new Regex(@"(?<no>.+)\t(?<state>\w+)\n");
            foreach (Match match in regex.Matches(res))
            {
                // 解析序列号
                var nmatch = match.Groups["no"];
                var smatch = match.Groups["state"];
                if (!nmatch.Success || !smatch.Success)
                {
                    continue;
                }
                // 解析状态
                var state = DeviceState.Unknown;
                switch (match.Groups["state"].Value)
                {
                    case "device":
                        state = DeviceState.Device;
                        break;
                    case "offline":
                        state = DeviceState.Offline;
                        break;
                    case "unauthorized":
                        state = DeviceState.UnAuth;
                        break;
                }
                var device = new Device
                {
                    Serial = nmatch.Value,
                    State = state
                };
                // 设备为在线状态，获取设备其他属性
                if (state == DeviceState.Device)
                {
                    var prop = new PropHandler(device.Serial, error);
                    device.Brand = prop.GetBrand().Trim();
                    device.Model = prop.GetModel().Trim();
                    device.Sdk = prop.GetSdkVersion().Trim();
                    device.Version = prop.GetRelease().Trim();
                    device.IsRoot = prop.IsRoot();
                }
                list.Add(device);
            }
            return list;
        }

        #endregion

        #region 尝试连接、尝试申请ROOT权限
        /// <summary>
        /// 尝试连接到手机：安装、运行相关软件
        /// </summary>
        /// <param name="error">错误处理</param>
        /// <returns></returns>
        public bool TryConnect(Action<ErrorResult> error)
        {
            const string package = "net.cflab.sockettransport";
            var pm = new PackageHandler(Serial, error);
            // 1.检测是否安装最新版本
            var flag = pm.CheckInstall(package, "1.3.2", error);
            if (!flag)
            {
                // 根据SDK版本判断是否自动授权
                int.TryParse(Sdk, out var sdk);
                var opt = sdk >= 23 ? "-r -g" : "-r";
                // 未安装或版本不对时，安装APK
                flag = pm.Install(AdbConnection.ApkPath, opt, error);
                if (flag)
                {
                    flag = pm.CheckInstall(package, AdbConnection.ApkVersion, error);
                }
                // 安装软件失败，返回false
                if (!flag)
                {
                    return false;
                }
            }
            // 2.尝试启动软件
            flag = pm.Launch(package, error);
            if (flag)
            {
                // 3.检测软件是否启动成功
                flag = pm.IsRunning(package, error);
            }
            // 4.尝试连接到Apk端的Socket
            if (!flag)
            {
                return false;
            }
            var test = new TestHandler(Serial);
            // 5.测试连接，循环检测5秒钟，共20次，等待软件启动延迟
            flag = test.TestConnect(10101, 20, error);
            return flag;
        }

        /// <summary>
        /// 申请并检查ROOT权限
        /// </summary>
        /// <returns></returns>
        public bool TryRequestRoot(int times,Action<ErrorResult> error)
        {
            // 手机没有Root或连接不通时直接返回
            if (!IsRoot || !TryConnect(error))
            {
                return false;
            }
            var handler = new TestHandler(Serial);
            return handler.TestRoot(10101, RootChecker, times, error);
        }
        #endregion

        #region 获取信息

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <typeparam name="TInfo"></typeparam>
        /// <param name="handler"></param>
        /// <param name="error"></param>
        public bool GetInfo<TInfo>(Action<List<TInfo>, int, int> handler, Action<ErrorResult> error) where TInfo : IInfo
        {
            // 测试连接状态
            if (!TryConnect(error))
            {
                return false;  
            }
            // 获取信息环境准备
            var are = new AutoResetEvent(false);
            var flag = false;
            var cmd = Command.Create(typeof(TInfo).Name);
            var info = new InfoHandler<TInfo>(Serial)
            {
                DataHandler = handler,
                Exit = success =>
                {
                    flag = success;
                    are.Set();
                }
            };
            // 开始获取数据
            info.BeginGetInfo(cmd.ToString(), error);
            are.WaitOne();
            return flag;
        }

        #endregion

        #region 获取文件、文件列表
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="progress"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<AnFile> GetFileList(string dir, Action<int, int> progress, Action<ErrorResult> error)
        {
            // 获取AnFile列表
            var are = new AutoResetEvent(false);
            var files = new List<AnFile>();
            // 测试连接状态
            if (!TryConnect(error))
            {
                return null;
            }
            // 获取信息
            var cmd = Command.Create(typeof(AnFile).Name);
            var info = new InfoHandler<AnFile>(Serial)
            {
                DataHandler = (infos, current, total) =>
                {
                    files.AddRange(infos);
                    progress?.Invoke(current, total);
                },
                // 处理退出事件
                Exit = success => { are.Set(); }
            };
            info.BeginGetInfo(cmd.ToString(), error);
            are.WaitOne();
            if (files.Count == 0)
            {
                return null;
            }
            // 解析Anfile
            var root = new List<AnFile>();
            foreach (var file in files.FindAll(file => file.IsRoot))
            {
                root.Add(file);
                AnFile.Prepare(file, files);
            }
            return root;
        }

        /// <summary>
        /// 从手机端下载文件
        /// </summary>
        /// <param name="path">远程文件路径</param>
        /// <param name="dest">本地保存地址</param>
        /// <param name="progress">进度回调</param>
        /// <param name="error"></param>
        public bool GetFile(string path, string dest,  Action<long,long> progress, Action<ErrorResult> error)
        {
            // 检查连接情况
            if (!TryConnect(error))
            {
                return false;
            }
            var flag = false;
            try
            {
                // 处理目录和文件名
                if (path.StartsWith("/"))
                {
                    path = path.Substring(1);
                }
                var full = Path.Combine(dest, path);
                dest = Path.GetDirectoryName(full);
                if (string.IsNullOrEmpty(dest))
                {
                    return false;
                }
                if (!Directory.Exists(dest))
                {
                    Directory.CreateDirectory(dest);
                }
                var cmd = Command.Create("File", path);
                var are = new AutoResetEvent(false);
                var file = new FileHandler(Serial)
                {
                    DataHandler = progress,
                    Exit = success =>
                    {
                        flag = success;
                        are.Set();
                    }
                };
                file.BeginGetFile(cmd.ToString(), full, error);
                are.WaitOne();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                ErrorResult.InvokeError(error,CommonError.InvalidDirectory,dest);
            }
            return flag;
        }


        #endregion

        #region 生成ADB备份
        /// <summary>
        /// 生成ADB备份
        /// </summary>
        /// <param name="path"></param>
        /// <param name="confirm"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool Backup(string path, Func<bool> confirm, Action<ErrorResult> error)
        {
            // 检测连接是否通常
            if (!TryConnect(error))
            {
                return false;
            }
            var flag = false;
            // 转化为同步方式
            var are = new AutoResetEvent(false);
            var backup = new BackupHandler(Serial)
            {
                Confirme = confirm,
                Exit = success =>
                {
                    flag = success;
                    are.Set();
                }
            };
            backup.BeginBackup(path, error);
            // 等待异步任务完成
            are.WaitOne();
            return flag;
        }

        #endregion
    }
}
