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
}
