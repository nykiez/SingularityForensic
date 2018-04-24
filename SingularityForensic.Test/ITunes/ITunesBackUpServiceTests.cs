using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.ITunes;
using SingularityForensic.Test.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.ITunes {
    [TestClass()]
    public class ITunesBackUpServiceTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _iTunesBackUpService = ServiceProvider.GetInstance<ITunesBackUpService>();
            _iTunesBackUpService.Initialize();
        }
        private ITunesBackUpService _iTunesBackUpService;

        [TestMethod()]
        public void InitializeTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddITunesBackUpDirTest() {
            AppMockers.OpenDirName =  "H://iosb";
            _iTunesBackUpService.AddITunesBackUpDir();
        }
    }
}