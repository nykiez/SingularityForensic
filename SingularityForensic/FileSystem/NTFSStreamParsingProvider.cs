using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace SingularityForensic.FileSystem {
    [Export(typeof(IStreamParsingProvider))]
    partial class NTFSStreamParsingProvider : IStreamParsingProvider {
        public int Order => 8;

        public string GUID => Constants.StreamParser_NTFS;

        public bool CheckIsValidStream(Stream stream) {
            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            try {
                //var unManagedManager = new UnmanagedBasicDeviceManager(stream);
                //if(unManagedManager.BasicDevicePtr == IntPtr.Zero) {
                //    return false;
                //}

                //var isFat = Partition_B_Ntfs(unManagedManager.BasicDevicePtr);
                //unManagedManager.Dispose();
                return false;
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                return false;
            }
        }

        public IFile ParseStream(Stream stream, string name, XElement xElem, IProgressReporter reporter) {
            throw new NotImplementedException();
        }
    }

    partial class NTFSStreamParsingProvider {
        private const string partAsm = "PartitionManager.dll";
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static bool Partition_B_Ntfs(IntPtr stPartition);
    }

}
