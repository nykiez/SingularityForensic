using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.FileSystem {
    [TestClass]
    public class DosAndGptTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _parsingProviders = ServiceProvider.Current.GetAllInstances<IStreamParsingProvider>();
            _fsService = ServiceProvider.Current.GetInstance<IFileSystemService>();

            Assert.AreNotEqual(_parsingProviders.Count(), 0);
            Assert.IsNotNull(_fsService);
        }

        private const string GPTImgPath = "E://anli/gpt.img";
        private const string DOSImgPath = "E://anli/dos.img";
        private const int DOSPartCount = 5;
        private const int GPTPartCount = 5;

        private Stream _stream;
        private IEnumerable<IStreamParsingProvider> _parsingProviders;
        private IFileSystemService _fsService;
        
        [TestMethod]
        public void TestDOSDevice() {
            _stream = File.OpenRead(DOSImgPath);
            var file = _fsService.MountStream(_stream, string.Empty, null, null);

            //检查"签名";
            Assert.IsTrue(file.TypeGuids.Contains(SingularityForensic.FileSystem.Constants.DeviceType_DOS));

            if (file is Device device) {
                Assert.AreEqual(device.PartitionEntries.Count(), DOSPartCount);
                foreach (var entry in device.PartitionEntries) {
                    Trace.WriteLine($"{entry.PartStartLBA} - {entry.PartSize}");
                }
                device.Dispose();
            }
            else {
                Assert.Fail($"The {nameof(file)} should be a {nameof(Device)}.");
            }
            
        }

        [TestMethod]
        public void TestGPTDevice() {
            _stream = File.OpenRead(GPTImgPath);
            var file = _fsService.MountStream(_stream, string.Empty, null, null);

            //检查"签名";
            Assert.IsTrue(file.TypeGuids.Contains(SingularityForensic.FileSystem.Constants.DeviceType_GPT));

            if(file is Device device) {
                Assert.AreEqual(GPTPartCount,device.PartitionEntries.Count());

                foreach (var entry in device.PartitionEntries) {
                    Trace.WriteLine($"{entry.PartStartLBA} - {entry.PartSize}");
                }
                device.Dispose();
            }
        }

        [TestCleanup]
        public void Clean() {
            _stream?.Dispose();
        }
    }
}
