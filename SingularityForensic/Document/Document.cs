using Prism.Commands;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Document {
    /// <summary>
    /// 默认文档页;
    /// </summary>
    public class Document : ExtensibleBindableBase , IDocument {
        private string _title;
        public string Title {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private ObservableCollection<ICommandItem> _commandItem = new ObservableCollection<ICommandItem>();
        public IList<ICommandItem> CustomCommands => _commandItem;
        
        private object _uiObject;
        public virtual object UIObject {
            get => _uiObject;
            set => SetProperty(ref _uiObject, value);
        }
        
        public virtual void Dispose() {
            
        }


        //The command will be executed while Close Button clicked ;
        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(() => {
                CloseRequest?.Invoke(this, EventArgs.Empty);
            }));

        //关闭请求;
        public event EventHandler CloseRequest;
     

        public ObservableCollection<ICommandItem> CommandItems { get; set; } =
            new ObservableCollection<ICommandItem>();
        
    }
}
