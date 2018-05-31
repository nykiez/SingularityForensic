using SingularityForensic.Contracts.StatusBar;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.StatusBar {

    [Export(typeof(IStatusBarItemFactory))]
    class StatusBarItemFactoryImpl : IStatusBarItemFactory {
        public IStatusBarObjectItem CreateStatusBarObjectItem(object content, string guid) => new StatusBarObjectItem(guid, content);

        public IStatusBarTextItem CreateStatusBarTextItem(string guid) => new StatusBarTextItem(guid);
    }
}
