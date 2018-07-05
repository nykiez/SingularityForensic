using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Hash.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash.ViewModels
{
    public class CreateHashSetDialogViewModel:BindableBase
    {
        public InteractionRequest<INotification> CloseRequest { get; } = new InteractionRequest<INotification>();
        private string _hashSetName;
        public string HashSetName {
            get => _hashSetName;
            set {
                SetProperty(ref _hashSetName, value);
                if (_hashSetStorageModified) {
                    return;
                }
                if (string.IsNullOrEmpty(value)) {
                    return;
                }
                HashSetStoragePath = $"{AppService.AppResourceFolder}\\{Constants.HashDefaultStorageFolder}\\{value}";
            }
        }

        private bool _hashSetIsEnabled = true;
        public bool HashSetIsEnabled {
            get => _hashSetIsEnabled;
            set => SetProperty(ref _hashSetIsEnabled, value);
        }
        
        private string _hashSetDescription;
        public string HashSetDescription {
            get => _hashSetDescription;
            set => SetProperty(ref _hashSetDescription, value);
        }

        //存储路径是否被自定义过;
        private bool _hashSetStorageModified;
        private string _hashSetStoragePath;
        public string HashSetStoragePath {
            get => _hashSetStoragePath;
            set {
                _hashSetStorageModified = true;
                SetProperty(ref _hashSetStoragePath, value);
            }
        }


        private HasherModel _selectedHasherModel;
        public HasherModel SelectedHasherModel {
            get => _selectedHasherModel;
            set => SetProperty(ref _selectedHasherModel, value);
        }

        public ObservableCollection<HasherModel> HasherModels { get; } = new ObservableCollection<HasherModel>();

        private DelegateCommand _findPathCommand;
        public DelegateCommand FindPathCommand => _findPathCommand ??
            (_findPathCommand = new DelegateCommand(
                () => {
                    var direct = DialogService.Current.OpenDirect();
                    if (string.IsNullOrEmpty(direct)) {
                        return;
                    }

                    HashSetStoragePath = direct;
                }
            ));

        public IHashSet HashSet { get; private set; }


        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??
            (_confirmCommand = new DelegateCommand(
                () => {
                    if (!ValidateInput()) {
                        return;
                    }

                    try {
                        HashSet = HashSetFactory.CreateNew(HashSetStoragePath, Guid.NewGuid().ToString("P"), SelectedHasherModel.Hasher);

                        HashSet.Description = HashSetDescription;
                        HashSet.Name = HashSetName;
                        HashSet.IsEnabled = HashSetIsEnabled;
                        
                        CloseRequest.Raise(new Notification());
                    }
                    catch(Exception ex) {
                        LoggerService.WriteException(ex);
                        MsgBoxService.Show(ex.Message);
                    }
                    
                }
            ));

        private bool ValidateInput() {
            if (string.IsNullOrEmpty(HashSetName)) {
                MsgBoxService.Show($"{LanguageService.FindResourceString(Constants.MsgText_HashSetNameCannotBeEmpty)}");
                return false;
            }

            //检查路径是否合法;
            if (HashSetStoragePath.Any(c => Path.GetInvalidPathChars().Contains(c))) {
                MsgBoxService.Show(LanguageService.FindResourceString(Constants.MsgText_InvalidPathChar));
                return false;
            }

            try {
                //检查路径是否已经指向了现存的哈希集存储路径;
                var existingHashSet = HashSetManagementService.HashSets.
                    FirstOrDefault(p => Path.GetFullPath(p.StoragePath) == Path.GetFullPath(HashSetStoragePath));
                if (existingHashSet != null) {
                    MsgBoxService.Show(
                        $"{LanguageService.FindResourceString(Constants.MsgText_StoragePathAlreadyOccupied)}:{HashSet.Name}");
                    return false;
                }
            }
            catch(Exception ex) {
                MsgBoxService.Show(ex.Message);
                return false;
            }
            
            //如若不存在路径,尝试创建路径;
            if (!Directory.Exists(HashSetStoragePath)) {
                try {
                    Directory.CreateDirectory(HashSetStoragePath);
                }
                catch(Exception ex) {
                    LoggerService.WriteException(ex);
                    MsgBoxService.Show(
                        LanguageService.FindResourceString(Constants.MsgText_HashSetStoragePathCannotBeCreated)+
                        $":{ex.Message}"
                    );
                    return false;
                }
            }

            return true;
        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ??
            (_cancelCommand = new DelegateCommand(
                () => {
                    HashSet?.Dispose();
                    HashSet = null;
                    CloseRequest.Raise(new Notification());
                }
            ));

        public void Initialize() {
            HasherModels.Clear();
            var hashers = GenericServiceStaticInstances<IHasher>.Currents.OrderBy(p => p.Sort);
            foreach (var hasher in hashers) {
                HasherModels.Add(new HasherModel(hasher));
            }

            if(HasherModels.Count != 0) {
                SelectedHasherModel = HasherModels[0];
            }
        }
    }
}
