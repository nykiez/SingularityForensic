using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Casing.Models {
    public class RecentCaseRecordGroupModel : BindableBase{
        public ObservableCollection<RecentCaseRecordModel> Items { get; } = new ObservableCollection<RecentCaseRecordModel>();
        private string _header;
        public string Header {
            get => _header;
            set => SetProperty(ref _header, value);
        }
    }
}
