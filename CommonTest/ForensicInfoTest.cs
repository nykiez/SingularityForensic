using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTest {
    /// <summary>
    /// 服务提供者Mocker;
    /// </summary>
    public class MockServiceProvider : EmptyServiceProvider<MockServiceProvider> {
        public override object GetInstance(Type serviceType) {
            foreach (var item in maps) {
                if (item.Key == serviceType) {
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
                if (item.Key == typeof(TInstance)) {
                    throw new InvalidOperationException($"The Type {typeof(TInstance)} has already been set.");
                }
            }

            maps.Add(typeof(TInstance), instance);
        }

        //类型保存图;
        Dictionary<Type, object> maps = new Dictionary<Type, object>();
    }

    [TestClass]
    public class ForensicInfoTest {
        //预设MockerProvider;
        private void SetServiceProvider() {
            //将mocker设定为当前服务器提供器;
            SingularityForensic.Contracts.Common.ServiceProvider.SetServiceProvider(MockServiceProvider.StaticInstance);
        }

        /// <summary>
        /// 预设案件服务以及Provider;
        /// </summary>
        private void SetCaseService() {


            //创建案件服务;
            var cvm = new CreateCaseWindowViewModel();
            //保存;
            cvm.SingularityCase.Save();
            var service = new CaseService();
            //加载案件;
            service.LoadCase(cvm.SingularityCase);

            //将案件服务加载至mocker中;
            MockServiceProvider.StaticInstance.SetInstance<ICaseService>(service);

            //var provider = new Mock<SingularityForensic.Contracts.Common.IServiceProvider>();
            //provider.Setup(p => p.GetInstance(It.Is<Type>(tp => tp == typeof(ICaseService)))).Returns(service);

        }

        //预设文件系统服务器提供者;
        private void SetFileSystemServiceProvider() {
            MockServiceProvider.StaticInstance.SetInstance<IFileSystemServiceProvider>(new FileSystemServiceProvider());
        }

        //镜像文件路径;
        const string imgPath = "G:/MobileImgs/Vivo/VIVO.ardimg";
        /// <summary>
        /// 本方法获得将加载一个镜像文件到案件中;
        /// </summary>
        /// <returns></returns>
        public CaseEvidence GetAdCaseEvidence() {
            //加载一个镜像文件;
            var device = AndroidDevice.LoadFromPath(imgPath, true, tuple => {
                //Debug.WriteLine($"{tuple.curSize}/{tuple.allSize}");
            });
            Assert.IsNotNull(device);

            //加载至案件中;
            var adEvidence = new AndroidDeviceCaseEvidence(device, imgPath, DateTime.Now);
            ServiceProvider.Current.GetInstance<ICaseService>().AddNewCaseFile(adEvidence);

            var file = ServiceProvider.Current.GetInstance<IFileSystemServiceProvider>()?.OpenFile($"{adEvidence.GUID}/1/");

            Assert.AreEqual(file.File, device.Children.ElementAt(1));



            return adEvidence;
            //new AndroidDeviceCaseFile
        }

        /// <summary>
        /// 本方法将设立ServiceProvider，案件，文件系统服务，并加载一个来自imgPath的镜像到案件中;
        /// 以提供测试所需数据源;
        /// </summary>
        [TestMethod]
        public void TestAdServiceProvider() {
            //预设ServiceProvider,案件服务以及文件系统服务;
            SetServiceProvider();
            SetCaseService();
            SetFileSystemServiceProvider();
            //加载镜像;
            var evidence = GetAdCaseEvidence();

            //获得文件系统服务;
            var fsService = ServiceProvider.Current.GetInstance<IFileSystemServiceProvider>();

            //获得案件服务;
            var csService = ServiceProvider.Current.GetInstance<ICaseService>();

            var forensicInfoProvider = new AdImgInfoForensicInfoServiceProviderExample();

            forensicInfoProvider.StartForensic(evidence, new string[] { "dasdad", "dasd3123" }, tuple => {
                Debug.WriteLine($"{tuple.word}:{tuple.percentage} / 100");
            });
        }


        [TestMethod]
        public void TestStartForensic() {
            //ServiceLocator.SetLocatorProvider()
        }
    }
}
