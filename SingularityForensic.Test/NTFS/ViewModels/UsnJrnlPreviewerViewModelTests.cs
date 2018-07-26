using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.NTFS.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.NTFS.ViewModels {
    [TestClass()]
    public class UsnJrnlPreviewerViewModelTests {

        [TestInitialize]
        public void Initialize() {
            _vm = new UsnJrnlPreviewerViewModel(File.OpenRead(UsnFileName));
            Assert.IsNotNull(_vm.Stream);
        }
        private const string UsnFileName = "E://anli/$UsnJrnl(1)";
        private UsnJrnlPreviewerViewModel _vm;

        [TestMethod()]
        public void UsnJrnlPreviewerViewModelTest() {
            Assert.Fail();
        }
        
        [TestMethod()]
        public void LoadRecordsTest() {
            _vm.LoadRecords();
            Assert.AreNotEqual(_vm.Records.Count(), 0);
            foreach (var record in _vm.Records) {
                Trace.WriteLine(record.FileName);
            }
        }

        [TestCleanup]
        public void Clean() {
            _vm.Stream.Dispose();
        }
    }
}