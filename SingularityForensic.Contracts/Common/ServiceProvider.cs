using System;

namespace SingularityForensic.Contracts.Common {
    public static class ServiceProvider {
        public static IServiceProvider Current {
            get {
                if (IsServiceProviderProvided) {
                    return _serviceProvider;
                }
                throw new InvalidOperationException("ServiceProvidder has not been set!");
            }
        }

        public static bool IsServiceProviderProvided => _serviceProvider != null;

        private static IServiceProvider _serviceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
    }
}
