using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using System.IO;
using System.Linq;

namespace SingularityForensic.Test.Casing {
    [TestClass]
    public class CaseServiceTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            RegisterEvents();
            _csService = ServiceProvider.Current.GetInstance<ICaseService>();
            Assert.IsNotNull(_csService);
        }

        private bool _caseLoading = false;
        private bool _caseLoaded = false;
        private void RegisterEvents() {
            CommonEventHelper.GetEvent<CaseLoadingEvent>().Subscribe(cs => {
                _caseLoading = true;
            });
            CommonEventHelper.GetEvent<CaseLoadedEvent>().Subscribe(() => {
                _caseLoaded = true;
            });
        }

        private ICaseService _csService;
        
        public void CreateAndLoadCase() {
            _csService.ConfirmCaseLoaded();
        }

        public void CloseCase() {
            _csService.CloseCurrentCase();
        }

        public void AddNewCaseEvidence() {
            _csEvidence = CaseService.Current.CreateNewCaseEvidence(
                new string[] {
                    TestEvidenceTypeGuid
                },
                TestEvidenceName,
                TestInterLabel
            );

            AssertEvidenceMatched();

            _csService.CurrentCase.AddNewCaseEvidence(_csEvidence);
        }

        public void LoadCaseEvidence() {
            _csService.CurrentCase.LoadCaseEvidence(_csEvidence);
        }

        public void LoadCase() {
            _csService.LoadCase($"{CaseMockers.CaseFolder}/{CaseMockers.CaseName}/{CaseMockers.CaseName}{SingularityForensic.Casing.Constants.CaseFileExtention}");
        }

        [TestMethod]
        public void TestCreateCase() {
            //测试案件加载中/已加载的事件是否正常触发;
            _caseLoading = false;
            _caseLoaded = false;

            CreateAndLoadCase();

            Assert.IsTrue(_caseLoading);
            Assert.IsTrue(_caseLoaded);

            Assert.IsNotNull(_csService.CurrentCase);
            Assert.IsTrue(Directory.Exists($"{_csService.CurrentCase.Path}"));
            Assert.AreEqual(_csService.CurrentCase.Path, $"{CaseMockers.CaseFolder}/{CaseMockers.CaseName}");
        }

        [TestMethod]
        public void TestCloseCase() {
            CreateAndLoadCase();
            //测试案件关闭中/已关闭事件是否被正常触发;
            bool closing = false;
            bool closed = false;

            //阻止案件关闭;
            var closingStoken = CommonEventHelper.GetEvent<CaseUnloadingEvent>().Subscribe(e => {
                closing = true;
                e.Cancel = true;
            });
            CommonEventHelper.GetEvent<CaseUnloadedEvent>().Subscribe(cs => closed = true);

            CloseCase();

            Assert.IsTrue(closing);
            Assert.IsFalse(closed);
            Assert.IsNotNull(_csService.CurrentCase);

            closing = false;

            //取消阻止案件事件;
            CommonEventHelper.GetEvent<CaseUnloadingEvent>().Unsubscribe(closingStoken);
            //或者如下语句,均能取消订阅;
            //closingStoken.Dispose();

            closingStoken = null;

            //并订阅新的卸载案件中事件;
            CommonEventHelper.GetEvent<CaseUnloadingEvent>().Subscribe(e => {
                closing = true;
            });

            _csService.CloseCurrentCase();

            Assert.IsTrue(closing);
            Assert.IsTrue(closed);

            Assert.IsNull(_csService.CurrentCase);
            
        }

        [TestMethod]
        public void TestLoadCase() {
            //测试案件加载中/已加载的事件是否正常触发;
            _caseLoading = false;
            _caseLoaded = false;

            LoadCase();
            
            Assert.IsTrue(_caseLoading);
            Assert.IsTrue(_caseLoaded);

            Assert.IsNotNull(_csService.CurrentCase);

            Assert.AreEqual(_csService.CurrentCase.CaseName, CaseMockers.CaseName);
        }


        //加载案件文件部分的测试;
        const string TestEvidenceTypeGuid = nameof(TestEvidenceTypeGuid);
        const string TestInterLabel = nameof(TestInterLabel);
        const string TestEvidenceName = nameof(TestEvidenceName);
        private ICaseEvidence _csEvidence;
        //断言案件文件是否准确;
        private void AssertEvidenceMatched() {
            Assert.IsFalse(string.IsNullOrEmpty(_csEvidence.EvidenceGUID));
            Assert.IsTrue(_csEvidence.EvidenceTypeGuids.Contains(TestEvidenceTypeGuid));
            Assert.AreEqual(_csEvidence.Name, TestEvidenceName);
            Assert.AreEqual(_csEvidence.InterLabel, TestInterLabel);
        }

        [TestMethod]
        public void TestAddCaseEvidence() {
            CreateAndLoadCase();
            
            //测试添加案件文件事件是否能够被正常触发;
            bool addedCaptured = false;
            CommonEventHelper.GetEvent<CaseEvidenceAddedEvent>().Subscribe(csEvidence => {
                addedCaptured = true;
            });

            AddNewCaseEvidence();
            
            Assert.AreEqual(_csService.CurrentCase.CaseEvidences.Count(),0);
            Assert.AreEqual(addedCaptured, true);

            Assert.IsTrue(Directory.Exists($"{_csService.CurrentCase.Path}/{_csEvidence.EvidenceGUID}"));
        }

        [TestMethod]
        public void TestLoadCaseEvidence() {
            AddNewCaseEvidence();

            //测试案件文件加载中/已加载是否能够被正常触发;
            var evidenceLoading = false;
            var evidenceLoaded = false;
            CommonEventHelper.GetEvent<CaseEvidenceLoadingEvent>().Subscribe(tuple => {
                evidenceLoading = true;
                Assert.AreEqual(tuple.csEvidence, _csEvidence);
            });

            CommonEventHelper.GetEvent<CaseEvidenceLoadedEvent>().Subscribe(csEvidence => {
                evidenceLoaded = true;
                Assert.AreEqual(csEvidence, _csEvidence);
            });

            LoadCaseEvidence();

            Assert.IsTrue(evidenceLoading);
            Assert.IsTrue(evidenceLoaded);

            Assert.AreEqual(_csService.CurrentCase.CaseEvidences.Count(), 1);
            Assert.AreEqual(_csService.CurrentCase.CaseEvidences.First(), _csEvidence);
            
        }
        
        //测试存在案件文件的案件加载是否正常工作;
        [TestMethod]
        public void TestLoadCaseWithEvidences() {
            LoadCase();
            _csEvidence = _csService.CurrentCase.CaseEvidences.FirstOrDefault();
            Assert.IsNotNull(_csEvidence);
            AssertEvidenceMatched();
        }
    }

    
}
