using Prism.Mvvm;
using SingularityForensic.Casing.Models;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Casing.ViewModels {
    [Export]
    public class RecentCaseRecordsViewModel: BindableBase {
        [ImportingConstructor]
        public RecentCaseRecordsViewModel() {
            InitializeGroups();
        }


        /// <summary>
        /// 添加RecordModel到group中;
        /// </summary>
        /// <param name="recordModel"></param>
        /// <param name="group"></param>
        private void AddRecordModelToGroup(IRecentCaseRecord record, RecentCaseRecordGroupModel group) {
            RecentCaseRecordModel recordModel = new RecentCaseRecordModel(record);
            group.Items.Add(recordModel);
            //订阅打开事件;
            recordModel.OpenCaseRequired += (sender, e) => {
                try {
                    //如若案件文件不存在,询问是否移除最近案件引用;
                    var csFilePath = $"{recordModel.Record.CasePath}\\{recordModel.Record.CaseName}{Constants.CaseFileExtention}";
                    if (!File.Exists(csFilePath)) {
                        if (MsgBoxService.Show(
                           LanguageService.TryGetStringWithFormat(Constants.MsgText_ConfirmToRemoveRecentCaseFormat, csFilePath),
                           MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                            RecentCaseRecordManagementService.RemoveRecord(recordModel.Record);
                            InitializeGroups();
                            return;
                        }
                    }

                    CaseService.Current.LoadCase(csFilePath);
                    RegionHelper.RequestNavigate(Contracts.MainPage.Constants.MainPageDocumentRegion, Contracts.MainPage.Constants.WelcomeView);
                }
                catch (Exception ex) {
                    LoggerService.WriteException(ex);
                }
            };
        }


        public ObservableCollection<RecentCaseRecordGroupModel> RecordGroups { get; } = new ObservableCollection<RecentCaseRecordGroupModel>();


        private void InitializeGroups() {
            RecordGroups.Clear();
            var records = RecentCaseRecordManagementService.GetAllRecentRecords();

            
            //根据时间段添加group;
            void AddGroup(string groupName,DateTime latestTime,TimeSpan? lastedTime) {
                var thisGroupRecords = records.Where(p => p.LastAccessTime <= latestTime &&
                (lastedTime == null || p.LastAccessTime > latestTime - lastedTime.Value)).ToArray(); 
                if(thisGroupRecords.Length == 0) {
                    return;
                }

                var group = new RecentCaseRecordGroupModel {
                    Header = groupName
                };

                foreach (var record in thisGroupRecords) {
                    AddRecordModelToGroup(record, group);
                }
                RecordGroups.Add(group);
            }

            AddGroup(
                LanguageService.FindResourceString(Constants.RecentCaseRecordGroupName_Today), 
                DateTime.Now.Date.AddDays(1),TimeSpan.FromDays(1)
            );

            AddGroup(
                LanguageService.FindResourceString(Constants.RecentCaseRecordGroupName_Yesterday),
                DateTime.Now.Date,
                TimeSpan.FromDays(1));

            AddGroup(
                LanguageService.FindResourceString(Constants.RecentCaseRecordGroupName_Earlier),
                DateTime.Now.Date.AddDays(-1),
                null);
        }


    }
}
