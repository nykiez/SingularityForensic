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
            var device = FileFactory.CreateDevice(string.Empty);
            for (int i = 0; i < 24; i++) {
                var part = FileFactory.CreatePartition(string.Empty);
                var entry = part.GetStoken(string.Empty);
                entry.Size = 1024;
                entry.Name = testPartName;
                device.Children.Add(part);
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