using SingularityForensic.Contracts.TreeView;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.TreeView {
    [Export(typeof(IPropertyGridDataContextFactory))]
    class PropertyListDataContextFactoryImpl : IPropertyGridDataContextFactory {
        public IPropertyListDataContext CreateNew() => new PropertyListDataContext();
    }
}
