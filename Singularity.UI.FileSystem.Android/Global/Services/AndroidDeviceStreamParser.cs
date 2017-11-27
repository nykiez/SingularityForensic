using CDFC.Parse.Android.DeviceObjects;
using CDFC.Parse.Contracts;
using CDFC.Util;
using Singularity.UI.FileSystem.Interfaces;
using System;
using System.IO;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.FileSystem.Android.Global.Services {
    public class AndroidDeviceStreamParser : GenericStaticInstance<AndroidDeviceStreamParser>, IStreamFileParser {
        public bool CheckIsValid(Stream stream) {
            if (stream is FileStream fs) {
                return AndroidDevice.CheckIsValid(fs);
            }
            return false;
        }

        public IFile ParseStream(Stream stream, Action<(int totalPro, int detailPro, string word, string desc)> ntfAct,Func<bool> isCancel) {
            if (stream is FileStream fs) {
                return AndroidDevice.LoadFromFileStream(fs, tuple => {
                    if (tuple.allSize != 0L && tuple.thePartSize != 0L) {
                        ntfAct(
                            (
                                (int)(tuple.curSize * 100L / tuple.allSize),
                                (int)(tuple.curPartSize * 100L / tuple.thePartSize),
                                FindResourceString("LoadingImg"),
                                $"{FindResourceString("LoadingPartition")}{tuple.curPart}/{tuple.allPart}"
                            )
                        );
                    }
                },isCancel);
            }
            return null;
        }

        //public IFile ParseStream(Stream stream, Action<(int totalPro, int detailPro, string word, string desc)> ntfAct, Func<bool> isCancel) {
        //    throw new NotImplementedException();
        //}
    }
}
