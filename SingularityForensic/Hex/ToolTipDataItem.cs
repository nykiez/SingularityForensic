using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex.ViewModels;

namespace SingularityForensic.Hex {
    class ToolTipDataItem : IToolTipDataItem {
        public string KeyName { get; set; }

        public string Value { get; set; }
    }
}
