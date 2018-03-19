using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SingularityForensic.App {
    [Export(typeof(IThreadInvoker))]
    class ThreadInvoker : IThreadInvoker {
        public void BackInvoke(Action act) {
            if(act == null) {
                throw new ArgumentNullException(nameof(act));
            }

            ThreadPool.QueueUserWorkItem(cb => {
                act.Invoke();
            });
        }

        public void UIInvoke(Action act) {
            if (act == null) {
                throw new ArgumentNullException(nameof(act));
            }

            Application.Current.Dispatcher.Invoke(act);
        }
    }
}
