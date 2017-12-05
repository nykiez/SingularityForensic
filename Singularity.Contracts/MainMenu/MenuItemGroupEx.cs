using System.ComponentModel;

namespace Singularity.Contracts.Contracts.MainMenu {
    public class MenuItemGroupEx:MenuItemGroup,INotifyPropertyChanged {
        public MenuItemGroupEx(string guid,int sortOrder = 32):base(guid,sortOrder) {

        }

        private string _customRegionName;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CustomRegionName {
            get {
                return _customRegionName;
            }
            set {
                _customRegionName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomRegionName)));
            }
        }
    }
}
