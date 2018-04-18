using CDFCMessageBoxes.MessageBoxes;
using Ookii.Dialogs.Wpf;
using SingularityForensic.App.MessaggeBoxes;
using SingularityForensic.Contracts.App;
using SingularityForensic.Controls.Windows;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading;
using System.Windows;

namespace SingularityForensic.App {
    /// <summary>
    /// 对话框服务;
    /// </summary>
    [Export(typeof(IDialogService))]
    class DialogService : IDialogService {
        public string GetSaveFilePath(string defaultFileName = null) {
            var dialog = new VistaSaveFileDialog {
                FileName = defaultFileName
            };
            if (dialog.ShowDialog() == true) {
                return dialog.FileName;
            }
            return null;
        }

        public string OpenFile() {
            return OpenFile(string.Empty);
        }

        public ILoadingDialog CreateLoadingDialog() {
            return new ProcessDialog();
        }

        public IDoubleLoadingDialog CreateDoubleLoadingDialog() => new DoubleProcessDialog();

        public string OpenFile(string filter) {
            var dialog = new VistaOpenFileDialog();
            dialog.Filter = filter;
            if (dialog.ShowDialog() == true) {
                return dialog.FileName;
            }
            return null;
        }

        public string GetDirect() {
            var dialog = new VistaFolderBrowserDialog();
            if(dialog.ShowDialog() == true) {
                return dialog.SelectedPath;
            }

            return string.Empty;
        }

        public string GetInputValue(string title = "", string desc = "", string val = "") {
            return InputValueMessageBox.Show(title, desc,val);
        }
    }

    public class ProcessDialog : ILoadingDialog {

        public bool CancellationPending => _msgBox.CancellationPending;
        private ProgressMessageBox _msgBox = new ProgressMessageBox();

        public event EventHandler Canceld;

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

        
        public void ShowDialog(object owner = null) {
            if(owner is Window window && window.IsVisible) {

                _msgBox.ShowDialog(window);
            }
            else {
                _msgBox.ShowDialog();
            }
        }

        public void Show() {
            _msgBox.ShowDialog();
        }
    }

    public class DoubleProcessDialog:IDoubleLoadingDialog {
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
            window.ShowDialog();
        }
        private void RunTask() {
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
