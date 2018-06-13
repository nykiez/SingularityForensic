using CDFC.Util;
using System;
using System.Collections.Generic;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// 默认空服务提供者;
    /// </summary>
    public abstract class EmptyServiceProvider<TProvider> :
        GenericStaticInstance<TProvider> , IServiceProvider
        where TProvider :class,new() {
        public TService AddInstance<TService>(string key) {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<object> GetAllInstances(Type serviceType) => null;
        public virtual IEnumerable<TService> GetAllInstances<TService>() {
            IEnumerable<object> services = GetAllInstances(typeof(TService));
            if( services != null){
                return (IEnumerable<TService>)services;
            }
            return null;
        }

        public virtual object GetInstance(Type serviceType) => null;
        public virtual TService GetInstance<TService>() {
            var service = GetInstance(typeof(TService));
            if(service != null) {
                return (TService)service;
            }
            return default(TService);
        }

        public virtual TService GetInstance<TService>(string key) {
            object service = null;
            if((service = GetInstance(typeof(TService),key)) != null) {
                return (TService)service;
            }
            return default(TService);
        }
        public virtual object GetInstance(Type serviceType, string key) => null;
    }
}
