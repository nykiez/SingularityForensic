using System.ComponentModel;

namespace SingularityForensic.Modules.MainMenu.Models {
    public class MenuItemGroupEx:MenuItemGroup,INotifyPropertyChanged {
        public MenuItemGroupEx(int sortOrder = 32):base(sortOrder) {

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
