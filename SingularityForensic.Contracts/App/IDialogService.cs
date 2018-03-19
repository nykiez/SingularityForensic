using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Contracts.App {
    /// <summary>
    /// 对话框服务契约;
    /// </summary>
    public interface IDialogService {
        /// <summary>
        /// 获得文件路径;
        /// </summary>
        /// <returns></returns>
        string OpenFile();

        /// <summary>
        /// 获得文件路径;
        /// </summary>
        /// <param name="filter">后缀过滤</param>
        /// <returns></returns>
        string OpenFile(string filter);

        /// <summary>
        /// 获得保存文件路径;
        /// </summary>
        /// <returns></returns>
        string SaveFile();

        

        ILoadingDialog CreateLoadingDialog();
        IDoubleLoadingDialog CreateDoubleLoadingDialog();
    }
    
    //进度报告器,便于在调用同一个动作时,便于使用不同的展示方式;
    public class ProgressReporter {
        public void ReportProgress(int percentProgress) {
            ReportProgress(percentProgress, string.Empty, string.Empty);
        }
        public void ReportProgress(int percentProgress, string text, string descrip) {
            ProgressReported?.Invoke(this,(percentProgress, text, descrip));
        }

        public event EventHandler<(int pro, string text, string descrip)> ProgressReported;

        void ReportProgress(int totalPer, int detailPer, string desc, string detail) {
            DoubleProgressReported?.Invoke(this,(totalPer, detailPer, desc, detail));
        }

        public event EventHandler<(int totalPer, int detailPer, string desc, string detail)> DoubleProgressReported;

        public string Title { set => TitleChanged?.Invoke(this, value); }
        public event EventHandler<string> TitleChanged;

        //取消工作;
        public void Cancel() {
            CancelPending = true;
        }

        //是否正在取消;
        public bool CancelPending { get; private set; }
    }
    
    public class DialogService: GenericServiceStaticInstance<IDialogService>{

    }

    public interface ILoadingDialog {
        bool CancellationPending { get; }
        string Word { get; set; }
        string Description { get; set; }
        string WindowTitle { get; set; }
        
        event DoWorkEventHandler DoWork;
        event RunWorkerCompletedEventHandler RunWorkerCompleted;
        event EventHandler Canceld;

        void ReportProgress(int percentProgress);
        void ReportProgress(int percentProgress, string text, string descrip);
        void ShowDialog();
        void Show();
    }

    public interface IDoubleLoadingDialog {
        event DoWorkEventHandler DoWork;
        event RunWorkerCompletedEventHandler RunWorkerCompleted;
        event EventHandler Canceld;

        void ReportProgress(int totalPer, int detailPer, string desc, string detail);

        bool CancellationPending { get; }

        void ShowDialog();
        void Show();

        string Title { get; set; }
    }
}
