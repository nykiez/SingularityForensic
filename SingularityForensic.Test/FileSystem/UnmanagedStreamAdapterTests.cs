using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem.Tests {
    [TestClass]
    public class UnmanagedStreamAdapterTests {
        [TestInitialize]
        public void Initialize() {
            _fs = File.Open("D://vc_2015.x64.exe", FileMode.Open, FileAccess.ReadWrite);
            Assert.IsNotNull(_fs);
            _adapter = new UnmanagedStreamAdapter(_fs);
            Assert.IsNotNull(_fs);
        }
        
        FileStream _fs = null;
        UnmanagedStreamAdapter _adapter = null;

        private const string streamAsm = "StreamExtension.dll";
        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void ReadTest(IntPtr stream);
        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void WriteTest(IntPtr stream);
        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern long GetStreamLength(IntPtr stream);
        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern long GetStreamPosition(IntPtr stream);
        [DllImport(streamAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetStreamPosition(IntPtr stream, long pos);

        private void TestThread() {
            var fs = File.Open("D://vc_2010_x86.exe", FileMode.Open, FileAccess.ReadWrite);
            var stream = new UnmanagedStreamAdapter(fs);
            var ptr = stream.StreamPtr;
            ThreadPool.QueueUserWorkItem(cb => {
                Thread.Sleep(4000);
                ReadTest(ptr);
                WriteTest(ptr);
                evt.Set();
            });
            return;
        }
        AutoResetEvent evt = new AutoResetEvent(false);

        
        [TestMethod]
        public void UnmanagedStreamAdapterTest() {
            TestThread();
            GC.Collect();
            GC.Collect();
            evt.WaitOne();
        }

        [TestMethod()]
        public void TestLength() {
            Assert.AreEqual(_fs.Length, GetStreamLength(_adapter.StreamPtr));

            Assert.AreEqual(_fs.Position, GetStreamPosition(_adapter.StreamPtr));

            var tPos = 128;
            SetStreamPosition(_adapter.StreamPtr, tPos);

            Assert.AreEqual(_fs.Position, tPos);

            Assert.AreEqual(GetStreamPosition(_adapter.StreamPtr), tPos);
        }

        [TestCleanup]
        public void Clean() {
            _fs.Close();
            _adapter.Dispose();
        }
    }
   
}