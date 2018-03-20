using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.App {
    public class DoubleLoadingDialogMocker : IDoubleLoadingDialog {
        public bool CancellationPending => false;

        public string Title { get; set; }

        public event DoWorkEventHandler DoWork;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        public event EventHandler Canceld;

        public void ReportProgress(int totalPer, int detailPer, string desc, string detail) {
            Trace.WriteLine($"{totalPer} - {detailPer} - {desc} - {detail}");
        }

        public void Show() {
            ShowDialog();
        }

        public void ShowDialog() {
            DoWork?.Invoke(this, new DoWorkEventArgs(null));
            RunWorkerCompleted?.Invoke(this, new RunWorkerCompletedEventArgs(null, null, false));
        }
    }

    public class LoadingDialogMocker : ILoadingDialog {
        public bool CancellationPending => throw new NotImplementedException();

        public string Word { get; set; }
        public string Description { get; set; }
        public string WindowTitle { get; set; }

        public event DoWorkEventHandler DoWork;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        public event EventHandler Canceld;

        public void ReportProgress(int percentProgress) {
            Trace.WriteLine($"{percentProgress}");
        }

        public void ReportProgress(int percentProgress, string text, string descrip) {
            Trace.WriteLine($"{percentProgress} - {text} - {descrip} ");
        }

        public void Show() {
            ShowDialog();
        }

        public void ShowDialog() {
            DoWork?.Invoke(this, new DoWorkEventArgs(null));
            RunWorkerCompleted?.Invoke(this, new RunWorkerCompletedEventArgs(null, null, false));
        }
    }
}
