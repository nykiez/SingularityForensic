using Moq;
using Prism.Events;
using Prism.Mef.Events;
using SingularityForensic.Contracts.Common;
using System.Windows;

namespace SingularityForensic.Test.Common {
    public static class CommonMockers {
        private static IEventAggregator _aggregator;
        public static IEventAggregator AggregatorMocker {
            get {
                if(_aggregator == null) {
                    _aggregator = new MefEventAggregator();
                    
                }
                return _aggregator;
            }
        }
        
        private static IViewProvider _viewProvider;
        public static IViewProvider ViewProviderMocker {
            get {
                if(_viewProvider == null) {
                    var mocker = new Mock<IViewProvider>();
                    mocker.Setup(p => p.GetView(It.IsAny<string>())).Returns(null);
                    _viewProvider = mocker.Object;
                }
                return _viewProvider;
            }
        }
    }
}
