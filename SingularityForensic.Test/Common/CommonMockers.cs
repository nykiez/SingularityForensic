using Prism.Events;
using Prism.Mef.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
