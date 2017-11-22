using Singularity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDFC.Parse.Contracts;
using System.IO;
using CDFC.Parse.Android.DeviceObjects;
using static CDFCCultures.Managers.ManagerLocator;
using System.ComponentModel.Composition;

namespace Singularity.UI.FileSystem.Android {
    [Export(typeof(IStreamFileParser))]
    public class AndroidDeviceStreamParser : IStreamFileParser {
        public bool CheckIsValid(Stream stream) {
            if(stream is FileStream fs) {
                return AndroidDevice.CheckIsValid(fs);
            }
            return false;
        }

        public IFile ParseStream(Stream stream, Action<( double totalPro, double detailPro, string word, string desc)> ntfAct) {
            if (stream is FileStream fs) {
                return AndroidDevice.LoadFromFileStream(fs, tuple => {
                    if(tuple.allSize != 0L && tuple.thePartSize != 0L) {
                        ntfAct(
                            (
                                (int)(tuple.curSize * 100L / tuple.allSize),
                                (int)(tuple.curPartSize * 100L / tuple.thePartSize),
                                FindResourceString("LoadingImg"), 
                                $"{FindResourceString("LoadingPartition")}{tuple.curPart}/{tuple.allPart}"
                            )
                        );
                    }
                });
            }
            return null;
        }
    }
}
