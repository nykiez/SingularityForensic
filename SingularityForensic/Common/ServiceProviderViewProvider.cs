using SingularityForensic.Contracts.Common;
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
