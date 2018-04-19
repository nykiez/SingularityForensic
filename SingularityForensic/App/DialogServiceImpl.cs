using Ookii.Dialogs.Wpf;
using SingularityForensic.App.Dialogs;
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
    class DialogServiceImpl : IDialogService {
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
            return new Dialogs.ProgressDialog();
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
            return InputValueDialog.Show(title, desc,val);
        }
    }
    
    
}
