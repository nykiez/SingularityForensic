using CDFC.Parse.Android.DeviceObjects;
using CDFC.Parse.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Singularity.Android.Models;
using Singularity.Android.Services;
using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using Singularity.Contracts.FileSystem;
using Singularity.UI.Case.Services;
using Singularity.UI.Case.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AndroidInfo.Tests {
    /// <summary>
    /// 服务提供者Mocker;
    /// </summary>
    public class MockServiceProvider: EmptyServiceProvider<MockServiceProvider> {
        public override object GetInstance(Type serviceType) {
            foreach (var item in maps) {
                if(item.Key == serviceType) {
                    return item.Value;
                }
            }
            
            return null;
        }

        /// <summary>
        /// 设定当前实例;
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="instance"></param>
        public void SetInstance<TInstance>(TInstance instance) {
            foreach (var item in maps) {
                if(item.Key == typeof(TInstance)) {
                    throw new InvalidOperationException($"The Type {typeof(TInstance)} has already been set.");
                }
            }

            maps.Add(typeof(TInstance), instance);
        }

        //类型保存图;
        Dictionary<Type, object> maps = new Dictionary<Type, object>();
    }

    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestCall() {
            //var vm = new CreateCaseWindowViewModel();
            //SingularityCase.Current = vm.SingulartityCase;
            //SingularityCase.Current.Save();
            //var device = AndroidDevice.LoadFromPath("G:/MobileImgs/Coolpad/mmcblk0", true, tp => {
            //    Console.WriteLine($"{tp.curSize}/{tp.allSize}");
            //});
            //var adv = new AndroidDeviceCaseFile(device, string.Empty, DateTime.Now);
            //Console.WriteLine($"Building Xml Doc");
            //SingularityCase.Current.AddNewCaseFile(adv);

            //var tp2 = AdvPythonHelper.GetProcessOutPut(adv, "qq_extract.py");

            //xDoc.Save("new.xml");
            //Assert.IsTrue(File.Exists(SingularityCase.Current.))
        }
        
        /// <summary>
        /// 预设案件服务以及Provider;
        /// </summary>
        private void SetCaseAndProvider() {
            //将mocker设定为当前服务器提供器;
            ServiceProvider.SetServiceProvider(MockServiceProvider.StaticInstance);

            //创建案件服务;
            var cvm = new CreateCaseWindowViewModel();
            //保存;
            cvm.SingulartityCase.Save();
            var service = new CaseService();
            //加载案件;
            service.LoadCase(cvm.SingulartityCase);

            //将案件服务加载至mocker中;
            MockServiceProvider.StaticInstance.SetInstance<ICaseService>(service);

            //var provider = new Mock<Singularity.Contracts.Common.IServiceProvider>();
            //provider.Setup(p => p.GetInstance(It.Is<Type>(tp => tp == typeof(ICaseService)))).Returns(service);
            
        }

        
        //镜像文件路径;
        const string imgPath = "G:/MobileImgs/Vivo/VIVO.ardimg";

        /// <summary>
        /// 本方法获得将加载一个镜像并提供对应的文件系统服务与对应案件文件远足;
        /// </summary>
        /// <returns></returns>
        public (IFileSystemServiceProvider fsProvider,ICaseEvidence evidence) GetAndroidDeviceFileSystemServiceProvider() {
            //预设案件服务以及Provider;
            SetCaseAndProvider();

            //加载一个镜像文件;
            var device = AndroidDevice.LoadFromPath(imgPath,true,tuple => {
                //Debug.WriteLine($"{tuple.curSize}/{tuple.allSize}");
            });
            Assert.IsNotNull(device);

            //加载至案件中;
            var adEvidence = new AndroidDeviceCaseEvidence(device, imgPath, DateTime.Now);
            ServiceProvider.Current.GetInstance<ICaseService>().AddNewCaseFile(adEvidence);


            var file = AndroidDeviceFileSystemServiceProvider.StaticInstance.OpenFile(adEvidence,"1/");

            Assert.AreEqual(file.File, device.Children.ElementAt(1));

            

            return (AndroidDeviceFileSystemServiceProvider.StaticInstance, adEvidence);
            //new AndroidDeviceCaseFile
        }

        [TestMethod]
        public void TestAdFsServiceProvider() {
            var s = GetAndroidDeviceFileSystemServiceProvider();
            var csService = ServiceProvider.Current.GetInstance<ICaseService>();
            foreach (var item in csService.CurrentCase.CaseEvidences) {
                
            }
            var part = s.fsProvider.OpenFile(s.evidence, "0/");
            var s2 = part.Children;
        }


        [TestMethod]
        public void TestStartForensic() {
            //ServiceLocator.SetLocatorProvider()
        }
    }
}
