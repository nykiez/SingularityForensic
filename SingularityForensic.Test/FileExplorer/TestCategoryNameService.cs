using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.FileExplorer {
    [TestClass]
    public class TestCategoryNameService {

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _categoryNameService = NameCategoryService.Current;
            Assert.IsNotNull(_categoryNameService);
            _categoryNameService.Initialize();
        }

        private INameCategoryService _categoryNameService;
        [TestMethod]
        public void TestLoadKeyPairFrom() {
            NameCategoryService.LoadDescriptorsFromFile("D:\\SingularitySolution\\SingularityForensic\\FileExplorer\\File Type Categories.txt");
        }
        [TestMethod]
        public void TestExpired() {
            var pair = NameCategoryService.GetNameCategory("thumbcache_96.db");
            Assert.IsNotNull(pair);
            Assert.IsFalse(pair.IsExpired);
            NameCategoryService.LoadDescriptorsFromFile("D:\\SingularitySolution\\SingularityForensic\\FileExplorer\\File Type Categories.txt");
            Assert.IsTrue(pair.IsExpired);
        }

        [TestMethod]
        public void TestGetKeyPair() {
            var pair = NameCategoryService.GetNameCategory("thumbcache_96.db");
            Assert.IsNotNull(pair);
            Assert.IsFalse(pair.IsExpired);
            Assert.AreEqual(pair.CategoryName, "Windows 7缩略图");
        }
    }
}
