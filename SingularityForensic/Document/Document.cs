using Prism.Mvvm;
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
    class Document : ExtensibleBindableBase , IDocument {
        public string Title { get ; set ; }

        private ObservableCollection<CommandItem> _commandItem = new ObservableCollection<CommandItem>();
        public IList<CommandItem> CustomCommands => _commandItem;
        
        private object _uiObject;
        public virtual object UIObject {
            get => _uiObject;
            set => SetProperty(ref _uiObject, value);
        }
        
        public object Tag { get ; set ; }
        
        public virtual void Dispose() {
            
        }
    }
}
