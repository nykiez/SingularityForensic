using CDFC.Util.IO;
using Prism.Commands;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.NTFS.USN;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.ViewModels {
    public class UsnJrnlPreviewerViewModel:BindableBase,IDisposable {
        public UsnJrnlPreviewerViewModel(Stream stream) {
            this.Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }
        public Stream Stream { get; }
        private bool _loaded = false;
        public void LoadRecords() {
            if (_loaded) {
                return;
            }

            _loaded = true;
            IsBusy = true;
            ThreadInvoker.BackInvoke(() => {
                //通知进度;
                var opStream = new OperatebleStream(Stream);

                //释放中断处理;
                _dispoingActions.Add(opStream.Break);

                var percentage = 0;
                //通知进度;
                opStream.PositionChanged += (sender, e) => {
                    if(percentage == (int)(e * 100 / opStream.Length)) {
                        return;
                    }
                    percentage = (int)(e * 100 / opStream.Length);
                    BusyWord = $"{percentage}%";
                };

                try {
                    Records = UsnRecordV2.ReadRecordsFromStream(opStream).ToList();
                    IsBusy = false;
                }
                catch(Exception ex) {
                    LoggerService.WriteException(ex);
                    BusyWord = ex.Message;
                }
                

                //取消释放中断处理;
                _dispoingActions.Remove(opStream.Break);
            });
            
        }

        /// <summary>
        /// 释放的动作集合;
        /// </summary>
        private List<Action> _dispoingActions = new List<Action>();
        private bool _disposed;
        public void Dispose() {
            if (_disposed) {
                return;
            }

            _dispoingActions.ForEach(p => p.Invoke());
            Stream.Dispose();
            _disposed = true;
        }
        

        private bool _isBusy;
        public bool IsBusy {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        
        private string _busyWord;
        public string BusyWord {
            get => _busyWord;
            set => SetProperty(ref _busyWord, value);
        }

        private IEnumerable<UsnRecordV2> _records;
        public IEnumerable<UsnRecordV2> Records {
            get => _records;
            set => SetProperty(ref _records, value);
        }

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand => _loadedCommand ??
            (_loadedCommand = new DelegateCommand(
                () => {
                    LoadRecords();
                }
            ));

    }
}
