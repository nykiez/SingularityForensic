using System;
using System.Collections.Generic;

namespace SingularityForensic.Contracts.Common {
    public interface IServiceProvider {
        //
        // Summary:
        //     Get all instances of the given serviceType currently registered in the container.
        //
        // Parameters:
        //   serviceType:
        //     Type of object requested.
        //
        // Returns:
        //     A sequence of instances of the requested serviceType.
        //
        // Exceptions:
        //   T:Microsoft.Practices.ServiceLocation.ActivationException:
        //     if there is are errors resolving the service instance.
        IEnumerable<object> GetAllInstances(Type serviceType);
        //
        // Summary:
        //     Get all instances of the given TService currently registered in the container.
        //
        // Type parameters:
        //   TService:
        //     Type of object requested.
        //
        // Returns:
        //     A sequence of instances of the requested TService.
        //
        // Exceptions:
        //   T:Microsoft.Practices.ServiceLocation.ActivationException:
        //     if there is are errors resolving the service instance.
        IEnumerable<TService> GetAllInstances<TService>();
        //
        // Summary:
        //     Get an instance of the given serviceType.
        //
        // Parameters:
        //   serviceType:
        //     Type of object requested.
        //
        // Returns:
        //     The requested service instance.
        //
        // Exceptions:
        //   T:Microsoft.Practices.ServiceLocation.ActivationException:
        //     if there is an error resolving the service instance.
        object GetInstance(Type serviceType);
        //
        // Summary:
        //     Get an instance of the given named serviceType.
        //
        // Parameters:
        //   serviceType:
        //     Type of object requested.
        //
        //   key:
        //     Name the object was registered with.
        //
        // Returns:
        //     The requested service instance.
        //
        // Exceptions:
        //   T:Microsoft.Practices.ServiceLocation.ActivationException:
        //     if there is an error resolving the service instance.
        object GetInstance(Type serviceType, string key);

        //
        // Summary:
        //     Get an instance of the given TService.
        //
        // Type parameters:
        //   TService:
        //     Type of object requested.
        //
        // Returns:
        //     The requested service instance.
        //
        // Exceptions:
        //   T:Microsoft.Practices.ServiceLocation.ActivationException:
        //     if there is are errors resolving the service instance.
        TService GetInstance<TService>();

        //
        // Summary:
        //     Get an instance of the given named TService.
        //
        // Parameters:
        //   key:
        //     Name the object was registered with.
        //
        // Type parameters:
        //   TService:
        //     Type of object requested.
        //
        // Returns:
        //     The requested service instance.
        //
        // Exceptions:
        //   T:Microsoft.Practices.ServiceLocation.ActivationException:
        //     if there is are errors resolving the service instance.
        TService GetInstance<TService>(string key);
    }

    public static class ServiceProvider {
        public static IServiceProvider Current => _serviceProvider;
        
        private static IServiceProvider _serviceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public static TService GetInstance<TService>() where TService : class {
            if (Current == null) {
                throw new InvalidOperationException("ServiceProvidder has not been set!");
            }

            return Current.GetInstance<TService>();
        }

        public static IEnumerable<TService> GetAllInstances<TService>() where TService : class {
            if (Current == null) {
                throw new InvalidOperationException("ServiceProvidder has not been set!");
            }

            return Current.GetAllInstances<TService>();
        }

        public static TService GetInstance<TService>(string key) where TService : class {
            if (Current == null) {
                throw new InvalidOperationException("ServiceProvidder has not been set!");
            }

            return Current.GetInstance<TService>(key);
        }
    }
}
