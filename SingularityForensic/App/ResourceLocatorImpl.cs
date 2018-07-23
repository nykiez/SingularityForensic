using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.App {
    [Export(typeof(IResourceLocator))]
    class ResourceLocatorImpl : IResourceLocator {
        public object FindResource(string keyName) {
            return Application.Current.Resources[keyName];
        }
    }
}
