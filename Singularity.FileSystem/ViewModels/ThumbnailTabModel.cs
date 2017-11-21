using CDFCUIContracts.Abstracts;
using CDFCUIContracts.Events;
using Prism.Mvvm;
using Singularity.UI.FileSystem.Models;
using System;
using System.Collections.ObjectModel;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.FileSystem.ViewModels {
    public class ThumbnailViewModel : BindableBase, ITabModel {
        public string Header => FindResourceString("ThumbnailTab");

        public Func<ObservableCollection<FileRow>> GetRowsFunc { get; set; }

        public event EventHandler<TEventArgs<ObservableCollection<FileRow>>> SetRows;
        
        public ObservableCollection<FileRow> FileRows {
            get {
                if(GetRowsFunc != null) {
                    return GetRowsFunc();
                }
                return null;
            }
            set {
                SetRows?.Invoke(this,new TEventArgs<ObservableCollection<FileRow>>(value));
            }
        }
        
        public event EventHandler<TEventArgs<FileRow>> EnterRowed;
        public void EnterRow(FileRow row) {
            if(row != null) {
                EnterRowed?.Invoke(this,new TEventArgs<FileRow>( row ));
            }
        }

        public event EventHandler<TEventArgs<FileRow>> SelectedRowChanged;
        private FileRow _selectedRow;
        public FileRow SelectedRow {
            get {
                return _selectedRow;
            }
            set {
                SetProperty(ref _selectedRow, value);
                SelectedRowChanged?.Invoke(this,new TEventArgs<FileRow>(_selectedRow));
            }
        }


    }
}
