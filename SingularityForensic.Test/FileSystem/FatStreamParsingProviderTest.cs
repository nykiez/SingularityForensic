using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.FileSystem {
    [TestClass()]
    public class FatStreamParsingProviderTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();

            _fatStream = File.OpenRead(FAT32ImgPath);
            _streamParsingProvider = ServiceProvider.Current.GetAllInstances<IStreamParsingProvider>().
                FirstOrDefault(p => 
                    p.GUID == SingularityForensic.FileSystem.Constants.StreamParser_FAT
                );

            Assert.IsNotNull(_streamParsingProvider);
        }

        private const string FAT32ImgPath = "E://anli/fat32.img";
        private IStreamParsingProvider _streamParsingProvider;

        private Stream _fatStream;

        private const string UnknownImgPath = "E://anli/gpt.img";

        [TestMethod()]
        public void CheckIsValidStreamTest() {
            Assert.IsTrue(_streamParsingProvider.CheckIsValidStream(_fatStream));

            using(var unKnownStream = File.OpenRead(UnknownImgPath)) {
                Assert.IsFalse(_streamParsingProvider.CheckIsValidStream(unKnownStream));
            }
        }
        

        [TestMethod()]
        public void ParseStreamTest() {
            Assert.Fail();
        }

        [TestCleanup]
        public void Clean() {
            _fatStream.Dispose();
        }
    }
}