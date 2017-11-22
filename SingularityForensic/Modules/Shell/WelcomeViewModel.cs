using Prism.Commands;
using Prism.Mvvm;
using System.Diagnostics;

namespace SingularityForensic.ViewModels.Shell {
    public partial class WelcomeViewModel:BindableBase {
        private static readonly string originSite = "http://www.cflab.net";
        private DelegateCommand openOriginSiteCommand;
        public DelegateCommand OpenOriginSiteCommand {
            get {
                return openOriginSiteCommand ??
                    (openOriginSiteCommand = new DelegateCommand(() => {
                        Process.Start(originSite);
                    }));
            }
        }
    }
}
