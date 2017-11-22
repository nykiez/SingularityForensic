using CDFCUIContracts.Abstracts;
using Singularity.UI.MessageBoxes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.MessageBoxes.ViewModels {
    public class BlockDeviceFSInfoViewModel:BindableBase {
        //标题;
        private string title;
        public string Title {
            get {
                return title;
            }
            set {
                SetProperty(ref title, value);
            }
        }

        //节点;
        public ObservableCollection<StorageTreeUnit> Units { get; set; } = new ObservableCollection<StorageTreeUnit>();
        
    }
}
