using System;
using CDFCEntities.Enums;
using CDFCEntities.Structs;
using CDFCEntities.DeviceInfoes;
using CDFCEntities.Abstracts;
using System.IO;

namespace CDFCEntities.DeviceObjects {

    public class Partition : DefaultObjectDevice {
        public static Partition Create(PartitonStruct st) {
            Partition partition = new Partition();
            partition.LoGo = st.m_LoGo;
            partition.VolumeName = st.VolumeName;
            partition.FileSystem = st.FileSystem;
            partition.Name = st.m_Name;
            partition.Size = st.m_Size;
            partition.Type = (PartitionType)st.m_Type;
            partition.Offset = st.m_Offset;
            partition.Boot = st.m_Boot;
            partition.pDev = st.m_pDev;
            partition.Sign = Convert.ToChar(st.m_Sign);
            partition.pDBR = st.pDBR;
            partition.DriveType = Enums.DriveType.Disk;
            return partition;
        }
        public Device Device { get; set; }
        public int LoGo { get; set; }   //为那个物理设备的分区
        public string VolumeName { get; set; }  //卷标名称
        public string FileSystem { get; set; }     //文件系统
        public IntPtr Name { get; set; }              //分区名称
        public PartitionType Type { get; set; }                    //分区类型
        public ulong Offset { get; set; }                  //MBR的偏移
        public bool Boot { get; set; }                      //是否引导
        public IntPtr pDev { get; set; }                 //指向设备
        public char Sign { get; set; }                      //分区盘符
        public IntPtr pDBR { get; set; }
        public HddInfo HddInfo { get; set; }

        //剩余空间;
        private ulong freeSpace;
        public ulong FreeSpace {
            get {
                if(freeSpace == 0) {
                    try { 
                        DriveInfo di = new DriveInfo(Sign.ToString());
                        freeSpace = Convert.ToUInt64(di.AvailableFreeSpace);
                    }
                    catch(Exception ex) {
                        EventLogger.Logger.WriteLine("获得剩余空间失败!" + ex.Message);
                        return 0;
                    }
                }
                return freeSpace;
            }
        }
        
        //已用空间;
        public ulong ElapsedSpace {
            get {
                return Size - FreeSpace;
            }
        }
    }
}
