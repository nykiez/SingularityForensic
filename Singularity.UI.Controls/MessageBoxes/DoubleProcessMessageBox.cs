using Singularity.UI.MessageBoxes.Windows;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace Singularity.UI.MessageBoxes.MessageBoxes {
    public class DoubleProcessMessageBox {
        public event DoWorkEventHandler DoWork;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        private readonly DoubleProcessWindow window = new DoubleProcessWindow();
        public void ReportProgress(int totalPer,int detailPer,string desc,string detail) {
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
        
        public void ShowDialog(Window owner = null) {
            ThreadPool.QueueUserWorkItem(cb => {
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
            if (owner != null) {
                window.Owner = owner;
            }
            else {
                window.Owner = Application.Current.MainWindow;
            }

            window.ShowDialog();
        }

        public string Title {
            get => window.Title;
            set => window.Title = value;
        }
    }
}
