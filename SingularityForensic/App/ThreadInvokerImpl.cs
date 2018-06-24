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

        /// <summary>
        /// UI线程状态;priLevel为优先级;done表示是否完成;
        /// </summary>
        private (uint priLevel, bool done) uiState;

        private AutoResetEvent evt = new AutoResetEvent(false);
        public void UIInvoke(Action act) {
            if (act == null) {
                throw new ArgumentNullException(nameof(act));
            }
            

            Application.Current.Dispatcher.Invoke(act);
        }
    }
}
