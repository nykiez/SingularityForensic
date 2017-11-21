using CDFC.PInvoke.Static;
using EventLogger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CDFC.Parse.ITunes.Models {
    //IOS备份文件;
    [Serializable]
    public class IOSBackFile {
        public IOSBackFile(IOSFileStruct st) {
            this.IOSFileStruct = st;
        }

        public IOSFileStruct IOSFileStruct { get; }
        
        private string _name;
        public string Name {
            get {
                if(_name == null) {
                    var lastIndex = IOSFileStruct.strPhonePath.LastIndexOf("\\");
                    if(lastIndex != -1) {
                        _name = IOSFileStruct.strPhonePath.Substring(lastIndex + 1);
                    }
                }
                return _name;
            }
        }

        private long? _size;
        public long Size {
            get {
                if(_size == null) {
                    try {
                        var fi = new FileInfo(IOSFileStruct.strLocalPath);
                        if (fi.Exists) {
                            _size = fi.Length;
                        }
                    }
                    catch(Exception ex) {
                        Logger.WriteCallerLine(ex.Message);
                    }
                }
                return _size??-1;
            }
        }
    }

   

}
