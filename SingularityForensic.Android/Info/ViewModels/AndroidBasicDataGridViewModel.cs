using Prism.Mvvm;
using SingularityForensic.Info.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SingularityForensic.Android.Info.ViewModels {
    public class AndroidDataGridViewModel<TDbModel>:BindableBase where TDbModel:ForensicInfoDbModel {
        public AndroidDataGridViewModel(IEnumerable<TDbModel> dbModels) {
            DbModels = new ObservableCollection<TDbModel>(dbModels);
        }
        public Type RowType { get; } = typeof(TDbModel);
        public ObservableCollection<TDbModel> DbModels { get; set; }

        public event EventHandler<TDbModel> SelectedModelChanged;
        private TDbModel _selectedDbModel;
        public TDbModel SelectedDbModel {
            get => _selectedDbModel;
            set {
                SetProperty(ref _selectedDbModel, value);
                SelectedModelChanged?.Invoke(this, _selectedDbModel);
            }
        }
    }
}
