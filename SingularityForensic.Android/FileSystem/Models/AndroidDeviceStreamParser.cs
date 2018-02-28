using CDFC.Parse.Modules.DeviceObjects;
using CDFC.Parse.Contracts;
using CDFC.Util;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.IO;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Android.FileSystem.Models {
    public class AndroidDeviceStreamParser : GenericStaticInstance<AndroidDeviceStreamParser>, IImgParser {
        public string EvidenceTypeGuid => Constants.AndroidDeviceImg;

        public ICaseManager CaseManager => null;

        public int SortNum => 4;

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
                               LanguageService.Current?.FindResourceString("LoadingImg"),
                                $"{FindResourceString("LoadingPartition")}{tuple.curPart}/{tuple.allPart}"
                            )
                        );
                    }
                },isCancel);
            }
            return null;
        }
        

    }
}
