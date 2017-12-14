using CDFC.Parse.Contracts;
using Singularity.UI.MessageBoxes.ViewModels;
using Singularity.UI.MessageBoxes.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Singularity.UI.MessageBoxes.MessageBoxes {
    /// <summary>
    /// 列出的簇对话框;
    /// </summary>
    public class ListBlockMessageBox
    {
        public event EventHandler<long> SelectedAddressChanged;                //选定了某个块地址的事件;
        
        private ListBlockWindowViewModel vm;
        private ListBlocksWindow window;
        public bool windowClosed { get; set; }                            //窗体是否已经关闭;

        public IEnumerable<BlockGroup> Groups { get; private set; }                //描述的快组;

        public ListBlockMessageBox(IEnumerable<BlockGroup> groups,IFile file)
        {
            if(groups == null) {
                throw new ArgumentNullException(nameof(groups));
            }
            

            this.Groups = groups;
            
            vm = new ListBlockWindowViewModel();
            vm.SelectedAdrressChanged += (sender, e) => {
                SelectedAddressChanged?.Invoke(this, e);
            };

            File = file;

            window = new ListBlocksWindow();
            window.Title = file.Name;
            window.Closed += (sender, e) => {
                windowClosed = true;
            };


            window.DataContext = vm;
        }

        public IFile File { get; }
        public void Show() {
            try {
                window.Show();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (sender, e) => {
                    try {
                        foreach (var p in Groups) {
                            if (!windowClosed) {
                                Application.Current.Dispatcher.Invoke(() => {
                                    vm.AddGroup(p);
                                });
                            }
                        }
                    }
                    catch {

                    }
                };

                worker.RunWorkerCompleted += (sender, e) => {
                    vm.CalcGroups();
                };
                worker.RunWorkerAsync();
            }
            catch {

            }
        }
        public void Close() {
            if (window != null && !windowClosed) {
                window.Close();
                windowClosed = true;
            }
        }
    }
}
