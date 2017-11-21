using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Singularity.UI.MessageBoxes.ViewModels;
using CDFC.Singularity.Forensics.Cases;
using CDFC.Parse.Android.DeviceObjects;
using SingularityForensic.Modules.FileSystem.Models;
using Singularity.UI.Info.Android.Models;
using System.IO;
using CDFC.Info.Android;
using Singularity.UI.Info.Android.Helpers;

namespace AndroidInfo.Tests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestCall() {
            var vm = new CreateCaseWindowViewModel();
            SingularityCase.Current = vm.SingulartityCase;
            SingularityCase.Current.Save();
            var device = AndroidDevice.LoadFromPath("G:/MobileImgs/Coolpad/mmcblk0", true, tp => {
                Console.WriteLine($"{tp.curSize}/{tp.allSize}");
            });
            var adv = new AndroidDeviceCaseFile(device, string.Empty, DateTime.Now);
            Console.WriteLine($"Building Xml Doc");
            SingularityCase.Current.AddCaseFile(adv);

            var tp2 = AdvPythonHelper.GetProcessOutPut(adv, "qq_extract.py");

            //xDoc.Save("new.xml");
            //Assert.IsTrue(File.Exists(SingularityCase.Current.))
        }
        [TestMethod]
        public void InstanceChatTest() {
            var context = new AndroidDeviceQQContext("data source = D:/C# Console/Python/2017/qq_53077093.db");
            foreach (var item in context.FriendMsgs) {

            }
            foreach (var item in context.Friends) {

            }
            foreach (var item in context.GroupMembers) {

            }
            foreach (var item in context.GroupMsgs) {

            }
            context.Dispose();
        }

        [TestMethod]
        public void TestStartForensic() {
            
        }
    }
}
