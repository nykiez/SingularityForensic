using SingularityForensic.Contracts.Hash;
using SingularityForensic.Contracts.Hash.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash.Events {
    /// <summary>
    /// 哈希模块加载时初始化哈希集管理服务;
    /// </summary>
    [Export(typeof(IHashModuleLoadingEventHandler))]
    class OnHashModuleLoadingHashSetManagementServiceInitializingHandler : IHashModuleLoadingEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle() {
            HashSetManagementService.Current.Initialize();
        }
    }
}
