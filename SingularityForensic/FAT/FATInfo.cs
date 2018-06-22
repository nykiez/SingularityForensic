using System.Reflection;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.FAT {
    public class FATInfo: StructFieldDecriptorBase<StFatINFO>,ICustomMemberDescriptor {
        public FATInfo(StFatINFO stFatINFO,long offset):base(stFatINFO) {
            this.Offset = offset;
            
        }
        public long Offset { get; }
        internal string InternalDisplayName { get; set; }
        public override string DisplayName => InternalDisplayName;

        protected override void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = LanguageService.FindResourceString($"{Constants.FATFieldPrefix_Info}{fieldInfo.Name}");
        }
    }
}
