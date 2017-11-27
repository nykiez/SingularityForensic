using CDFC.Parse.Android.Contracts;
using CDFC.Parse.Android.DeviceObjects;
using CDFC.Parse.Contracts;
using CDFCCultures.Helpers;
using Singularity.UI.FileSystem.Models;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.FileSystem.Android.Models {
    public class AndroidFileRow<TFile>:FileRow<TFile> where TFile:IFile {
        public AndroidFileRow(TFile file):base(file) {

        }

        private string _filePermission;
        public override string FilePermission {
            get {
                if (_filePermission == null) {
                    if (File is IExt4Node) {
                        _filePermission = FindResourceString((File as IExt4Node).GetPermission().ToString());
                    }
                }
                return _filePermission;
            }
        }

        //内部标识ID;
        private string _gid;
        public override string GID {
            get {
                if (File is IExt4Node) {
                    _gid = (File as IExt4Node).GetUID().ToString();
                }
                return _gid;
            }
        }

        //标识ID;
        private string _uid;
        public override string UID {
            get {
                if (File is IExt4Node) {
                    _uid = (File as IExt4Node).GetGID().ToString();
                }
                return _uid;
            }
        }

        public override string LastWriteTime {
            get {
                if (File is AndroidPartition) {
                    return (File as AndroidPartition).LastWriteTime.ToWDTimeString();
                }
                return string.Empty;
            }
        }

        public override string LastMountTime {
            get {
                if (File is AndroidPartition) {
                    return (File as AndroidPartition).LastMountTime.ToWDTimeString();
                }
                return string.Empty;
            }
        }
    }
}
