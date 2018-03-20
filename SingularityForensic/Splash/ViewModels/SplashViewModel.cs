using Prism.Mvvm;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Splash.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Splash.ViewModels {
    [Export]
    public class SplashViewModel:BindableBase {
        public SplashViewModel() {
            RegisterEvents();
        }
        private void RegisterEvents() {
            PubEventHelper.GetEvent<SplashMessageEvent>().Subscribe(msg => {
                LoadingText = msg;
            });
        }


        private string _loadingText;
        public string LoadingText {
            get => _loadingText;
            set => SetProperty(ref _loadingText, value);
        }

    }
}
