using Prism.Mvvm;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Splash.ViewModels {
    [Export]
    class SplashViewModel:BindableBase {
        public SplashViewModel() {
            //PubEventHelper.GetEvent<Splash>
        }
        private void RegisterEvents() {

        }
    }
}
