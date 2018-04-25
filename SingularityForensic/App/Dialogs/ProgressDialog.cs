using SingularityForensic.App.Views;
using SingularityForensic.Contracts.App;
using System;
using System.ComponentModel;
using System.Windows;

namespace SingularityForensic.App.Dialogs {
    public class ProgressDialog :ILoadingDialog {
        public event DoWorkEventHandler DoWork;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        private readonly ProgressWindow window = new ProgressWindow();
        public ProgressDialog () {
            window.Canceld += Window_Canceld;
        }

        private void Window_Canceld(object sender, EventArgs e) {
            this.Canceld?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler Canceld;

        public void ReportProgress(int percentProgress) {
            window.Dispatcher.Invoke(() => {
                window.Pro = percentProgress;
            });
        }
        public void ReportProgress(int percentProgress, string text, string descrip) {
            window.Dispatcher.Invoke(() => {
                window.Pro = percentProgress;
                Word = text;
                Description = descrip;
            });
        }

        public bool CancellationPending {
            get {
                return window.CancellationPending;
            }
        }

        public string Word {
            get {
                return window.Word;
            }
            set {
                window.Word = value;
            }
        }

        public string Description {
            get {
                return window.Desc;
            }
            set {
                if (window.CancellationPending) {
                    return;
                }
                window.Desc = value;
            }
        }

        public string WindowTitle {
            get {
                return window.Title;
            }
            set {
                window.Title = value;
            }
        }

        public void Show() {
            ShowCore(null, false);
        }

        public void ShowDialog(object owner = null) {
            ShowCore(owner as Window, true);
        }

        private void ShowCore(Window owner = null, bool showAsDialog = true) {
            if (owner != null) {
                window.Owner = owner;
            }
            else {
                window.Owner = Application.Current.MainWindow;
            }

            //window.ShowInTaskbar = false;
            ThreadInvoker.BackInvoke(() => {
                try {
                    DoWork?.Invoke(this, new DoWorkEventArgs(this));
                    window.Dispatcher.Invoke(() => {
                        window.Close();
                        RunWorkerCompleted?.Invoke(this, new RunWorkerCompletedEventArgs(null, null, CancellationPending));
                    });
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                    window.Dispatcher.Invoke(() => {
                        window.Close();
                        RunWorkerCompleted?.Invoke(this, new RunWorkerCompletedEventArgs(null, ex, CancellationPending));
                    });
                }
            });

            if (showAsDialog) {
                window.ShowDialog();
            }
            else {
                window.Show();
            }
        }
    }
}
