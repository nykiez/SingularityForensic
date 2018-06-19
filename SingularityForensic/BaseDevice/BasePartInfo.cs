using SingularityForensic.Contracts.Common;

namespace SingularityForensic.BaseDevice {
    /// <summary>
    /// //GPT/Dos分区信息基类;
    /// </summary>
    internal abstract class BasePartInfo {
        public InfoDisk InfoDisk { get; set; }
    }

    public class InfoDisk: StructFieldDecriptorBase<StInFoDisk>, ICustomMemerDecriptor {
        public InfoDisk(StInFoDisk stInfoDisk):base(stInfoDisk) {
            
        }
    }

    /// <summary>
    /// EFI信息;
    /// </summary>
    public class EFIInfo : StructFieldDecriptorBase<StEFIInfo> {
        public EFIInfo(StEFIInfo stEFIInfo):base(stEFIInfo) {

        }
    }

    /// <summary>
    /// GPT分区项信息;
    /// </summary>
    public class EFIPTable : StructFieldDecriptorBase<StEFIPTable> {
        public EFIPTable(StEFIPTable stEFIPtable):base(stEFIPtable) {

        }
    }

    public class DosPTable:StructFieldDecriptorBase<StDosPTable>,ICustomMemerDecriptor {
        public DosPTable(StDosPTable stDosPTable):base(stDosPTable) {

        }
    }

    /// <summary>
    /// //Dos分区项信息;
    /// </summary>
    internal class DOSPartInfo : BasePartInfo {
        public DosPTable DosPTable { get; set; }
    }

    /// <summary>
    /// //GPT分区项信息;
    /// </summary>
    internal class GPTPartInfo : BasePartInfo {
        public StGptPTable StGptPTable { get; set; }
        public EFIInfo EFIInfo { get; set; }
        public EFIPTable EFIPTable { get; set; }
    }
}
