using CDFC.Util.PInvoke;
using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 硬盘相关描述信息项;
/// </summary>
namespace SingularityForensic.Drive {
    public class HddInfo {
        public HddInfo(HDDInfoStruct st) {
            ID = st.ID;
            VendorID = st.VendorID;
            ProductID = st.ProductID;
            ProductRevision = st.ProductRevision;
            SerialNumber = st.SerialNumber;

            if(st.info == IntPtr.Zero) {
                return;
            }

            try {
                var hddInfo2Struct = st.info.GetStructure<HDDInfo2Struct>();
                var hddInfo2 = new HddInfo2 (hddInfo2Struct);
                HddInfo2 = hddInfo2;
            }
            catch(Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
            }
        }

        public int ID { get;  }
        public string VendorID { get;  }
        public string ProductID { get;  }
        public string ProductRevision { get;  }
        public string SerialNumber { get; }
        public char Lable { get; }
        public HddInfo2 HddInfo2 { get;  }
    }

    public class HddInfo2 {
        public HddInfo2(HDDInfo2Struct st) {
            ID = st.ID;
            ModelNumber = st.szModelNumber;
            SerialNumber = st.szSerialNumber;
            ControllerNumber = st.szControllerNumber;
        }
        public int ID { get; }
        public string ModelNumber { get; }
        public string SerialNumber { get; }
        public string ControllerNumber { get; }
    }

    public class CHS {
        public static CHS Create(CHSStruct st) {
            CHS chs = new CHS();
            chs.Cylinder = st.m_Cylinder;
            chs.HeadTrack = st.m_Head_Track;
            chs.TrackSector = st.m_Track_Sector;
            return chs;
        }
        public ulong Cylinder { get; set; }                   //柱面数
        public ulong HeadTrack { get; set; }              //每柱面磁道数
        public uint TrackSector { get; set; }
    }
}
