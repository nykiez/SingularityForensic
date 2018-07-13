using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.Casing;

namespace SingularityForensic.Test.Casing {
    [TestClass]
    public class RecentCaseRecordManagementServiceTest {

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _recentCaseRecordManagementService = RecentCaseRecordManagementService.Current;
            
        }

        private IRecentCaseRecordManagementService _recentCaseRecordManagementService;
        [TestMethod]
        public void TestAddAndRemove() {
            _recentCaseRecordManagementService.ClearRecords();
            var records = _recentCaseRecordManagementService.GetAllRecentRecords();
            Assert.IsNotNull(records);
            Assert.AreEqual(records.Count(), 0);

            var cs = CaseService.Current.CreateNewCase();

            _recentCaseRecordManagementService.AddCase(cs);
            records = _recentCaseRecordManagementService.GetAllRecentRecords();

            Assert.IsNotNull(records);
            Assert.AreEqual(records.Count(), 1);

            var record = records.First();
            Assert.AreEqual(record.CaseGUID, cs.GUID);
            Assert.AreEqual(record.CaseName, cs.CaseName);
            Assert.AreEqual(record.CasePath, cs.Path);
            Assert.AreEqual(record.CaseTime, cs.CaseTime);

            //测试添加重复案件;
            _recentCaseRecordManagementService.AddCase(cs);
            records = _recentCaseRecordManagementService.GetAllRecentRecords();
            Assert.AreEqual(records.Count(), 1);

            //测试移除;
            _recentCaseRecordManagementService.RemoveRecord(record);
            records = _recentCaseRecordManagementService.GetAllRecentRecords();
            Assert.IsNotNull(records);
            Assert.AreEqual(records.Count(), 0);


            //测试清除;
            _recentCaseRecordManagementService.AddCase(cs);
            records = _recentCaseRecordManagementService.GetAllRecentRecords();
            Assert.IsNotNull(records);
            Assert.AreEqual(records.Count(), 1);

            _recentCaseRecordManagementService.ClearRecords();
            records = _recentCaseRecordManagementService.GetAllRecentRecords();
            Assert.IsNotNull(records);
            Assert.AreEqual(records.Count(), 0);
        }

        [TestMethod]
        public void TestLoadCase() {
            _recentCaseRecordManagementService.ClearRecords();
            var records = _recentCaseRecordManagementService.GetAllRecentRecords();
            Assert.IsNotNull(records);
            Assert.AreEqual(records.Count(), 0);

            var cs = CaseService.Current.CreateNewCase();
            //加载案件后将会添加加载的案件到最近打开的案件列表中;(通过某个事件处理器);
            CaseService.Current.LoadCase(cs);

            records = _recentCaseRecordManagementService.GetAllRecentRecords();
            Assert.IsNotNull(records);
            Assert.AreEqual(records.Count(), 1);

            var record = records.First();
            Assert.AreEqual(record.CaseGUID, cs.GUID);
            Assert.AreEqual(record.CaseName, cs.CaseName);
            Assert.AreEqual(record.CasePath, cs.Path);
            Assert.AreEqual(record.CaseTime, cs.CaseTime);

            Assert.AreEqual(record.LastAccessTime.Date, DateTime.Now.Date);

            //测试时间更新;
            Thread.Sleep(1000);
            _recentCaseRecordManagementService.AddCase(cs);
            records = _recentCaseRecordManagementService.GetAllRecentRecords();
            Assert.IsNotNull(records);
            Assert.AreEqual(records.Count(), 1);
            var record2 = records.First();
            Assert.IsTrue(record2.LastAccessTime > record.LastAccessTime);
        }
    }
}
