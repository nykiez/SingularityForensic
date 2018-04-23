using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex;
using SingularityForensic.Test.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Hex {
    [TestClass()]
    public class HexDataContextCommandItemFactoryTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            var fs = File.OpenRead("E://anli/noname");
            _hexDataContext = HexService.Current.CreateNewHexDataContext(fs);;
            Assert.IsNotNull(_hexDataContext);
            Assert.AreEqual(_hexDataContext.Stream, fs);
        }

        IHexDataContext _hexDataContext;
        private void ExecuteCopy(long selectionStart,long selectionLength,Func<IHexDataContext,ICommandItem> createCommandItem) {
            _hexDataContext.SelectionStart = selectionStart;
            _hexDataContext.SelectionLength = selectionLength;

            var cmi = createCommandItem(_hexDataContext);
            _hexDataContext.AddContextCommand(cmi);
            Assert.IsTrue(_hexDataContext.ContextCommands.Contains(cmi));
            cmi.Command.Execute(null);
        }

        [TestMethod()]
        public void CreateCopyToNewFileCommandItemTest() {
            _hexDataContext.SelectionStart = 0;
            _hexDataContext.SelectionLength = 512;
            var saveFileName = "E://anli/noname2";
            AppMockers.SaveFileName = saveFileName;

            var cmi = HexDataContextCommandFactory.CreateCopyToNewFileCommandItem(_hexDataContext);
            _hexDataContext.AddContextCommand(cmi);
            Assert.IsTrue(_hexDataContext.ContextCommands.Contains(cmi));
            cmi.Command.Execute(null);

            Assert.IsTrue(File.Exists(saveFileName));

            var bts = File.ReadAllBytes(saveFileName);
            Assert.AreEqual(bts.Length, _hexDataContext.SelectionLength);
            Assert.AreEqual(bts[510], 0x55);
            Assert.AreEqual(bts[511], 0xAA);
        }

        [TestMethod()]
        public void CreateCopyToClipBoardCommandItemTest() {
            ExecuteCopy(510,2,HexDataContextCommandFactory.CreateCopyToClipBoardCommandItem);
            //剪切板怎么测试呢object呢? :(
            //....
        }

        [TestMethod]
        public void CreateCopyASCIIToCBoardCommandItemTest() {
            var rightString = "U?";
            ExecuteCopy(510,2, HexDataContextCommandFactory.CreateCopyASCIIToCBoardCommandItem);
            Assert.AreEqual(ClipBoardService.GetText(),rightString);
        }

        [TestMethod()]
        public void CreateCopyToCopyHexToCBoardCommandItemTest() {
            ExecuteCopy(510, 2, HexDataContextCommandFactory.CreateCopyToCopyHexToCBoardCommandItem);
            Assert.AreEqual(ClipBoardService.GetText(), "55AA");
        }

        [TestMethod()]
        public void CreateSetAsStartCommandItemTest() {
            _hexDataContext.FocusPosition = 512;
            var cmi = HexDataContextCommandFactory.CreateSetAsStartCommandItem(_hexDataContext);
            cmi.Command.Execute(null);
            Assert.AreEqual(_hexDataContext.SelectionStart, 512);
        }

        [TestMethod()]
        public void CreateSetAsEndCommandItemTest() {
            _hexDataContext.SelectionStart = 0;
            _hexDataContext.FocusPosition = 511;
            var cmi = HexDataContextCommandFactory.CreateSetAsEndCommandItem(_hexDataContext);
            cmi.Command.Execute(null);
            Assert.AreEqual(_hexDataContext.SelectionLength, 512);
            Assert.AreEqual(_hexDataContext.SelectionStart, 0);
        }

        [TestMethod()]
        public void CreateCopyAsProCodeCommandItemTest() {
            _hexDataContext.SelectionStart = 0;
            _hexDataContext.SelectionLength = 512;
            var cmi = HexDataContextCommandFactory.CreateCopyAsProCodeCommandItem(_hexDataContext);
            cmi.Children.ElementAt(0).Command.Execute(null);
        }

        [TestCleanup]
        public void Clean() {
            _hexDataContext.Stream.Dispose();
        }
    }
}