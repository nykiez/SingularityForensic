using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.StatusBar {
    public interface IStatusBarItemFactory {
        IStatusBarObjectItem CreateStatusBarObjectItem(object content, string guid);
        IStatusBarTextItem CreateStatusBarTextItem(string guid);
    }

    public class StatusBarItemFactory :GenericServiceStaticInstance<IStatusBarItemFactory>{
        public static IStatusBarObjectItem CreateStatusBarObjectItem(object content, string guid) =>
            Current.CreateStatusBarObjectItem(content, guid);
        public static IStatusBarTextItem CreateStatusBarTextItem(string guid) =>
            Current.CreateStatusBarTextItem(guid);
    }
}
