using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.ViewModels;
using SingularityForensic.FileExplorer.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace SingularityForensic.FileExplorer.MessageBoxes {
    /// <summary>
    /// 列出簇对话框;
    /// </summary>
    public class ListBlockMessageBox {
        public event EventHandler<long> SelectedAddressChanged;                //选定了某个块地址的事件;

        private ListBlockWindowViewModel _vm;
        private ListBlocksWindow _window;
        public bool windowClosed { get; set; }                            //窗体是否已经关闭;

        public IEnumerable<BlockGroup> Groups { get; private set; }                //描述的快组;

        public ListBlockMessageBox(IEnumerable<BlockGroup> groups) {
            if (groups == null) {
                throw new ArgumentNullException(nameof(groups));
            }
            
            this.Groups = groups;

            _vm = new ListBlockWindowViewModel();
            _vm.SelectedAdrressChanged += (sender, e) => {
                SelectedAddressChanged?.Invoke(this, e);
            };
            
            _window = new ListBlocksWindow();
            
            _window.Closed += (sender, e) => {
                windowClosed = true;
            };


            _window.DataContext = _vm;
        }

        public IFile File { get; }
        public void Show() {
            try {
                _window.Show();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (sender, e) => {
                    try {
                        foreach (var p in Groups) {
                            if (!windowClosed) {
                                Application.Current.Dispatcher.Invoke(() => {
                                    _vm.AddGroup(p);
                                });
                            }
                        }
                    }
                    catch {

                    }
                };

                worker.RunWorkerCompleted += (sender, e) => {
                    _vm.CalcGroups();
                };
                worker.RunWorkerAsync();
            }
            catch {

            }
        }
        public void Close() {
            if (_window != null && !windowClosed) {
                _window.Close();
                windowClosed = true;
            }
        }
    }
}
