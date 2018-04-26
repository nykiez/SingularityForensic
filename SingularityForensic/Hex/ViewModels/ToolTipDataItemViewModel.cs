using Prism.Mvvm;

namespace SingularityForensic.Hex.ViewModels {
    public class ToolTipDataItemViewModel :BindableBase {
        private string _keyName;
        public string KeyName {
            get => _keyName;
            set => SetProperty(ref _keyName, value);
        }
        
        private string _value ;
        public string Value {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
