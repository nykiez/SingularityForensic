using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Contracts.Splash;
using SingularityForensic.Splash.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SingularityForensic.Splash {
    [Export(typeof(ISplashService))]
    public class SplashService : ISplashService {
        private SplashView sWin => ServiceProvider.Current.GetInstance<SplashView>();
        
        public void ReportMessage(string msg) {
            
        }

        public void CloseSplash() {
            var splash = ServiceProvider.Current.GetInstance<SplashView>();
            splash.Dispatcher.BeginInvoke((Action)splash.Close);
        }

        public void ShowSplash() {
            var WaitForCreation = new AutoResetEvent(false);
            var thread = new Thread(() => {
                Dispatcher.CurrentDispatcher.BeginInvoke(
                      (Action)(() => {
                          //Container.RegisterType<SplashViewModel, SplashViewModel>();
                          //Container.RegisterType<SplashView, SplashView>();

                          var splash = ServiceProvider.Current.GetInstance<SplashView>();
                          splash.Show();

                          WaitForCreation.Set();
                      }));

                Dispatcher.Run();
            }) { Name = "Splash Thread", IsBackground = true };

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            WaitForCreation.WaitOne();
        }
    }
}
