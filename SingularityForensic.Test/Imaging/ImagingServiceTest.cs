﻿using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Imaging;
using SingularityForensic.Test.App;
using SingularityForensic.Test.Casing;

namespace SingularityForensic.Test.Imaging {
    /// <summary>
    /// 测试镜像服务;
    /// </summary>
    [TestClass]
    public class ImagingServiceTest {
        //private const string OpenFileName = "G://MobileImgs/Honor/mmcblk0";
        private const string OpenFileName = "I://test.E01";

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            AppMockers.OpenFileName = OpenFileName;
            _imgService = ImgService.Current;
            _fsService = FileSystemService.Current;
            _csService = CaseService.Current;

            Assert.IsNotNull(_imgService);
            Assert.IsNotNull(_fsService);
            Assert.IsNotNull(_csService);
            _imgService.Initialize();
        }

        private IImagingService _imgService;
        private IFileSystemService _fsService;
        private ICaseService _csService;

        [TestMethod]
        public void TestAddImg() {
            _imgService.AddImg();
            
            Assert.AreEqual(_csService.CurrentCase.CaseEvidences.Count(), 1);

            Assert.AreEqual(_fsService.MountedEntities.Count(), 1);

            Assert.AreEqual(_imgService.MounterEntities.Count(), 1);

            var firstEvi = _imgService.MounterEntities.First().csEvidence;

            Assert.IsTrue(firstEvi.EvidenceTypeGuids.Contains(Contracts.Imaging.Constants.EvidenceType_Img));
        }

        //测试从案件中加载镜像;
        [TestMethod]
        public void TestLoadImgFromCase() {
            _csService.LoadCase($"{CaseMockers.CaseFolder}/{CaseMockers.CaseName}/{CaseMockers.CaseName}{SingularityForensic.Casing.Constants.CaseFileExtention}");
            Assert.IsNotNull(_csService.CurrentCase);

            Assert.AreNotEqual(_imgService.MounterEntities.Count(),0);

            var imgPath = _imgService.MounterEntities.First().mounter.ImgPath;
            
            Assert.AreEqual(Path.GetFullPath(imgPath), Path.GetFullPath(OpenFileName));

            var stream = _imgService.MounterEntities.First().mounter.RawStream;
        }
    }
}
