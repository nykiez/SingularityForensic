using System.ComponentModel.Composition;
using Prism.Mvvm;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;
using SingularityForensic.Helpers;
using System.Windows.Input;
using Prism.Interactivity.InteractionRequest;

namespace SingularityForensic.Shell.ViewModels {
    //主模型;
    [Export]
    public partial class ShellViewModel:BindableBase {
        public ShellViewModel() {
            
        }
        
        private string BrandName {
            get {
                return  LanguageService.FindResourceString(Contracts.App.Constants.WindowTile_AppName);
            }
        }
        
        private string _title;
        public string Title {
            get => _title ?? (_title = BrandName);
            set {
                SetProperty(ref _title, value);
            }
        }

        public void SetTitle(string word,bool saveBrandName = true) {
            if (saveBrandName && word != null) {
                Title = $"{word} - {BrandName}";
            }
            else if(word == null) {
                Title = BrandName;
            }
            else {
                Title = word;
            }
            
        }

        private string _loadingWord;
        public string LoadingWord {
            get {
                return _loadingWord;
            }
            set {
                SetProperty(ref _loadingWord, value);
            }
        }

        public InteractionRequest<Notification> FocusRequest { get; } = new InteractionRequest<Notification>();

        public void Focus() {
            FocusRequest.Raise(new Notification());
        }

    }
    
    
    public partial class ShellViewModel {
        private bool isLoading;
        public bool IsLoading {
            get {
                return isLoading;
            }
            set {
                SetProperty(ref isLoading, value);
            }
        }
    }
}
