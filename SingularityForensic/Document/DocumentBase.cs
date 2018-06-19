using Prism.Commands;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SingularityForensic.Document {
    public abstract class DocumentBase : ExtensibleBindableBase, IDocumentBase {
        private string _title;
        public string Title {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private ObservableCollection<ICommandItem> _commandItem = new ObservableCollection<ICommandItem>();
        public IList<ICommandItem> CustomCommands => _commandItem;

        public abstract object UIObject { get; }

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
