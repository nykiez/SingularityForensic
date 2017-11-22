using CDFCUIContracts.Commands;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Regions;
using Singularity.Interfaces;
using SingularityForensic.Modules.MainPage.Global.Services;
using System;
using System.Collections.ObjectModel;
using Xceed.Wpf.AvalonDock.Layout;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Modules.MainPage.Models {
    //Tab类型;
    public abstract class TabModel : LayoutDocument, IDisposable {
        public TabModel() {
            CommandItems.Add(CloseCmItem);
            CommandItems.Add(CloseAllCmItem);
        }

        //To do:define the UI for tabcontrol's content;
        //private FrameworkElement _view;
        //public FrameworkElement Content {
        //    get => _view;
        //    set => SetProperty(ref _view, value);
        //}

        //The command will be executed while Close Button clicked ;
        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(() => {
                if (ConfirmToClose()) {
                    var fsTabService = ServiceLocator.Current.GetInstance<IDocumentTabService>();
                    fsTabService?.CloseTab(this);
                    this.Dispose();
                }
            }));

        private CommandItem _closeCmItem;
        public CommandItem CloseCmItem => _closeCmItem ??
            (_closeCmItem = new CommandItem {
                Command = CloseCommand,
                CommandName = FindResourceString("CloseTab")
            });

        //It can be overwrite in inherited class to ask for confirming to closing the tab;
        protected virtual bool ConfirmToClose() => true;

        public virtual void Dispose() {

        }

        private DelegateCommand _closeAllCommand;
        public DelegateCommand CloseAllCommand => _closeAllCommand ??
            (_closeAllCommand = new DelegateCommand(() => {
                var fsTabService = ServiceLocator.Current.GetInstance<IDocumentTabService>();
                fsTabService?.CloseAllTab();
            }));

        private CommandItem _closeAllCmItem;
        public CommandItem CloseAllCmItem => _closeAllCmItem ?? (
            _closeAllCmItem = new CommandItem {
                Command = CloseAllCommand,
                CommandName = FindResourceString("CloseAllTabs")
            }
        );

        public ObservableCollection<CommandItem> CommandItems { get; set; } = new ObservableCollection<CommandItem>();
    }

    /// <summary>
    /// /// 可拓展Tab类型;通过字符串作为唯一类型标识;
    /// </summary>
    public class PinTabModel : TabModel, IHavePinKind {
        public PinTabModel(string pinKind) {
            this.ContentId = pinKind;
            //System.Collections.IEnumerable
        }
    }

    public class ExtDataTabModel {

    }

    /// <summary>
    /// 可拓展Tab类型;具有上下文;
    /// </summary>
    /// <typeparam name="TData">相关数据类型Context</typeparam>
    public class ExtTabModel<TData> : PinTabModel, IHaveData<TData> {
        public ExtTabModel(TData data, string pinKind) : base(pinKind) {
            this.Data = data;
        }

        public TData Data { get; }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => navigationContext.Parameters["pinKind"] as string == ContentId;

    }
}
