using SingularityForensic.Contracts.App;
using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Windows;

namespace SingularityForensic.App {
    [Export(typeof(IThreadInvoker))]
    class ThreadInvokerImpl : IThreadInvoker {
        public void BackInvoke(Action act) {
            if(act == null) {
                throw new ArgumentNullException(nameof(act));
            }

            ThreadPool.QueueUserWorkItem(cb => {
                act.Invoke();
            });
        }
        
        private readonly AutoResetEvent evt = new AutoResetEvent(false);
        public void UIInvoke(Action act) {
            if (act == null) {
                throw new ArgumentNullException(nameof(act));
            }
            

            Application.Current.Dispatcher.Invoke(act);
        }
    }
}
