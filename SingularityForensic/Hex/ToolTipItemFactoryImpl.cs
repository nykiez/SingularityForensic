using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex.Models;
using System.ComponentModel.Composition;

namespace SingularityForensic.Hex {
    [Export(typeof(IToolTipItemFactory))]
    public class ToolTipItemFactoryImpl : IToolTipItemFactory {
        public IToolTipDataItem CreateToolTipDataItem(string keyName = null, string value = null) => new ToolTipDataItem {
            KeyName = keyName,
            Value = value
        };

        public IToolTipObjectItem CreateToolTipObjectItem() => new ToolTipObjectItem();
    }
}
