using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Hash;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace SingularityForensic.Test.Hash {
    [TestClass()]
    public class HasherTests {
        
        [TestInitialize]
        public void Initialize() {
            //TestCommon.InitializeTest();
            _hasher = new MD5Hasher();
        }

        IHasher _hasher;

        [TestMethod()]
        public void ComputeHashTest() {
            string md5String = "0FA777FF2D120A61F76D379E138DFE66";
            
            var proMocker = new Mock<IProgressReporter>();
            var lastPro = 0;

            proMocker.Setup(p => p.ReportProgress(It.IsAny<int>())).
                Callback<int>(p => {
                    if(lastPro < p) {
                        lastPro = p;
                        Trace.WriteLine(lastPro);
                    }
                });
            proMocker.Setup(p => p.Cancel()).Raises(p => p.Canceld += null, EventArgs.Empty);

            
            //检查值是否正确;
            using (var fs = File.OpenRead("E://anli/FAT32.img")) {
                var bts = _hasher.ComputeHash(fs, proMocker.Object);
                var sb = new StringBuilder();
                
                foreach (var bt in bts) {
                    sb.Append(bt.ToString("X2"));
                }

                Assert.AreEqual(sb.ToString(), md5String);
            }

            //检查是否能够正常取消;
            ThreadPool.QueueUserWorkItem(cb => {
                Thread.Sleep(1000);
                proMocker.Object.Cancel();
            });

            using (var fs = File.OpenRead("E://anli/FAT32.img")) {
                var bts = _hasher.ComputeHash(fs, proMocker.Object);
                Assert.IsNull(bts);
            }
        }

        
        
    }
}