using System;
using System.Collections.Generic;
using Cflab.DataTransport.Modules.Transport.Model;
using Cflab.DataTransport.Tools;
using Cflab.DataTransport.Tools.Adb;
using Cflab.DataTransport.Tools.Adb.Devices;

namespace Cflab.DataTransport
{
    /// <summary>
    /// 设备管理器
    /// </summary>
    public static class DeviceManager
    {

        /// <summary>
        /// 用于监控设备连接状况的监控器
        /// </summary>
        private static DeviceTracker tracker;

        /// <summary>
        /// Adb服务是否初始化完成
        /// </summary>
        public static bool AdbServerReady { get; private set; }

        /// <summary>
        /// 初始化ADB服务服务
        /// </summary>
        /// <returns>初始化是否成功</returns>
        /// <param name="error">错误回调</param>
        public static bool Init(Action<ErrorResult> error)
        {
            AdbServerReady = AdbConnection.InitService(error);
            return AdbServerReady;
        }

        /// <summary>
        /// 获取设备列表
        /// </summary>
        public static List<Device> GetDevices(Action<ErrorResult> error)
        {
            var devices = new List<Device>();
            var track = new DeviceTracker();
            track.GetDevices(list =>
            {
                devices = list;
            }, error);
            return devices;
        }

        /// <summary>
        ///  监控Android设备的连接与断开
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="error"></param>
	    public static void TrackDevices(Action<List<Device>> handler, Action<ErrorResult> error)
	    {
		    tracker = new DeviceTracker();
	        tracker.BeginTrackDevices(handler, error);
	    }

        /// <summary>
        /// 停止监控设备的连接与断开
        /// </summary>
        public static void StopTrackDevices()
        {
            tracker.StopTrack(null);
        }
    }
}