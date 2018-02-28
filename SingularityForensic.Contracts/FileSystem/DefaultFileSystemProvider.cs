using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFC.Parse.DeviceObjects;
using CDFC.Util;
using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public class DefaultFileSystemProvider :
        EmptyServiceProvider<DefaultFileSystemProvider>,
        ICaseEvidenceServiceProvider {
        
        public void AddNewCaseFile(IFile file, string interLabel) {
            if (file is UnKnownDevice device) {
                //ServiceProvider.Current.GetInstance<ICaseService>()?.AddNewCaseFile(
                //    new CaseEvidence(device, interLabel, DateTime.Now));
            }

        }

        public bool CheckIsValid(CaseEvidence file) {
            throw new NotImplementedException();
        }

        //public bool CheckIsValid(CaseEvidence file) => file is UnknownDeviceCaseFile;

        public RegularFile GetFile(string path) {
            throw new NotImplementedException();
        }

        public TService GetService<TService>() {
            return (TService)GetService(typeof(TService));
        }

        public object GetService(Type ServiceType) {

            return null;
        }

        public FSFile OpenFile(CaseEvidence caseEvidence, string path) {
            //if (caseEvidence is UnknownDeviceCaseFile devc) {
            //    var oriFile = devc.Data.GetFileByUrl(path);
            //    if (oriFile != null) {
            //        return new FSFile(oriFile);
            //    }
            //    return null;
            //}
            //throw new ArgumentException(nameof(caseEvidence));
            return null;
        }
    }

    //public class DefaultFileStreamParser : GenericStaticInstance<DefaultFileStreamParser>, IImgParser {
    //    public bool CheckIsValid(Stream stream) => true;

    //    public IFile ParseStream(Stream stream, Action<(int totalPro, int detailPro, string word, string desc)> ntfAct, Func<bool> isCancel) {
    //        if (stream is FileStream fs) {
    //            return UnKnownDevice.LoadFromPath(fs.Name);
    //        }
    //        return null;
    //    }
    //}
}
