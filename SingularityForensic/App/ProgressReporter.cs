using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.App {
    /// <summary>
    /// 进度报告器,便于在调用同一个动作时,便于使用不同的展示方式;
    /// </summary>
    public class ProgressReporter : IProgressReporter {
        public void ReportProgress(int percentProgress) {
            ReportProgress(percentProgress, string.Empty, string.Empty);
        }
        public void ReportProgress(int percentProgress, string text, string descrip) {
            ProgressReported?.Invoke(this, (percentProgress, text, descrip));
        }

        public event EventHandler<(int pro, string text, string descrip)> ProgressReported;

        public void ReportProgress(int totalPer, int detailPer, string desc, string detail) {
            DoubleProgressReported?.Invoke(this, (totalPer, detailPer, desc, detail));
        }

        public event EventHandler<(int totalPer, int detailPer, string desc, string detail)> DoubleProgressReported;

        private string _title;
        public string Title {
            get => _title;
            set {
                _title = value;
                TitleChanged?.Invoke(this, _title);
            }
        }

        public event EventHandler<string> TitleChanged;

        //取消工作;
        public void Cancel() {
            CancelPending = true;
        }

        //是否正在取消;
        public bool CancelPending { get; private set; }
    }

    [Export(typeof(IProgessReporterFactory))]
    public class ProgressReporterFactory : IProgessReporterFactory {
        public IProgressReporter CreateNew() => new ProgressReporter();
    }
}
