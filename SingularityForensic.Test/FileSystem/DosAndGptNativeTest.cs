using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using CDFC.Util.PInvoke;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.BaseDevice;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileSystem;

namespace SingularityForensic.Test.FileSystem {
    /// <summary>
    /// 非托管调用调试;
    /// </summary>
    [TestClass]
    public class DosAndGptNativeTest {
        private const string partAsm = "PartitionManager.dll";
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Partition_Init(IntPtr stStream);

        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static bool Partition_B_Dos(IntPtr stPartition);

        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static bool Partition_B_Gpt(IntPtr stPartition);

        //StDosPTable* Partition_Get_DosPTable(void* stPartition);
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Partition_Get_DosPTable(IntPtr stPartition);

        //StGptPTable* Partition_Get_GptPTable(IntPtr stPartition);
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Partition_Get_GptPTable(IntPtr stPartition);

        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static void Partition_Exit(IntPtr stPartition);
        
        [TestInitialize]
        public void Initialize() {
            _fs = File.OpenRead("E://anli/gpt.img");
            //"G:\\MobileImgs\\Honor\\mmcblk0");
            //"J://anli/noname");
            _unManagedStreamAdapter = new UnmanagedStreamAdapter(_fs);
            Assert.AreNotEqual(_unManagedStreamAdapter.StreamPtr, IntPtr.Zero);
            _partPtr = Partition_Init(_unManagedStreamAdapter.StreamPtr);
            Assert.AreNotEqual(_partPtr, IntPtr.Zero);
        }

        private FileStream _fs;
        private UnmanagedStreamAdapter _unManagedStreamAdapter;
        private IntPtr _partPtr;

        [TestMethod]
        public void TestDos() {
            var isDos = Partition_B_Dos(_partPtr);
            Assert.IsTrue(isDos);
            
            var partPtr = Partition_Get_DosPTable(_partPtr);
            Assert.AreNotEqual(partPtr, IntPtr.Zero);

            var partNode = partPtr;
            while(partNode != IntPtr.Zero) {
                var dosPTable = partNode.GetStructure<StDosPTable>();
                Trace.WriteLine($"{dosPTable.DosPartType} - {dosPTable.nOffset}");
                var stInfo = dosPTable.Info.GetStructure<StInFoDisk>();
                Trace.WriteLine($"\t{stInfo.HeadSector} - {stInfo.AllSector}");
                partNode = dosPTable.next;
            }
            
        }

        [TestMethod]
        public void TestGpt() {
            var isGpt = Partition_B_Gpt(_partPtr);
            Assert.IsTrue(isGpt);

            var partPtr = Partition_Get_GptPTable(_partPtr);
            Assert.AreNotEqual(partPtr, IntPtr.Zero);

            var partNode = partPtr;
            while(partNode != IntPtr.Zero) {
                var gptPTable = partNode.GetStructure<StGptPTable>();
                Trace.WriteLine(gptPTable.nOffset);
                var stEFIInfo = gptPTable.EFIInfo.GetStructure<StEFIInfo>();
                
                var stInfo = gptPTable.Info.GetStructure<StInFoDisk>();
                Trace.WriteLine($"\t{stEFIInfo.GPTStartLBA * 512} - {stEFIInfo.GPTEndLBA * 512}");
                var stEFITable = gptPTable.EFIPTable.GetStructure<StEFIPTable>();
                if (gptPTable.EFIPTable != IntPtr.Zero) {
                    Trace.WriteLine($"\t{stEFITable.PartTabStartLBA } - {stEFITable.PartTabEndLBA }");
                }
                else {
                    Trace.WriteLine($"Null EFITable");
                }
                
                //Trace.WriteLine($"\t{stInfo.HeadSecor * 512} - {stInfo.AllSector}");
                partNode = gptPTable.next;
            }
        }

        [TestCleanup]
        public void Clean() {
            _unManagedStreamAdapter.Dispose();
            Partition_Exit(_partPtr);
        }
    }
}
