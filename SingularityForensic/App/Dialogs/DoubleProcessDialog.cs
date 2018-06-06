using SingularityForensic.App.Views;
using SingularityForensic.Contracts.App;
using System;
using System.ComponentModel;
using System.Windows;

namespace SingularityForensic.App.Dialogs {
    public class DoubleProcessDialog : IDoubleLoadingDialog {
        public event DoWorkEventHandler DoWork;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        public event EventHandler Canceld;

        private readonly DoubleProcessWindow window = new DoubleProcessWindow();
        public void ReportProgress(int totalPer, int detailPer, string desc, string detail) {
            window.Dispatcher.Invoke(() => {
                window.ProCap = totalPer;
                window.ProDetail = detailPer;
                window.Desc = desc;
                window.Detail = detail;
            });
        }

        public bool CancellationPending {
            get {
                return window.CancellationPending;
            }
        }

        public void ShowDialog() {
            RunTask();
            window.Canceld += delegate {
                Canceld?.Invoke(this, EventArgs.Empty);
            };
            window.ShowDialog();
        }
        private void RunTask() {
            ThreadInvoker.BackInvoke(() => {
                try {
                    DoWork?.Invoke(this, new DoWorkEventArgs(this));
                    window.Dispatcher.Invoke(() => {
                        window.Close();
                        RunWorkerCompleted?.Invoke(this, new RunWorkerCompletedEventArgs(null, null, CancellationPending));
                    });
                }
                catch (Exception ex) {
                    window.Dispatcher.Invoke(() => {
                        window.Close();
                        RunWorkerCompleted?.Invoke(this, new RunWorkerCompletedEventArgs(null, ex, CancellationPending));
                    });
                }
            });
            window.Owner = Application.Current.MainWindow;
            window.ShowInTaskbar = false;
        }

        public void Show() {
            RunTask();
            window.Show();
        }

        public string Title {
            get => window.Title;
            set => window.Title = value;
        }
    }
}
