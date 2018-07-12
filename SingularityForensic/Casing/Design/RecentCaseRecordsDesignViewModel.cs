using Prism.Mvvm;
using SingularityForensic.Casing.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Casing.Design {
    class RecentCaseRecordsDesignViewModel:BindableBase {
        public RecentCaseRecordsDesignViewModel() {
            void AddGroup(RecentCaseRecordGroupModel groupModel,RecentCaseRecord rRecord) {
                var rModel = new RecentCaseRecordModel(rRecord);
                groupModel.Items.Add(rModel);
            }

            var todayGroup = new RecentCaseRecordGroupModel { Header = "今天" };
            var tItem1 = new RecentCaseRecord {
                CaseName = "Van VS 魔男",
                CasePath = "新日暮里2333",
                CaseTime = "2099/31/13",
                LastAccessTime = new DateTime(2019, 12, 13)
            };
            
            AddGroup(todayGroup, tItem1);
            AddGroup(todayGroup, tItem1);
            RecordGroups.Add(todayGroup);
        }
        public ObservableCollection<RecentCaseRecordGroupModel> RecordGroups { get; } = new ObservableCollection<RecentCaseRecordGroupModel>();
    }
}
