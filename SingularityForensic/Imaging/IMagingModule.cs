using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Imaging {
    [ModuleExport(typeof(ImagingModule))]
    class ImagingModule : IModule {
        [ImportingConstructor]
        public ImagingModule(IImagingService imagingService) {
            this._imgingService = imagingService;
            
        }
        public void Initialize() {
            _imgingService.Initialize();
        }

        private IImagingService _imgingService;
    }
}
