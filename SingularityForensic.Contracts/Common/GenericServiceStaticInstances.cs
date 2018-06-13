using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public abstract class GenericServiceStaticInstances<TService> where TService : class {
        private static IEnumerable<TService> _currents;
        public static IEnumerable<TService> Currents => _currents ?? (_currents = ServiceProvider.GetAllInstances<TService>());
    }
}
