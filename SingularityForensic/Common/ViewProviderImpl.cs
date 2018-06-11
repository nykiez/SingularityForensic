using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Common.Events;
using SingularityForensic.Contracts.Helpers;
using System.Collections.Generic;
using System.Windows;

namespace SingularityForensic.Common {
    public class ViewProviderImpl : IViewProvider {
        public ViewProviderImpl(Contracts.Common.IServiceProvider serviceProvider) {
            this._serviceProvider = serviceProvider;
        }
        private Contracts.Common.IServiceProvider _serviceProvider;
        public object GetView(string viewName) => _serviceProvider.GetInstance<FrameworkElement>(viewName);

        private IEnumerable<IViewCreatedEventHandler> _viewCreatedEventHandlers;
        IEnumerable<IViewCreatedEventHandler> ViewCreateEventHandlers =>
            _viewCreatedEventHandlers ?? (_viewCreatedEventHandlers = _serviceProvider.GetAllInstances<IViewCreatedEventHandler>());

        public object CreateView(string viewName, object dataContext) {
            var elem = _serviceProvider.GetInstance<FrameworkElement>(viewName);
            if(elem != null) {
                elem.DataContext = dataContext;
                
            }
            PubEventHelper.GetEvent<ViewCreatedEvent>().Publish((elem as object,viewName));
            PubEventHelper.PublishEventToHandlers((elem as object,viewName), ViewCreateEventHandlers);
            return elem;
        }


    }
}
