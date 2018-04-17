using SingularityForensic.Contracts.TreeView;
using System.Collections.ObjectModel;

namespace SingularityForensic.Drive.Design {
    public class DriveItemsWindowDesignViewModel {
        public DriveItemsWindowDesignViewModel() {
            //DriveUnits.Add(
            //    //new TreeUnit(null, null) {
            //    //    Label = "HDD1"
            //    //}
            //);
        }
        public ObservableCollection<ITreeUnit> DriveUnits { get; set; } = new ObservableCollection<ITreeUnit>();
    }
}
