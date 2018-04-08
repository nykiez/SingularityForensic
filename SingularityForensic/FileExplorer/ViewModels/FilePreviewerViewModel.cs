using CDFCUIContracts.Abstracts;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.Previewers;
using System;
using System.IO;
using System.Windows;

namespace SingularityForensic.FileExplorer.ViewModels {
    public class FilePreviewerTabModel : BindableBase , ITabModel ,IDisposable {
        public string Header {
            get {
                return  ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("Preview");
            }
        }

        private IPreviewer previewer;
        public IPreviewer Previewer {
            get {
                return previewer;
            }
            set {
                SetProperty(ref previewer, value);
            }
        }

        /// <summary>
        /// 释放预览器;
        /// </summary>
        public void DisposePreviewer() {
            //释放预览器;
            Application.Current.Dispatcher.Invoke(() => {
                Previewer?.Dispose();
                Previewer = null;
            });
        }

        /// <summary>
        /// 通过文件名重置预览器;
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void LoadPreviewerByFileName(string fileName) {
            //检查是否存在文件;
            if (!File.Exists(fileName)) {
                throw new FileNotFoundException($"Could not find file:{nameof(fileName)}");
            }

            DisposePreviewer();

            //获得所有的预览器提供器;
            var preProviders = ServiceProvider.Current.GetAllInstances<IPreviewProvider>();
            if(preProviders == null) {
                return;
            }

            Application.Current.Dispatcher.Invoke(() => {
                try {
                    //foreach (var provider in preProviders) {
                    //    Previewer = provider.GetPreviewer(fileName);
                    //    if(Previewer != null) {
                    //        break;
                    //    }
                    //}
                }
                catch (Exception ex) {
                    LoggerService.Current?.WriteCallerLine(ex.Message);
                }

            });
        }

        /// <summary>
        /// 通过文件行加载预览器;
        /// </summary>
        /// <param name="row"></param>
        public void LoadPreviewerByFileRow(IFileRow row) {
            DisposePreviewer();

            //if(row is IFileRow<IFile> fileRow) {
            //    var fs = FileRowHelper.SaveFileToTemp(fileRow);

            //    if (fs != null) {
            //        var fileName = fs.Name;
            //        fs.Close();
            //        LoadPreviewerByFileName(fileName);
            //    }
            //}

            
        }

        /// <summary>
        /// 释放预览器所使用的流;
        /// </summary>
        public void Dispose() {
            DisposePreviewer();
        }
        
    }
}
