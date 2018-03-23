using Prism.Events;
using Prism.Mef.Events;

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
    }
}
