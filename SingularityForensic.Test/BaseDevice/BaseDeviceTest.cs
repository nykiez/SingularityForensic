using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.BaseDevice;

namespace SingularityForensic.Test.BaseDevice {
    [TestClass]
    public class BaseDeviceTest {
        [TestMethod]
        public void PrintStInfoDiskFields() {            
            PrintStructFields<StInFoDisk>(Constants.BaseDeviceFieldPrefix_InfoDisk);
        }

        [TestMethod]
        public void PrintStEFIInfoFields() {
            PrintStructFields<StEFIInfo>(Constants.GptFieldPrefix_EFIInfo);
        }

        [TestMethod]
        public void PrintStEFIPTableFields() {
            PrintStructFields<StEFIPTable>(Constants.GptFieldPrefix_EFIPTable);
        }

        private void PrintStructFields<T>(string prefix) {
            var tp = typeof(T);
            foreach (var fieldInfo in tp.GetFields()) {
                Trace.WriteLine($"<sys:String x:Key=\"{ prefix}{ fieldInfo.Name}\"></sys:String>");
            }
        }


    }
}
