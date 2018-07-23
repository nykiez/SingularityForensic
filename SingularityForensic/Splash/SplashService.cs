using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Splash;
using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Windows.Threading;

namespace SingularityForensic.Splash {
    [Export(typeof(ISplashService))]
    public class SplashService : ISplashService {
        private Views.Splash SWin => ServiceProvider.Current.GetInstance<Views.Splash>();
        
        public void ReportMessage(string msg) {
            
        }

        public void CloseSplash() {
            var splash = ServiceProvider.Current.GetInstance<Views.Splash>();
            splash.Dispatcher.BeginInvoke((Action)splash.Close);
        }

        public void ShowSplash() {
            var WaitForCreation = new AutoResetEvent(false);
            var thread = new Thread(() => {
                Dispatcher.CurrentDispatcher.BeginInvoke(
                      (Action)(() => {
                          //Container.RegisterType<SplashViewModel, SplashViewModel>();
                          //Container.RegisterType<SplashView, SplashView>();

                          var splash = ServiceProvider.Current.GetInstance<Views.Splash>();
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
