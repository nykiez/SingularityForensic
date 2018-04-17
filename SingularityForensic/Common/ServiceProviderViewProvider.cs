using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Common {
    public class ServiceProviderViewProvider : IViewProvider {
        public ServiceProviderViewProvider(Contracts.Common.IServiceProvider serviceProvider) {
            this._seviceProvider = serviceProvider;
        }
        private Contracts.Common.IServiceProvider _seviceProvider;
        public object GetView(string viewName) => _seviceProvider.GetInstance<FrameworkElement>(viewName);
    }
}
