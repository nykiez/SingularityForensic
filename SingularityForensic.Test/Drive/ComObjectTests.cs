using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Test;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SingularityForensic.Drive.DeviceObjects.Tests {
    [TestClass()]
    public class ComObjectTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _comObject = ComObject.Current;
            Assert.IsNotNull(_comObject);
        }

        private ComObject _comObject;

        [TestMethod]
        public void PrintStacks() {
            Assert.IsNotNull(_comObject.LocalHdds);
            foreach (var hdd in _comObject.LocalHdds) {
                Trace.WriteLine(hdd.DevName);
                if(hdd.Volumes == null) {
                    continue;
                }

                foreach (var vol in hdd.Volumes) {
                    Trace.WriteLine($"\t{vol.Sign}");
                }
            }
            
        }

        [TestMethod]
        public void TestStream() {
            //测试是否能够正常读取;
            void TestReadLength(int testLength, Stream testStream,bool resetPos = true) {
                if (resetPos) {
                    testStream.Position = 0;
                }
                var buffer = new byte[testLength];
                var read = testStream.Read(buffer, 0, testLength);
                Assert.AreEqual(read, testLength);
                //Trace.WriteLine($"{nameof(TestReadLength)}:{nameof(testLength)}-{testLength}");
            }

            //测试跳转;
            void TestSeek(long pos,Stream testStream) {
                testStream.Position = pos;
                Assert.AreEqual(testStream.Position, pos);
            }


            var driveInfo = new DriveInfo("C");
            var vol = _comObject.LocalHdds.First().Volumes.First(p => p.Sign == 'C');

            //取得的总大小会与.Net API取得的大小有一定出入;
            //Assert.AreEqual(part.Size, driveInfo.TotalSize);

            Assert.AreEqual(vol.FreeSpace, driveInfo.AvailableFreeSpace);

            var volStream = vol.GetStream();
            Assert.AreEqual(volStream.Length, vol.Size);

            var hddStream = _comObject.LocalHdds.First().GetStream();
            Assert.AreEqual(hddStream.Length, _comObject.LocalHdds.First().Size);

            //测试按照扇区跳转;
            TestSeek(512 , volStream);
            TestSeek(512 , hddStream);

            //测试不按照扇区跳转;
            TestSeek(123 , volStream);
            TestSeek(123 , hddStream);
            

            //测试按照扇区整数倍读取;
            TestReadLength(512 , volStream);
            TestReadLength(512 , hddStream);

            //测试不按照扇区整数倍读取;
            TestReadLength(123 , volStream );
            TestReadLength(123 , volStream , false);
            TestReadLength(123 , hddStream);

            volStream.Position = 1;
            Assert.AreEqual(volStream.ReadByte(),0x52);
        }

        [TestMethod]
        public void ExitTest() {
            Assert.Fail();
        }

        [TestCleanup]
        public void Clean() {
            _comObject.Dispose();
        }
    }
}