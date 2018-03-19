using CDFC.Parse.Modules.Static;
using CDFC.Parse.Modules.Structs;
using CDFC.Util.PInvoke;
using EventLogger;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CDFC.Parse.Modules.DeviceObjects {
    public class DosDevice : Device {
        private const string DosAssembly = "DiskManager.dll";
        [DllImport(DosAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr DiskManager_Get_PTable(SafeFileHandle hDisk);
        [DllImport(DosAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static void DiskManager_Exit();

        private List<PartitionTableItem> _tableItems = new List<PartitionTableItem>();
        public IEnumerable<PartitionTableItem> TableItems => _tableItems;

        public override int SecSize { get; protected set; }

        private Stream _stream;
        public override Stream Stream => _stream;

        public override PartsType PartsType { get; }

        public override long Size { get; }

        public override void Exit() {
            throw new NotImplementedException();
        }

        public static DosDevice LoadFromPath(string path, bool readOnly = true,
            Action<(
                long curSize, long allSize,
                long curPartSize,
                long thePartSize,
                int curPart, int allPart)> ntfSizeAct = null,
            Func<bool> isCancel = null) {

            FileStream fs = null;

            try {
                fs = File.Open(path, FileMode.Open, readOnly ? FileAccess.Read : FileAccess.ReadWrite, FileShare.ReadWrite);
                return LoadFromFileStream(fs, ntfSizeAct, isCancel);
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(AndroidDevice)} -> {nameof(LoadFromPath)}:{ex.Message}");
                fs?.Close();
                throw new Exception("Failed to load img!", ex);
            }
        }


        public static DosDevice LoadFromFileStream(FileStream fs,
            Action<(
                long curSize, long allSize,
                long curPartSize,
                long thePartSize,
                int curPart, int allPart)> ntfSizeAct = null,
            Func<bool> isCancel = null) {
            if (fs == null) {
                throw new ArgumentNullException(nameof(fs));
            }

            var ptr = DiskManager_Get_PTable(fs.SafeFileHandle);
            if(ptr == IntPtr.Zero) {
                return null;
            }

            var device = new DosDevice {
                _stream = fs
            };

            while (ptr != IntPtr.Zero) {
                try {
                    var ptable = ptr.GetStructure<StPTable>();

                    device._tableItems.Add(new PartitionTableItem {
                        Offset = (long) ptable.nOffset,
                        Length = (ptable.EFIPTable != IntPtr.Zero)?(Marshal.SizeOf(typeof(StEFIPTable))):(Marshal.SizeOf(typeof(StInFoDisk)))
                    });
                    ptr = ptable.next;
                }
                catch(Exception ex) {
                    LoggerService.Current?.WriteCallerLine(ex.Message);
                    break;
                }

            }
            return device;
        }

        //public static bool CheckIsValid(string path) {

        //}
    }
}
