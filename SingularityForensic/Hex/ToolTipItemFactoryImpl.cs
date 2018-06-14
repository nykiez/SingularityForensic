using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex.Models;
using System.ComponentModel.Composition;

namespace SingularityForensic.Hex {
    [Export(typeof(IToolTipItemFactory))]
    public class ToolTipItemFactoryImpl : IToolTipItemFactory {
        public IToolTipDataItem CreateToolTipDataItem() => new ToolTipDataItem();

        public IToolTipObjectItem CreateToolTipObjectItem() => new ToolTipObjectItem();
    }
}
