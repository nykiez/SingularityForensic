using Prism.Commands;
using Prism.Mvvm;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Hash.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash.ViewModels
{
    class HashSetManagementDialogViewModel:BindableBase
    {
        public HashSetManagementDialogViewModel() {
            var hashSets = HashSetManagementService.HashSets;
            if(hashSets == null) {
                return;
            }

            foreach (var hashSet in hashSets) {
                HashSetModels.Add(new HashSetModel(hashSet));
            }
        }
        public ObservableCollection<HashSetModel> HashSetModels { get; } = new ObservableCollection<HashSetModel>();


        private HashSetModel _selectedHashSetModel;
        public HashSetModel SelectedHashSetModel {
            get => _selectedHashSetModel;
            set => SetProperty(ref _selectedHashSetModel, value);
        }


        private bool _isReadOnly;
        public bool IsReadOnly {
            get => _isReadOnly;
            set => SetProperty(ref _isReadOnly, value);
        }



        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??
            (_confirmCommand = new DelegateCommand(
                () => {

                }
            ));


        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ??
            (_cancelCommand = new DelegateCommand(
                () => {

                }
            ));

        
    }
}
