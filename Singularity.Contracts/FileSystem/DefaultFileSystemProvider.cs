using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFC.Parse.DeviceObjects;
using CDFC.Util;
using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.Contracts.FileSystem {
    public class DefaultFileSystemProvider : EmptyServiceProvider<DefaultFileSystemProvider>,IFileSystemServiceProvider {
        public IStreamFileParser StreamFileParser => DefaultFileStreamParser.StaticInstance;

        public void AddNewCaseFile(IFile file, string interLabel) {
            if (file is UnKnownDevice device) {
                ServiceProvider.Current.GetInstance<ICaseService>()?.AddNewCaseFile(new UnknownDeviceCaseFile(device, interLabel, DateTime.Now));
            }

        }

        public bool CheckIsValid(ICaseEvidence file) => file is UnknownDeviceCaseFile;

        public RegularFile GetFile(string path) {
            throw new NotImplementedException();
        }

        public TService GetService<TService>() {
            return (TService)GetService(typeof(TService));
        }

        public object GetService(Type ServiceType) {

            return null;
        }
        
        public FSFile OpenFile(ICaseEvidence caseEvidence, string path) {
            if(caseEvidence is UnknownDeviceCaseFile devc) {
                var oriFile = devc.Data.GetFileByUrl(path);
                if(oriFile != null) {
                    return new FSFile(oriFile);
                }
                return null;
            }
            throw new ArgumentException(nameof(caseEvidence));
        }
    }

    public class DefaultFileStreamParser : GenericStaticInstance<DefaultFileStreamParser>, IStreamFileParser {
        public bool CheckIsValid(Stream stream) => true;

        public IFile ParseStream(Stream stream, Action<(int totalPro, int detailPro, string word, string desc)> ntfAct, Func<bool> isCancel) {
            if (stream is FileStream fs) {
                return UnKnownDevice.LoadFromPath(fs.Name);
            }
            return null;
        }
    }
}
