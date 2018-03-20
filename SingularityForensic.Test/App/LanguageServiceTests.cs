using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.App;
using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.App {
    [TestClass()]
    public class LanguageServiceTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _languageService = Contracts.App.LanguageService.Current;
            Assert.IsNotNull(_languageService);
            _languageService.Initialize();
            Assert.IsNotNull(_languageService.CurrentProvider);
        }

        private ILanguageService _languageService;
        [TestMethod()]
        public void FindResourceStringTest() {
            var lan = _languageService.FindResourceString("BrandName");
            Assert.AreEqual(lan, "流火");
        }
    }
}