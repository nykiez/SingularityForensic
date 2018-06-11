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
        string GetSaveFilePath(string defaultFileName = null);

        /// <summary>
        /// 获得目录;
        /// </summary>
        /// <returns></returns>
        string OpenDirect();

        /// <summary>
        /// 单行输入框;
        /// </summary>
        /// <param name="title">窗体标题</param>
        /// <param name="desc">详细</param>
        /// <param name="val">初始值</param>
        /// <returns>输入值</returns>
        string GetInputValue(string title = null, string desc = null, string val = null);

        ILoadingDialog CreateLoadingDialog();

        IDoubleLoadingDialog CreateDoubleLoadingDialog();
    }


    /// <summary>
    /// 进度报告器,便于在调用同一个动作时,便于使用不同的展示方式;
    /// </summary>
    public interface IProgressReporter {
        /// <summary>
        /// 通知进度;
        /// </summary>
        /// <param name="percentProgress"></param>
        void ReportProgress(int percentProgress);
        void ReportProgress(int percentProgress, string text, string descrip);
        void ReportProgress(int totalPer, int detailPer, string desc, string detail);

        /// <summary>
        /// 通知事件;
        /// </summary>
        event EventHandler<(int totalPer, int detailPer, string desc, string detail)> ProgressReported;

        /// <summary>
        /// 是否已经取消;
        /// </summary>
        bool CancelPending { get; }
        /// <summary>
        /// 取消;
        /// </summary>
        void Cancel();

        /// <summary>
        /// 取消事件;
        /// </summary>
        event EventHandler Canceld;

        /// <summary>
        /// 标题;
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 标题变更事件;
        /// </summary>
        event EventHandler<string> TitleChanged;
    }

    /// <summary>
    /// 进度报告器工厂;
    /// </summary>
    public interface IProgessReporterFactory {
        IProgressReporter CreateNew();
    }

    public class ProgessReporterFactory : GenericServiceStaticInstance<IProgessReporterFactory> {
        public static IProgressReporter CreateNew() => Current?.CreateNew();
    }

    
    
    public class DialogService: GenericServiceStaticInstance<IDialogService>{

    }

    public interface ILoadingDialog {
        bool CancellationPending { get; }
        string Word { get; set; }
        string Description { get; set; }
        string WindowTitle { get; set; }
        bool IsProgressVisible { get; set; }

        event DoWorkEventHandler DoWork;
        event RunWorkerCompletedEventHandler RunWorkerCompleted;
        event EventHandler Canceld;

        void ReportProgress(int percentProgress);
        void ReportProgress(int percentProgress, string text, string descrip);
        void ShowDialog(object owner = null);
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
