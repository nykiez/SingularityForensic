
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Contracts.Hex {
    public interface IToolTipItemFactory {
        IToolTipDataItem CreateToolTipDataItem(string keyName = null,string value = null);
        IToolTipObjectItem CreateToolTipObjectItem();
    }
    public class ToolTipItemFactory:GenericServiceStaticInstance<IToolTipItemFactory> {
        public static IToolTipDataItem CreateIToolTipDataItem(string keyName = null,string value = null) => Current?.CreateToolTipDataItem(keyName,value);
        public static IToolTipObjectItem CreateToolTipObjectItem() => Current?.CreateToolTipObjectItem();
    }

}
