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

        public CDFC.Parse.Abstracts.Directory OpenDirectory(string path) => null;

        public RegularFile OpenFile(string path) => null;

        public FSFile OpenFile(ICaseEvidence file, string path) {
            throw new NotImplementedException();
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
