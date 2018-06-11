using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Shell;
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

        public IEnumerable<IBlockGroup> Groups { get; private set; }                //描述的快组;

        public ListBlockMessageBox(IBlockGroupedFile blockFile) {
            if (blockFile == null) {
                throw new ArgumentNullException(nameof(blockFile));
            }
            
            this.Groups = blockFile.BlockGroups;

            _vm = new ListBlockWindowViewModel();
            _vm.SelectedAdrressChanged += (sender, e) => {
                SelectedAddressChanged?.Invoke(this, e);
            };
            
            _window = new ListBlocksWindow();
            _window.Title = $"{blockFile.Name}-{LanguageService.FindResourceString(Constants.WindowTitle_ListBlock)}";
            _window.Closed += (sender, e) => {
                windowClosed = true;
            };


            _window.DataContext = _vm;
        }

        public IFile File { get; }
        public void Show() {
            try {
                if(ShellService.Current.Shell is Window owner && owner.IsLoaded) {
                    _window.Owner = owner;
                    _window.ShowInTaskbar = false;
                }
                    
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
