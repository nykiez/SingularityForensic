using SingularityForensic.Contracts.TreeView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Drive.Design {
    public class DriveItemsWindowDesignViewModel {
        public DriveItemsWindowDesignViewModel() {
            DriveUnits.Add(
                new TreeUnit(null, null) {
                    Label = "HDD1"
                }
            );
        }
        public ObservableCollection<TreeUnit> DriveUnits { get; set; } = new ObservableCollection<TreeUnit>();
    }
}
