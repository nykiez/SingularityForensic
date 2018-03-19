using CDFCUIContracts.Abstracts;
using CDFCUIContracts.Events;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using System;
using System.Collections.ObjectModel;

namespace SingularityForensic.FileExplorer.ViewModels {
    public class ThumbnailViewModel : BindableBase, ITabModel {
        public string Header => ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("ThumbnailTab");

        public Func<ObservableCollection<IFileRow>> GetRowsFunc { get; set; }

        public event EventHandler<TEventArgs<ObservableCollection<IFileRow>>> SetRows;
        
        public ObservableCollection<IFileRow> FileRows {
            get {
                if(GetRowsFunc != null) {
                    return GetRowsFunc();
                }
                return null;
            }
            set {
                SetRows?.Invoke(this,new TEventArgs<ObservableCollection<IFileRow>>(value));
            }
        }
        
        public event EventHandler<TEventArgs<IFileRow>> EnterRowed;
        public void EnterRow(IFileRow row) {
            if(row != null) {
                EnterRowed?.Invoke(this,new TEventArgs<IFileRow>( row ));
            }
        }

        public event EventHandler<TEventArgs<IFileRow>> SelectedRowChanged;
        private IFileRow _selectedRow;
        public IFileRow SelectedRow {
            get {
                return _selectedRow;
            }
            set {
                SetProperty(ref _selectedRow, value);
                SelectedRowChanged?.Invoke(this,new TEventArgs<IFileRow>(_selectedRow));
            }
        }


    }
}
