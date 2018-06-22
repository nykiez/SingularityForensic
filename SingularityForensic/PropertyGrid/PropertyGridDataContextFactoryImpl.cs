using SingularityForensic.Contracts.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.PropertyGrid {
    [Export(typeof(IPropertyGridDataContextFactory))]
    class PropertyGridDataContextFactoryImpl : IPropertyGridDataContextFactory {
        public IPropertyGridDataContext CreateNew() => new PropertyGridDataContext();
    }
}
