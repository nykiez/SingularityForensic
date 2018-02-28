using System;

namespace SingularityForensic.Contracts.Common {
    public static class ServiceProvider {
        public static IServiceProvider Current {
            get {
                if (IsServiceProviderProvided) {
                    return _serviceProvider ?? _serviceProviderFunc();
                }
                throw new InvalidOperationException("ServiceLocator has not been set!");
            }
        }

        public static bool IsServiceProviderProvided => _serviceProvider != null || _serviceProviderFunc != null;

        private static IServiceProvider _serviceProvider;
        private static Func<IServiceProvider> _serviceProviderFunc;

        public static void SetServiceProvider(Func<IServiceProvider> serviceProviderFunc) {
            _serviceProviderFunc = serviceProviderFunc;
        }

        public static void SetServiceProvider(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
    }
}
