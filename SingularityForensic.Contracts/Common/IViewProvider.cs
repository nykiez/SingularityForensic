using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// 视图提供器契约,避免单元测试因无法满足的条件而不能正常进行;
    /// </summary>
    public interface IViewProvider {
        object GetView(string viewName);
    }

    public static class ViewProvider {
        public static IViewProvider Current {
            get {
                if (IsViewProviderProvided) {
                    return _serviceProvider;
                }
                throw new InvalidOperationException("ViewProvidder has not been set!");
            }
        }

        public static bool IsViewProviderProvided => _serviceProvider != null;

        private static IViewProvider _serviceProvider;

        public static void SetViewProvider(IViewProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public static object GetView(string viewName) => Current?.GetView(viewName);

    }
}
