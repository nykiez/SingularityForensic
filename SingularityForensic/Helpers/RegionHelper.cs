using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using System;

namespace SingularityForensic.Helpers {
    public static class RegionHelper {
        private static IRegionManager _regionManager;
        public static IRegionManager RegionManager => _regionManager ?? (_regionManager = ServiceLocator.Current.GetInstance<IRegionManager>());

        public static void RequestNavigate(string regionName, Uri source) => RegionManager?.RequestNavigate(regionName, source);
        public static void RequestNavigate(string regionName, string source) => RegionManager?.RequestNavigate(regionName, source);

        public static IRegionManager RegisterViewWithRegion(string regionName, Type viewType) => RegionManager?.RegisterViewWithRegion(regionName, viewType);
        public static IRegionManager RegisterViewWithRegion(string regionName, Func<object> getContentDelegate) => RegionManager?.RegisterViewWithRegion(regionName, getContentDelegate);
    }
}
