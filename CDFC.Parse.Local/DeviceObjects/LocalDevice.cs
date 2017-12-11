using System;
using System.Collections.Generic;
using CDFC.Parse.Abstracts;
using CDFC.Parse.Local.Structs;
using CDFC.Parse.Contracts;

namespace CDFC.Parse.Local.DeviceObjects {
    //本地硬盘设备;
    public class LocalDevice : Device,IHaveHandle { 
        private LocalDevice() { }
        public LocalDevice(StPhysicsDevice st) {
            this.PhysicsDeviceStruct = st;
            Partitions = new List<Partition>();
            
        }

        public StPhysicsDevice PhysicsDeviceStruct { get; }
        //public override long Size => 
        //设备标数(如果为16不以物理名称打开)
        public int DeviceID => PhysicsDeviceStruct.ObjectID;
        public string Lable { get; set; }
        //驱动名称
        public string DevName { get; set; }                  
        
        public CHS DevCHS { get; set; }                   //设备几何
        public ulong DevMomd { get; set; }                      //访问模式
        public uint DevType { get; set; }                   //设备类型
        
        public IntPtr Buffer { get; set; }                     //读写缓存
        public IntPtr Partition { get; set; }                    //分区结构（废弃）
        public IntPtr Arch { get; set; }                   //调用指针
        public IntPtr DevRW { get; set; }                      //设备读写
        public bool DevState { get; set; }

        /// <summary>
        /// 硬盘标识号;
        /// </summary>
        public string SerialNumber {
            get {
                if(HddInfo?.HddInfo2 != null) {
                    return HddInfo.HddInfo2.szModelNumber;
                }
                return HddInfo?.VendorID??string.Empty;
            }
        }
        public HddInfo HddInfo { get; set; }
        public List<Partition> Partitions { get; set; }
        
    }
}
