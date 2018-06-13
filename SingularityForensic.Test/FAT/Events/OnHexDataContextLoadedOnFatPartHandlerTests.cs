using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using SingularityForensic.FAT;
using SingularityForensic.FAT.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.FAT.Events {
    [TestClass()]
    public class OnHexDataContextLoadedOnFatPartHandlerTests {

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _onHexDataContextLoadedOnFatPartHandler = ServiceProvider.GetAllInstances<IHexDataContextLoadedEventHandler>().
                FirstOrDefault(p => p is OnHexDataContextLoadedOnFatPartHandler) as OnHexDataContextLoadedOnFatPartHandler;

            Assert.IsNotNull(_onHexDataContextLoadedOnFatPartHandler);
        }

        [TestMethod]
        public void PrintDBRFields() {
            int s = 0;
            var size = Marshal.SizeOf(typeof(IntPtr));
            var ss = IntPtr.Size;
            Assert.AreEqual(size, 4);
            var tp = typeof(StFatINFO);
            var fields = tp.GetFields();
            foreach (var item in fields) {
                Trace.WriteLine($"<sys:String x:Key=\"{"FATFSInfo_"+item.Name,30}\"></sys:String>");
                
                //Trace.WriteLine(item.Name);
                //var attr = MarshalAsAttribute.GetCustomAttribute(item, typeof(MarshalAsAttribute));
            }
        }

        OnHexDataContextLoadedOnFatPartHandler _onHexDataContextLoadedOnFatPartHandler;
        [TestMethod()]
        public void HandleTest() {
            var hexDataContext = HexService.Current.CreateNewHexDataContext(null);
            var part = FatMockers.GetFATPartitition();
            Assert.IsNotNull(part);
            hexDataContext.SetInstance<IFile>(part, SingularityForensic.Contracts.FileExplorer.Constants.HexDataContextTag_File);
            _onHexDataContextLoadedOnFatPartHandler.Handle(hexDataContext);


        }
    }
}