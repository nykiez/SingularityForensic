using CDFCMessageBoxes.MessageBoxes;
using Ookii.Dialogs.Wpf;
using SingularityForensic.Contracts.App;
using SingularityForensic.Controls.MessageBoxes.MessageBoxes;
using SingularityForensic.Controls.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.App
{
    /// <summary>
    /// 对话框服务;
    /// </summary>
    [Export(typeof(IDialogService))]
    class DialogService : IDialogService {
        public string SaveFile() {
            var dialog = new VistaSaveFileDialog();
            if (dialog.ShowDialog() == true) {
                return dialog.FileName;
            }
            return null;
        }
        public string OpenFile() {
            var dialog = new VistaOpenFileDialog();
            if(dialog.ShowDialog() == true) {
                return dialog.FileName;
            }
            return null;
        }

        public ILoadingDialog CreateLoadingDialog() {
            return new ProcessDialog();
        }

        public IDoubleLoadingDialog CreateDoubleLoadingDialog() => new DoubleProcessDialog();
    }

    public class ProcessDialog : ILoadingDialog {

        public bool CancellationPending => _msgBox.CancellationPending;
        private ProgressMessageBox _msgBox = new ProgressMessageBox();

        public string Word { get => _msgBox.Word; set => _msgBox.Word = value; }
        public string Description { get => _msgBox.Description; set => _msgBox.Description = value; }
        public string WindowTitle { get => _msgBox.WindowTitle; set => _msgBox.WindowTitle = value; }

        public event DoWorkEventHandler DoWork {
            add {
                _msgBox.DoWork += value;
            }
            remove {
                _msgBox.DoWork -= value;
            }
        }
        public event RunWorkerCompletedEventHandler RunWorkerCompleted {
            add {
                _msgBox.RunWorkerCompleted += value;
            }
            remove {
                _msgBox.RunWorkerCompleted -= value;
            }
        }

        public void ReportProgress(int percentProgress) {
            _msgBox.ReportProgress(percentProgress);
        }

        public void ReportProgress(int percentProgress, string text, string descrip) {
            _msgBox.ReportProgress(percentProgress, text, descrip);
        }

        public void ShowDialog(Window owner = null) {
            _msgBox.ShowDialog(owner);
        }
    }

    public class DoubleProcessDialog:IDoubleLoadingDialog {
        public event DoWorkEventHandler DoWork;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
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
