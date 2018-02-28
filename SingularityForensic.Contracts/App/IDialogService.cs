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
        //打开文件;
        string OpenFile();
        //保存文件;
        string SaveFile();

        ILoadingDialog CreateLoadingDialog();
        IDoubleLoadingDialog CreateDoubleLoadingDialog();
    }
    


    public interface ILoadingDialog {
        bool CancellationPending { get; }
        string Word { get; set; }
        string Description { get; set; }
        string WindowTitle { get; set; }

        event DoWorkEventHandler DoWork;
        event RunWorkerCompletedEventHandler RunWorkerCompleted;

        void ReportProgress(int percentProgress);
        void ReportProgress(int percentProgress, string text, string descrip);
        void ShowDialog(Window owner = null);
    }

    public interface IDoubleLoadingDialog {
        event DoWorkEventHandler DoWork;
        event RunWorkerCompletedEventHandler RunWorkerCompleted;
        void ReportProgress(int totalPer, int detailPer, string desc, string detail);

        bool CancellationPending { get; }

        void ShowDialog(Window owner = null);

        string Title { get; set; }
    }
}
