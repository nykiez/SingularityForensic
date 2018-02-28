using CDFCCultures.Managers;
using CDFCUIContracts.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Adb.ViewModels.AdbViewer;
using System;
using System.Linq;
using System.Threading;

namespace AdbViewer.Test {
    [TestClass]
    public class AdbAquirTest {

        [TestMethod]
        public void DownLoadTest() {
            var mc = new Mock<IResourceManager>();
            mc.Setup(p => p.FindResourceString(It.IsAny<string>())).Returns("Dasd");
            mc.Setup(p => p.FindResourceString("PhoneInfoAquiredItems")).Returns("手机信息获取项");
            mc.Setup(p => p.FindResourceString("PhoneFileAquiredItems")).Returns("手机文件获取项");

            mc.Setup(p => p.FindResourceString("BasicInfo")).Returns("基本信息");

            ManagerLocator.SetInstance<IResourceManager>(mc.Object);

            var ms = new Mock<IStaInvoker>();
            ms.Setup(p => p.Invoke(It.IsAny<Action>())).Callback((Action act) => {
                act.Invoke();
            });
            ApplicationHelper.SetInstance(ms.Object);

            var adbVM = new AdbViewerViewModel();
            
            adbVM.DeviceSelectorViewModel.RefreshDevices();
            adbVM.DeviceSelectorViewModel.SelectedDevice = adbVM.DeviceSelectorViewModel.Devices[0];
            adbVM.InfoCheckerViewModel.Device = adbVM.DeviceSelectorViewModel.SelectedDevice.Device;
            foreach (var item in adbVM.InfoCheckerViewModel.AdbUnits) {
                item.IsChecked = false;
                if(item.Name == "手机文件获取项") {
                    item.Children.First(p => p.Name == "备份").IsChecked = true ;
                }
            } 
            
            //adbVM.InfoCheckerViewModel.ConfirmCommand.Execute();
            //while (adbVM.InfoCheckerViewModel.IsAquiring) {
            //    Thread.Sleep(1000);
            //}
        }
    }
}
