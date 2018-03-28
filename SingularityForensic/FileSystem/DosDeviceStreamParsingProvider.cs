using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.FileSystem {
    [Export(typeof(IStreamParsingProvider))]
    class DosDeviceStreamParsingProvider : BaseDeviceStreamParsingProvider {
        //StDosPTable* Partition_Get_DosPTable(void* stPartition);
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        protected extern static IntPtr Partition_Get_DosPTable(IntPtr stPartition);

        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        protected extern static bool Partition_B_Dos(IntPtr stPartition);

        public override int Order => 64;

        public override FileBase ParseStream(Stream stream, string name, XElement xElem, ProgressReporter reporter) {
            var tuple = GetManagerAndStreamPtr(stream);
            if(tuple == null) {
                return null;
            }

            (UnmanagedStreamAdapter adapter, IntPtr mgrPtr) = tuple.Value;

            try {
                //var device = new Device(Constants.DeviceKey_DOS)
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            return null;
        }

        protected override bool AssertIsValid(IntPtr streamPtr) {
            if(streamPtr == IntPtr.Zero) {
                return false;
            }

            return Partition_B_Dos(streamPtr);
        }
    }
}
