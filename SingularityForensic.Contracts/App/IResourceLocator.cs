using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.App {
    public interface IResourceLocator {
        object FindResource(string keyName);
    }

    public class ResourceLocator : GenericServiceStaticInstance<IResourceLocator> {
        public static object FindResource(string keyName) => Current?.FindResource(keyName);
    }
}
