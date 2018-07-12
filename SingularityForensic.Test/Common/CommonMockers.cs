using Moq;
using Prism.Events;
using Prism.Mef.Events;
using SingularityForensic.Contracts.Common;
using System;

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

        private static ILoggerService _loggerService;
        public static ILoggerService LoggerServiceMocker {
            get {
                if(_loggerService == null) {
                    var logService = new Mock<ILoggerService>();
                    logService.Setup(p => p.WriteException(It.IsAny<Exception>(),It.IsAny<string>())).Callback<Exception>(ex => throw ex);
                    //logService.Setup(p => p.WriteCallerLine(It.IsAny<string>(),It.IsAny<string>())).Throws(new System.Exception());
                    //logService.Setup(p => p.WriteLine(It.IsAny<string>())).Throws(new System.Exception());
                    _loggerService = logService.Object;
                }

                return _loggerService;
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
