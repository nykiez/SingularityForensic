using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FAT;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace SingularityForensic.Test.FAT {
    [TestClass()]
    public class FatStreamParsingProviderTests {
        private const string FAT32ImgPath = "E://anli//FAT32.img";
        private const string NotFAT32Path = "J:\\test_anli\\phone\\mmcblk0";
        private IStreamParsingProvider _streamParsingProvider;
        
        private Stream _fatStream;

        private const string UnknownImgPath = "E://anli/1.E01";

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();

            _fatStream = File.OpenRead(NotFAT32Path);
            _streamParsingProvider = ServiceProvider.Current.GetAllInstances<IStreamParsingProvider>().
                FirstOrDefault(p => 
                    p.GUID == SingularityForensic.FAT.Constants.StreamParserGUID_FAT
                );

            Assert.IsNotNull(_streamParsingProvider);
        }

        

        [TestMethod()]
        public void CheckIsValidStreamTest() {
            Assert.IsTrue(!_streamParsingProvider.CheckIsValidStream(_fatStream));

            using(var unKnownStream = File.OpenRead(UnknownImgPath)) {
                Assert.IsFalse(_streamParsingProvider.CheckIsValidStream(unKnownStream));
            }
        }
        
        [TestMethod]
        public void TestStructSize() {
            Assert.AreEqual( Marshal.SizeOf(typeof(StFatFileNode)) , 557);
            Assert.AreEqual( Marshal.SizeOf(typeof(StFatClusterNode)) , 20);
        }

        [TestMethod()]
        public void ParseStreamTest() {
            var file = _streamParsingProvider.ParseStream(_fatStream, string.Empty, null, null);
            var part = file as IPartition;
            Assert.IsNotNull(part);
            Assert.AreEqual(part.PartType.GUID , SingularityForensic.FAT.Constants.PartitionType_FAT32);
        }

        [TestMethod]
        public void TestFileName() {
            var fileBts = new byte[] {
                72 ,
                0  ,
                50 ,
                0  ,
                54 ,
                0  ,
                52 ,
                0  ,
                83 ,
                0  ,
                114,
                0  ,
                99 ,
                0 ,
                0,
                0 ,
                0,
                0 ,
                0,
                0 ,
                0 ,
                0 ,
                0,
                0
            };
            var fileName = StFatFileNode.ConvertFatBytesToString(fileBts);
            Assert.AreEqual("H264Src",fileName);

            
            //StFatFileNode.ConvertFatBytesToString()
        }

        [TestCleanup]
        public void Clean() {
            _fatStream.Dispose();
        }
    }
}