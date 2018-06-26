using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// UI元素提供者;
    /// </summary>
    public interface IUIObjectProvider {
        /// <summary>
        /// UI元素;
        /// </summary>
        object UIObject { get; }
    }
    

    public interface IUIObjectProviderFactory {
        IUIObjectProvider CreateNew(object uiObject);
    }

    public class UIObjectProviderFactory:GenericServiceStaticInstance<IUIObjectProviderFactory> {
        public static IUIObjectProvider CreateNew(object uiObject) => Current.CreateNew(uiObject);
    }
}
