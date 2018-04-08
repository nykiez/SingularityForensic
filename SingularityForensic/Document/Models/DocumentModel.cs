using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using System;
using System.Collections.ObjectModel;
using Xceed.Wpf.AvalonDock.Layout;

namespace SingularityForensic.Document.Models {
    //Tab类型;
    public class DocumentModel : LayoutDocument {
        
        public DocumentModel(IDocument tab) {
            if(tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }
            this.Document = tab;

            this.Content = tab.UIObject;
            this.Title = tab.Title;
            
            //CommandItems.Add(CloseCmItem);
            CommandItems.Add(CloseAllCmItem);
        }
        
        public IDocument Document { get; }
        
        //The command will be executed while Close Button clicked ;
        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(() => {
                CloseRequest?.Invoke(this, EventArgs.Empty);
            }));

        //关闭请求;
        public event EventHandler CloseRequest;
        
        private DelegateCommand _closeAllCommand;
        public DelegateCommand CloseAllCommand => _closeAllCommand ??
            (_closeAllCommand = new DelegateCommand(() => {
                CloseAllRequest?.Invoke(this, EventArgs.Empty);

            }));

        //清除请求;
        public event EventHandler CloseAllRequest;

        private CommandItem _closeAllCmItem;
        public CommandItem CloseAllCmItem => _closeAllCmItem ?? (
            _closeAllCmItem = new CommandItem {
                Command = CloseAllCommand,
                CommandName = LanguageService.Current?.FindResourceString("CloseAllTabs")
            }
        );

        public ObservableCollection<CommandItem> CommandItems { get; set; } = new ObservableCollection<CommandItem>();
    }
}
