using System.Reflection;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.BaseDevice {
    /// <summary>
    /// //GPT/Dos分区信息基类;
    /// </summary>
    internal abstract class BasePartInfo {
        public InfoDisk InfoDisk { get; set; }
    }

    public class InfoDisk: StructFieldDecriptorBase<StInFoDisk>, ICustomMemberDescriptor {
        public InfoDisk(StInFoDisk stInfoDisk):base(stInfoDisk) {
            
        }

        internal string InternalDisplayName { get; set; }
        public override string DisplayName => InternalDisplayName;

        protected override void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = LanguageService.FindResourceString($"{Constants.BaseDeviceFieldPrefix_InfoDisk}{fieldInfo.Name}");
        }

    }

    /// <summary>
    /// EFI信息;
    /// </summary>
    public class EFIInfo : StructFieldDecriptorBase<StEFIInfo> {
        public EFIInfo(StEFIInfo stEFIInfo):base(stEFIInfo) {

        }
        protected override void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = LanguageService.FindResourceString($"{Constants.BaseDeviceFieldPrefix_InfoDisk}{fieldInfo.Name}");
        }
        internal string InternalDisplayName { get; set; }
        public override string DisplayName => InternalDisplayName;
    }

    /// <summary>
    /// GPT分区项信息;
    /// </summary>
    public class EFIPTable : StructFieldDecriptorBase<StEFIPTable> {
        public EFIPTable(StEFIPTable stEFIPtable):base(stEFIPtable) {

        }
        protected override void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = LanguageService.FindResourceString($"{Constants.GptFieldPrefix_EFIPTable}{fieldInfo.Name}");
        }

        internal string InternalDisplayName { get; set; }
        public override string DisplayName => InternalDisplayName;
    }

    public class DosPTable {
        public DosPTable(StDosPTable stDosPTable){
            this.StDosPTable = stDosPTable;
        }
        public StDosPTable StDosPTable { get; }
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
