﻿using Prism.Mef.Modularity;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace SingularityForensic.Controls
{
    [ModuleExport(typeof(ControlsModule))]
    public class ControlsModule : IModule {
        public void Initialize() {
            LocalizationManager.Manager = new LanguageServiceToTelerikAdapter();
        }
    }


}
