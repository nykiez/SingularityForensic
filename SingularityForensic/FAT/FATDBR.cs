using System.Reflection;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.FAT {
    public class FATDBR : StructFieldDecriptorBase<StFatDBR>,ICustomMemberDescriptor {
        public FATDBR(StFatDBR stFatDBR,long offset):base(stFatDBR) {
            this.Offset = offset;
        }

        public long Offset { get; }

        internal string InternalDisplayName { get; set; }
        public override string DisplayName => InternalDisplayName;

        protected override void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = LanguageService.FindResourceString($"{Constants.FATFieldPrefix_DBR}{fieldInfo.Name}");
        }
    }
}
