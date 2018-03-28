using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

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

        //测试多线程下的读写是否正常;
        private void TestThread() {
            var ptr = _adapter.StreamPtr;
            ThreadPool.QueueUserWorkItem(cb => {
                Thread.Sleep(4000);
                ReadTest(ptr);
                WriteTest(ptr);
                evt.Set();
            });
            return;
        }
        AutoResetEvent evt = new AutoResetEvent(false);

        
        //测试垃圾回收器对适配器类的影响;
        [TestMethod]
        public void TestGCForUnmanagedAdapter() {
            TestThread();
            GC.Collect();
            GC.Collect();
            evt.WaitOne();
        }

        //测试读取;
        [TestMethod]
        private void TestRead() {
            var ptr = _adapter.StreamPtr;
            ReadTest(ptr);
        }

        //测试长度和位置;
        [TestMethod()]
        public void TestLengthAndPos() {
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