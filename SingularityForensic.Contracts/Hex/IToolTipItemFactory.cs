﻿
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Contracts.Hex {
    public interface IToolTipItemFactory {
        IToolTipDataItem CreateToolTipDataItem();
        IToolTipObjectItem CreateToolTipObjectItem();
    }
    public class ToolTipItemFactory:GenericServiceStaticInstance<IToolTipItemFactory> {
        public static IToolTipDataItem CreateIToolTipDataItem() => Current?.CreateToolTipDataItem();
        public static IToolTipObjectItem CreateToolTipObjectItem() => Current?.CreateToolTipObjectItem();
    }

}
