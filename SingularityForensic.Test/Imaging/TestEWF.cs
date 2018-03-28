using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EWF;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SingularityForensic.Test.Imaging {
    [TestClass]
    public class TestEWF {
        private const string _fileName = "I:\\test.E01";
        private const string _segName = "I:\\Frags\\test.E02";
        private Handle _handle;
        [TestInitialize]
        public void Init() {
            _handle = new Handle();

            Assert.IsTrue(Handle.CheckFileSignature(_fileName));

            var globs = Handle.Glob(_fileName);
            
            _handle.Open(globs, Handle.GetAccessFlagsRead());
        }

        //测试分块;
        [TestMethod]
        public void TestGlob() {
            var globs = Handle.Glob(_segName);
            
        }

        [TestMethod]
        public void TestRead() {
            var data = new byte[512];
            _handle.ReadBuffer(data, 512);
            Assert.AreEqual(data[0], 0xeb);
            Assert.AreEqual(data[1], 0x58);

            var bts = new byte[512];
            var count = _handle.GetNumberOfHeaderValues();
            var readCount = _handle.ReadBuffer(bts, 512);
            Assert.AreEqual(readCount,512);
        }

        [TestMethod]
        public void TestLength() {
            var length = _handle.SeekOffset(0, System.IO.SeekOrigin.End);
            Assert.AreNotEqual(length, 0);
            var pos = _handle.SeekOffset(500, System.IO.SeekOrigin.Begin);
            Assert.AreEqual(pos, 500);
            Assert.AreEqual(_handle.GetOffset(),500);
            
        }
        private const string exFileName = "D:\\1";

        //测试创建;
        [TestMethod]
        public void TestCreate() {
            var _hd = new Handle();
            _hd.Open(new string[] { exFileName,"D:\\2" }, Handle.GetAccessFlagsWrite());
            
            //_hd.SetBytesPerSector(512);
            //_hd.SetMediaSize(10240);
            
            var secSize = _hd.GetBytesPerSector();
            var chunkSize = _hd.GetChunkSize();
            _hd.SetSectorsPerChunk(100);
            
            var bSize = 5120;
            //_hd.SetSectorsPerChunk(20);
            _hd.SetFormat(15);

            var bts = new byte[chunkSize];
            //var rand = new Random();
            //rand.NextBytes(bts);
            for (int i = 0; i < 10000; i++) {
                _hd.WriteBuffer(bts, bSize);
            }
            
            _hd.Close();
            
            //_hd.Dispose();
        }

        [TestMethod]
        public void TestRead2() {
            var _hd = new Handle();
            _hd.Open(new string[] { exFileName }, Handle.GetAccessFlagsRead());
            var length = _hd.SeekOffset(0, System.IO.SeekOrigin.End);
        }

        [TestMethod]
        public void TestBasic() {
            
            var format = _handle.GetFormat();
            var secSize = _handle.GetSectorsPerChunk();
            var chunckSize = _handle.GetChunkSize();
        }
        [TestMethod]
        public void TestEntry() {
            var rootDir = _handle.GetRootFileEntry();
            
            Assert.IsNotNull(rootDir);
        }
    
        [TestMethod]
        public void TestHeaders() {
            var headerCount = _handle.GetNumberOfHeaderValues();
            for (int i = 0; i < headerCount; i++) {
                var id = _handle.GetHeaderValueIdentifier(i);
                var value = _handle.GetHeaderValue(id);
                Trace.WriteLine($"{id}  {value}");
            }
            
        }

        [TestCleanup]
        public void Clean() {
            _handle.Dispose();
        }

        [TestMethod]
        public void TestNativeSig() {
            Assert.IsTrue(libewf_check_file_signature_wide(_fileName, IntPtr.Zero) == 1);
        }

        const string ewfAsm = "libewf.dll";
        
        
        [DllImport(ewfAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static int libewf_check_file_signature_wide(
            [MarshalAs(UnmanagedType.LPWStr)]string filename,
            IntPtr err);
    }
}
