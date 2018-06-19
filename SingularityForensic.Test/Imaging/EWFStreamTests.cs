using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Imaging;

namespace SingularityForensic.Test.Imaging {
    [TestClass()]
    public class EWFStreamTests {

        [TestInitialize]
        public void Initialize() {
            _stream = new EWFStream("I:\\test.e01", System.IO.FileAccess.Read);
        }
            
        EWFStream _stream;
        [TestMethod()]
        public void EWFStreamTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void FlushTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadTest() {
            var bts = new byte[1024];
            var pos = 512;
            var rReadCount = bts.Length - pos;
            var readCount = _stream.Read(bts, pos, rReadCount);
            Assert.AreEqual(rReadCount, readCount);
        }

        [TestMethod()]
        public void SeekTest() {
            var newPos = _stream.Length - 1;
            _stream.Position = newPos;
            Assert.AreEqual(_stream.Position, newPos);
            
        }

        [TestMethod()]
        public void SetLengthTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void CloseTest() {
            Assert.Fail();
        }

        [TestCleanup]
        public void Clean() {
            _stream.Dispose();
        }
    }
}