using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.FileExplorer.ViewModels;
using SingularityForensic.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Tests {
    [TestClass()]
    public class PartitionsBrowserViewModelTests {
        private const string testPartName = nameof(testPartName);
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            var device = new Device(string.Empty, new DeviceStoken {
                
            });
            for (int i = 0; i < 24; i++) {
                device.Children.Add(new Partition(string.Empty, new PartitionStoken {
                    Size = 1024,
                    Name = testPartName
                }));
            }
            
            _vm = new PartitionsBrowserViewModel(device);

            Assert.AreEqual(_vm.Device, device);
            Assert.AreEqual(_vm.Partitions.Count, 24);
        }

        private PartitionsBrowserViewModel _vm;
        [TestMethod()]
        public void TestPartitionDoubleClick() {
            var clicked = false;
            
            PubEventHelper.GetEvent<PartitionDoubleClickedEvent>().Subscribe(tuple => {
                clicked = true;
                Assert.AreEqual(tuple.part, _vm.Partitions[0].File);
            });

            _vm.NotifyDoubleClickOnRow(_vm.Partitions[0]);
            Assert.IsTrue(clicked);
        }
    }
}